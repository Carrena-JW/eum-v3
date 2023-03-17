using Eum.Core.Data;
using Eum.gRPC.Server.ServiceDesk.Modules.ClientModule.Domains;
using Eum.gRPC.Server.ServiceDesk.Modules.CompanyModule.Domains;

namespace Eum.gRPC.Server.ServiceDesk.Modules.CompanyModule.Repositories
{
    public interface ICompanyRepository : IRepository
    {
        IEnumerable<Company> Get();
        Company? Get(Guid id);
        Company? Set(Company item);
        bool Del(Guid id);
    }

    public class CompanyRepository : DatabaseRepositoryBase, ICompanyRepository
    {
        public CompanyRepository(IConfiguration configuration) : base("EumServiceDesk", configuration)
        {
        }

        public IEnumerable<Company> Get()
        {
            var @query = "SELECT * FROM [T_COMPANY]";
            var results = ExecuteQuery<Company>(query);
            return results;
        }

        public Company? Get(Guid id)
        {
            var @query = "SELECT * FROM [T_COMPANY] WHERE Id = @Id";
            return ExecuteQuery<Company>(query, new { Id = id }).FirstOrDefault();
        }

        public Company? Set(Company item)
        {
            if (item.Id != null && Get(item.Id.Value) == null)
            {
                var @query = @"
UPDATE [dbo].[T_COMPANY]
   SET [Name] = @Name
      ,[Description] = @Description
      ,[CreatedId] = @CreatedId
      ,[CreatedDate] = GETUTCDATE()
   OUTPUT INSERTED.*
 WHERE Id = @Id";
                return ExecuteQuery<Company>(query, new
                {
                    item.Id,
                    item.Name,
                    item.Description,
                    item.CreatedId
                }).FirstOrDefault();
            }
            else
            {
                var @query = @"
INSERT INTO[dbo].[T_COMPANY]
           ([Name]
           ,[Description]
           ,[CreatedId]
           ,[CreatedDate])
     OUTPUT INSERTED.*
     VALUES
           (@Name
           ,@Description
           ,@CreatedId
           ,GETUTCDATE())";
                return ExecuteQuery<Company>(query, new
                {
                    item.Name,
                    item.Description,
                    item.CreatedId
                }).FirstOrDefault();
            }
        }

        public bool Del(Guid id)
        {
            var @query = "DELETE [T_COMPANY] WHERE Id = @Id";
            return Convert.ToBoolean(ExecuteNonQuery(query, new { Id = id }));
        }
    }
}
