using AuthorizationAPI.DAL;
using DataLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthorizationAPI.Interface
{
    public interface IUserService
    {
        TokenResponse Authenticate(string user, string password);
        string RefreshToken(string token);
        Task Registration(string user, string password);
        Task<IList<User>> Get();
    }
}
