using SmartHouse.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse.Abstractions.Services
{
    public interface ILoginService
    {
        Task<bool> IsUserExistsAsync(int id);
        Task LoginAsync(UserDTO user);
    }
}
