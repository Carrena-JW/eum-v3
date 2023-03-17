using Eum.Core.Data;
using Eum.Core.Service.Contracts.ServiceDesk.Data.Step;
using System.Collections.Generic;
using System.Xml.Linq;
using System;

namespace Eum.gRPC.Server.ServiceDesk.Modules.StepModule.Repositories
{
    public interface IStepRepository : IRepository
    {
        IEnumerable<StepDTO> Get();
        StepDTO? Get(Guid id);
        StepDTO? Set(StepDTO item);
        bool Del(Guid id);
    }

    public class StepRepository : DatabaseRepositoryBase, IStepRepository
    {
        public StepRepository(IConfiguration configuration) : base("EumServiceDesk", configuration)
        {
        }

        public IEnumerable<StepDTO> Get()
        {
            var @query = "SELECT * FROM [T_STEP]";
            var results = ExecuteQuery<StepDTO>(query);
            return results;
        }

        public StepDTO? Get(Guid id)
        {
            var @query = "SELECT * FROM [T_STEP] WHERE Id = @Id";
            return ExecuteQuery<StepDTO>(query, new { Id = id }).FirstOrDefault();
        }

        public StepDTO? Set(StepDTO item)
        {
            if (item.Id != null && Get(item.Id.Value) == null)
            {
                var @query = @"
UPDATE [dbo].[T_STEP]
   SET [Name] = @Name
      ,[Description] = @Description
      ,[CreatedId] = @CreatedId
      ,[CreatedDate] = GETUTCDATE()
   OUTPUT INSERTED.*
 WHERE Id = @Id";
                return ExecuteQuery<StepDTO>(query, new
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
INSERT INTO[dbo].[T_STEP]
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
                return ExecuteQuery<StepDTO>(query, new
                {
                    item.Name,
                    item.Description,
                    item.CreatedId
                }).FirstOrDefault();
            }
        }

        public bool Del(Guid id)
        {
            var @query = "DELETE [T_STEP] WHERE Id = @Id";
            return Convert.ToBoolean(ExecuteNonQuery(query, new { Id = id }));
        }
    }
}
