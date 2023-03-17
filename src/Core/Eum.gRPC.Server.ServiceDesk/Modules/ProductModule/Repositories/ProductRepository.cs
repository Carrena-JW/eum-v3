using Eum.Core.Data;
using Eum.gRPC.Server.ServiceDesk.Modules.ProductModule.Domains;

namespace Eum.gRPC.Server.ServiceDesk.Modules.ProductModule.Repositories
{
    public interface IProductRepository : IRepository
    {
        IEnumerable<Product> Get();
        Product? Get(Guid id);
        Product? Set(Product item);
        bool Del(Guid id);
    }

    public class ProductRepository : DatabaseRepositoryBase, IProductRepository
    {
        public ProductRepository(IConfiguration configuration) : base("EumServiceDesk", configuration)
        {
        }

        public IEnumerable<Product> Get()
        {
            var @query = "SELECT * FROM [T_PRODUCT]";
            var results = ExecuteQuery<Product>(query);
            return results;
        }

        public Product? Get(Guid id)
        {
            var @query = "SELECT * FROM [T_PRODUCT] WHERE Id = @Id";
            return ExecuteQuery<Product>(query, new { Id = id }).FirstOrDefault();
        }

        public Product? Set(Product item)
        {
            if (item.Id != null && Get(item.Id.Value) == null)
            {
                var @query = @"
UPDATE [dbo].[T_PRODUCT]
   SET [Name] = @Name
      ,[Description] = @Description
      ,[CreatedId] = @CreatedId
      ,[CreatedDate] = GETUTCDATE()
   OUTPUT INSERTED.*
 WHERE Id = @Id";
                return ExecuteQuery<Product>(query, new
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
INSERT INTO[dbo].[T_PRODUCT]
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
                return ExecuteQuery<Product>(query, new
                {
                    item.Name,
                    item.Description,
                    item.CreatedId
                }).FirstOrDefault();
            }
        }

        public bool Del(Guid id)
        {
            var @query = "DELETE [T_PRODUCT] WHERE Id = @Id";
            return Convert.ToBoolean(ExecuteNonQuery(query, new { Id = id }));
        }
    }
}
