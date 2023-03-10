using Eum.Core.Data;
using Eum.gRPC.Server.Auth.Modules.Token.Domains;
using System.ComponentModel;
using System.Data;

namespace Eum.gRPC.Server.Auth.Modules.Token.Repositories
{
    // TODO: 공통으로 빼야 함.
    public interface IAppConfigRepository : IRepository
    {
        IEnumerable<AppConfig> GetConfigs(bool? serverOnly = null);
        IEnumerable<AppConfig> GetConfigs();
        AppConfig GetByKey(string key);
        T GetByKey<T>(string key, T defaultValue);
    }

    public class AppConfigRepository : DatabaseRepositoryBase, IAppConfigRepository
    {
        protected const string TABLE_NAME = "AppConfig";
        protected ILogger<AppConfigRepository> _logger;

        public AppConfigRepository(ILogger<AppConfigRepository> logger, IConfiguration configuration) : base("EumCommon", configuration)
        {
            _logger = logger;
        }

        public virtual IEnumerable<AppConfig> GetConfigs(bool? serverOnly = null)
        {
            var query = $@"
SELECT * FROM [dbo].[{TABLE_NAME}]";
            if (serverOnly.HasValue)
            {
                query += " WHERE [ServerOnly] = " + (Convert.ToInt32(serverOnly.Value));
            }
            return base.ExecuteQuery<AppConfig>(query, new
            {
                ServerOnly = serverOnly
            });
        }

        public virtual IEnumerable<AppConfig> GetConfigs()
        {
            var query = $@"
SELECT * FROM [dbo].[{TABLE_NAME}]";
            return base.ExecuteQuery<AppConfig>(query, new { });
        }

        public virtual AppConfig GetByKey(string key)
        {
            var @query = @"
SELECT * FROM TC_APP_CONFIG
WHERE [Key] = @Key";
            return ExecuteQuery<AppConfig>(@query, new { Key = key }).FirstOrDefault();
        }

        public virtual T GetByKey<T>(string key, T defaultValue)
        {
            var config = this.GetByKey(key);
            if (config == null)
            {
                if (defaultValue == null)
                    throw new KeyNotFoundException(key);
                return defaultValue;
            }
            else
            {
                var type = typeof(T);
                var converter = TypeDescriptor.GetConverter(type);
                return (T)converter.ConvertFrom(config.Value);
            }
        }
    }
}
