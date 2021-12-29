using IotAdminAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IotAdminAPI.Services
{
    public interface IAuthenticationRepository
    {
        Task<User> ValidateCredentials(string userName, string passWord);
      


    }
}
