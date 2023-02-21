using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.interfaces.Services
{
    public interface IAuthorizationService
    {
        Task<bool> Authorize(int userId, string permissionName);
    }
}
