using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SmartHouse.Data.Entities;

namespace SmartHouse.Abstractions.Data.Repositories
{
    public interface IDeviceRepository : IRepository<Device>
    {
        public Task<List<Device>> GetDeviceByPageAsync(int id, int currentPage, int pageSize);
        public Task<int> AddAsync(Device device);
    }
}
