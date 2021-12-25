using IotAdminAPI.Models;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IotAdminAPI.Services
{
    [Authorize]
    public class UserRepository : IUserRepository
    {
        private readonly IotAdminDBContext _dbContext;
        public UserRepository(IotAdminDBContext dbcontext)
        {
            _dbContext = dbcontext;
        }
        public async Task<int> Add(User user)
        {
            User dbuser = new User();
            dbuser.Name = user.Name;
            dbuser.Email = user.Email;
            dbuser.Phone = user.Phone;
            dbuser.Username = user.Username;
            dbuser.PasswordHash= user.PasswordHash;

            _dbContext.Users.Add(dbuser);

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            User user = await _dbContext.Users.FindAsync(id);

            _dbContext.Remove(user);
           
           return await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<User> GetAll()
        {
            return  _dbContext.Users ;
        }
        public async Task<User> GetById(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }
        public async Task< int> Update(User user)
        {
            User dbuser = await _dbContext.Users.FindAsync(user.Id);
            dbuser.Name = user.Name;
            dbuser.Email=user.Email;
            dbuser.Phone = user.Phone; 
            dbuser.Username = user.Username;
           

            
            _dbContext.Entry(dbuser).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
          return  await _dbContext.SaveChangesAsync();
        }

       
    }
}
