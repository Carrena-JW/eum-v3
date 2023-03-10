using System.Security.Claims;

namespace Eum.Core.Shared.Models
{
    public interface IToken
    {
        string AccessToken { get; set; }
        string RefreshToken { get; set; }
        DateTime Expires { get; set; }
        IEnumerable<Claim> Claims { get; set; }
    }

    public class JsonWebToken : IToken
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expires { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
    }
}
