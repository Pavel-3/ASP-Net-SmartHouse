using SmartHouse.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse.Abstractions.Services
{
    public interface IAuthenticatorService
    {
        Task<bool> IsUserModelCorrect(int id, string password);
        Task<bool> IsUserExistsAsync(int id);
        Task<int> RegistrateUserAsync(UserDTO user);
        Task<string> GetRoleByIdAsync(int id);
        Task<int> RegisterAdminAsync(AdminDTO admin);
    }
}
