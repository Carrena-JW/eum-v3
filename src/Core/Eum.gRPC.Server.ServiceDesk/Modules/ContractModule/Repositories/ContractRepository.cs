using Eum.Core.Data;
using System.Collections.Generic;
using System.Xml.Linq;
using System;
using Eum.Core.Service.Contracts.ServiceDesk.ContractModule.Data;

namespace Eum.gRPC.Server.ServiceDesk.Modules.ContractModule.Repositories
{
    public interface IContractRepository : IRepository
    {
        IEnumerable<ContractDTO> Get();
        ContractDTO? Get(Guid id);
        ContractDTO? Set(ContractDTO item);
        bool Del(Guid id);
    }

    public class ContractRepository : DatabaseRepositoryBase, IContractRepository
    {
        public ContractRepository(IConfiguration configuration) : base("EumServiceDesk", configuration)
        {
        }

        public IEnumerable<ContractDTO> Get()
        {
            var @query = "SELECT * FROM [T_CONTRACT]";
            var results = ExecuteQuery<ContractDTO>(query);
            return results;
        }

        public ContractDTO? Get(Guid id)
        {
            var @query = "SELECT * FROM [T_CONTRACT] WHERE Id = @Id";
            return ExecuteQuery<ContractDTO>(query, new { Id = id }).FirstOrDefault();
        }

        public ContractDTO? Set(ContractDTO item)
        {
            if (item.Id != null && Get(item.Id.Value) == null)
            {
                var @query = @"
UPDATE [dbo].[T_CONTRACT]
   SET [Name] = @Name
      ,[Description] = @Description
      ,[CreatedId] = @CreatedId
      ,[CreatedDate] = GETUTCDATE()
   OUTPUT INSERTED.*
 WHERE Id = @Id";
                return ExecuteQuery<ContractDTO>(query, new
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
INSERT INTO[dbo].[T_CONTRACT]
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
                return ExecuteQuery<ContractDTO>(query, new
                {
                    item.Name,
                    item.Description,
                    item.CreatedId
                }).FirstOrDefault();
            }
        }

        public bool Del(Guid id)
        {
            var @query = "DELETE [T_CONTRACT] WHERE Id = @Id";
            return Convert.ToBoolean(ExecuteNonQuery(query, new { Id = id }));
        }
    }
}
