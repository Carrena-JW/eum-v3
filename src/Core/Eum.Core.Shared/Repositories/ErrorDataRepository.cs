using Eum.Core.Data;
using Eum.Core.Shared.Domains;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eum.Core.Shared.Repositories
{
    public interface IErrorDataRepository : IRepository
    {
        Error Create(Error item);
    }

    public class ErrorDataRepository : DatabaseRepositoryBase, IErrorDataRepository
    {
        public ErrorDataRepository(IConfiguration configuration) : base("EumCommon", configuration)
        {

        }

        public Error Create(Error item)
        {
            var query = $@"
INSERT INTO dbo.{nameof(Error)} 
           ([MachineName]
           ,[Path]
           ,[Type]
           ,[Message]
           ,[Detail]
           ,[CreatedId])
	 OUTPUT INSERTED.*
     VALUES
           (@MachineName
           ,@Path
           ,@Type
           ,@Message
           ,@Detail
           ,@CreatedId);";
            var result = base.ExecuteQuery<Error>(query, item);
            return result.FirstOrDefault();
        }
    }
}
