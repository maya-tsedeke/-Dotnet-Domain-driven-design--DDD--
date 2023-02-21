using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using userContext.domain.entities;

namespace domain.interfaces.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<User> Create(User user, string password);
        Task ChangePassword(User user, string oldPassword, string newPassword); 
    }
}
