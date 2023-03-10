using Eum.Core.Data;
using Eum.gRPC.Server.Auth.Modules.Token.Domains;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Eum.gRPC.Server.Auth.Modules.Token.Repositories
{
    public interface IPersonRepository : IRepository
    {
        IEnumerable<Person> GetUserInfoByEmail(string email);
        IEnumerable<Person> GetUserInfoByPersonCode(string personCode);
    }

    public class PersonRepository : DatabaseRepositoryBase, IPersonRepository
    {
        public PersonRepository(IConfiguration configuration) : base("EumCommon", configuration)
        {

        }

        public virtual IEnumerable<Person> GetUserInfoByEmail(string email)
        {
            var query = $@"
SELECT * FROM [dbo].[V_PERSON]
WHERE Email = @Email";

            return ExecuteQuery<Person>(query, new { Email = email });
        }

        public virtual IEnumerable<Person> GetUserInfoByPersonCode(string personId)
        {
            var query = $@"
SELECT * FROM [dbo].[V_PERSON]
WHERE PersonId = @PersonId";

            return base.ExecuteQuery<Person>(query, new { PersonId = personId });
        }
    }
}
