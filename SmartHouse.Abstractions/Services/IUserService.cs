using SmartHouse.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse.Abstractions.Services
{
    public interface IUserService
    {

        Task<int> GetTotalDevicesCountAsync(int id);
        Task<List<DeviceDTOWithValue>> GetDeicesByPageAsync(int id, int currentPage, int PageSize);

    }
}
