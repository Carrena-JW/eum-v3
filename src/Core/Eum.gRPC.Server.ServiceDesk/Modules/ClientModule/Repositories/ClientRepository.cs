using Eum.Core.Data;
using Eum.gRPC.Server.ServiceDesk.Modules.ClientModule.Domains;
using Eum.gRPC.Server.ServiceDesk.Shared.Domains;

namespace Eum.gRPC.Server.ServiceDesk.Modules.ClientModule.Repositories
{
    public interface IClientRepository : IRepository
    {
        IEnumerable<Client> Get();
        Client? Get(Guid id);
        Client? Set(Client item);
        Client? GetByEmail(string email);
        bool Del(Guid id);
    }

    public class ClientRepository : DatabaseRepositoryBase, IClientRepository
    {
        public ClientRepository(IConfiguration configuration) : base("EumServiceDesk", configuration)
        {
        }

        public IEnumerable<Client> Get()
        {
            var @query = "SELECT * FROM [T_CLIENT]";
            var results = ExecuteQuery<Client>(query);
            return results;
        }

        public Client? Get(Guid id)
        {
            var @query = "SELECT * FROM [T_CLIENT] WHERE Id = @Id";
            return ExecuteQuery<Client>(query, new { Id = id }).FirstOrDefault();
        }

        public Client? GetByEmail(string email)
        {
            var @query = "SELECT * FROM [T_CLIENT] WHERE Email = @Email";
            var user = ExecuteQuery<Client>(query, new { Email = email }).FirstOrDefault();
            return user;
        }

        public Client? Set(Client item)
        {
            // 아이디 지정되지 않은 경우, 메일 주소 중복 확인
            var user = !item.Id.HasValue ? GetByEmail(item.Email) : null;
            if (user != null) throw new DuplicateWaitObjectException(user.Email);

            if (item.Id.HasValue && Get(item.Id.Value) != null)
            {
                var @query = @"
UPDATE [dbo].[T_CLIENT]
   SET [Email] = @Email
      ,[CompanyId] = @CompanyId
      ,[Name] = @Name
      ,[CanLogin] = @CanLogin
      ,[Contact] = @Contact
      ,[UpdatedId] = @CreatedId
      ,[UpdatedDate] = GETUTCDATE()
   OUTPUT INSERTED.*
 WHERE Id = @Id";
                return ExecuteQuery<Client>(query, new
                {
                    item.Id,
                    item.Email,
                    item.CompanyId,
                    item.Name,
                    CanLogin = Convert.ToInt32(item.CanLogin),
                    item.Contact,
                    item.CreatedId
                }).FirstOrDefault();
            }
            else
            {
                var @query = @"
INSERT INTO[dbo].[T_CLIENT]
           ([Email]
           ,[CompanyId]
           ,[Name]
           ,[CanLogin]
           ,[Contact]
           ,[CreatedId]
           ,[CreatedDate])
     OUTPUT INSERTED.*
     VALUES
           (@Email
           ,@CompanyId
           ,@Name
           ,@CanLogin
           ,@Contact
           ,@CreatedId
           ,GETUTCDATE())";
                return ExecuteQuery<Client>(query, new
                {
                    item.Email,
                    item.CompanyId,
                    item.Name,
                    CanLogin = Convert.ToInt32(item.CanLogin),
                    item.Contact,
                    item.CreatedId
                }).FirstOrDefault();
            }
        }

        public bool Del(Guid id)
        {
            var @query = "DELETE [T_CLIENT] WHERE Id = @Id";
            return Convert.ToBoolean(ExecuteNonQuery(query, new { Id = id }));
        }
    }
}
