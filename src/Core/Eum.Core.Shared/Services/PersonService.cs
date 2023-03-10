using Eum.Core.Shared.Exceptions;
using Eum.Core.Shared.Models;
using Eum.Core.Shared.Repositories;
using Eum.Core.Data;

namespace Eum.Core.Shared.Services
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
