using System;
using System.Collections.Generic;

#nullable disable

namespace IotAdminAPI.Models
{
    public partial class User
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string ChangePasswordKey { get; set; }
        public int? RetryCount { get; set; }
        public DateTime? LockTime { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsFirstTimeLogin { get; set; }
        public DateTime? FirstLoginTime { get; set; }
        public DateTime? LastLoginTime { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
