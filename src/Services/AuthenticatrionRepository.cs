using IotAdminAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace IotAdminAPI.Services
{
    public class AuthenticatrionRepository : IAuthenticationRepository
    {
        private readonly IotAdminDBContext _dbContext;
        public AuthenticatrionRepository(IotAdminDBContext dbcontext)
        {
            _dbContext = dbcontext;
        }
        public async Task<User> ValidateCredentials(string userName, string passWord)
        {
            return  await _dbContext.Users
                    .Include("UserRoles")
                    .Include("UserRoles.Role")
                     .Where<User>(o => o.Username == userName &&
                                 o.PasswordHash == passWord)
                     .FirstOrDefaultAsync();


           
        }
    }
}
