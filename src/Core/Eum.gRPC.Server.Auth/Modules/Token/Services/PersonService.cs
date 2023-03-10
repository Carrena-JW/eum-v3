using Eum.Core.Data;
using Eum.gRPC.Server.Auth.Modules.Token.Domains;
using Eum.gRPC.Server.Auth.Modules.Token.Exceptions;
using Eum.gRPC.Server.Auth.Modules.Token.Repositories;

namespace Eum.gRPC.Server.Auth.Modules.Token.Services
{
    public interface IPersonService : IService
    {
        Person GetPerson(string username);
    }

    public class PersonService : IPersonService
    {
        private IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public Person GetPerson(string username)
        {
            Person? user;
            if (username.Contains("@"))
                user = _personRepository.GetUserInfoByEmail(username).FirstOrDefault();
            else user = _personRepository.GetUserInfoByPersonCode(username).FirstOrDefault();
            if (user is null) throw new UserNotFoundException(username, $"User does not exist ({username})");
            return user;
        }
    }
}
