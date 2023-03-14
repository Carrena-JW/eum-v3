using Eum.Core.Shared.Models;
using Eum.Core.Data;
using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Eum.Core.Shared.Repositories
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

        public virtual IEnumerable<Person> GetUserInfoByPersonCode(string personCode)
        {
            var query = $@"
SELECT * FROM [dbo].[V_PERSON]
WHERE PersonCode = @PersonCode";

            return base.ExecuteQuery<Person>(query, new { PersonCode = personCode });
        }
    }
}
