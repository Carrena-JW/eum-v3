using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Reflection;
using static Dapper.SqlMapper;

namespace Eum.Core.Data
{
    public abstract class DatabaseRepositoryBase : IRepository
    {
        private readonly string _connectionString;

        protected virtual DbConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        protected DatabaseRepositoryBase(string connectionStringKey, IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetService(typeof(IConfiguration)) as IConfiguration;
            string? connectionString = configuration!.GetValue<string>($"ConnectionStrings:{connectionStringKey}");
            _connectionString = connectionString ??
                                throw new NullReferenceException(
                                    "You must provided connection string fom appsettings.json.");
        }

        public IEnumerable<TResult> ExecuteQuery<TResult>(string query, object param = null,
            CommandType commandType = CommandType.Text,
            DbTransaction transaction = null, int? commandTimeout = null)
        {
            using var connection = this.GetConnection();
            return connection.Query<TResult>(query, param, transaction, true, commandTimeout, commandType);
        }

        public int ExecuteNonQuery(string query, object param, CommandType commandType = CommandType.Text,
            DbTransaction transaction = null, int? commandTimeout = null)
        {
            using var connection = this.GetConnection();
            return connection.Execute(query, param, transaction, commandTimeout, commandType);
        }

        public TResult ExecuteScalar<TResult>(string query, object param = null, CommandType? commandType = null)
        {
            using var connection = this.GetConnection();
            return connection.ExecuteScalar<TResult>(query, param: param, commandType: commandType);
        }

        public IEnumerable<TResult> ExecuteStoredProcedure<TResult>(string query, object param = null)
        {
            return ExecuteQuery<TResult>(query, param: param, commandType: CommandType.StoredProcedure);
        }

        #region [ Helpers ]
        public virtual DataTable ToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names 
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow 
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
        #endregion
    }
}