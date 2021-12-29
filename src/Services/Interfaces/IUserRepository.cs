using IotAdminAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IotAdminAPI.Services
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        Task<User> GetById(int id);
        Task<int> Add(User user);

        Task<int> Update(User user);

        Task<int> Delete(int id);


    }
}
