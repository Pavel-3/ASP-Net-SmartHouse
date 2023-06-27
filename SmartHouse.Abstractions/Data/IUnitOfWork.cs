using SmartHouse.Abstractions.Data.Repositories;
using SmartHouse.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse.Abstractions.Data
{
    public interface IUnitOfWork
    {
        public IAdminRepository Admins { get; }
        public IUserRepository Users { get; }
        public IDeviceRepository Devices { get; }
        public IPassworStorageRepository PassworStorages { get; }
        public Task CommitAsync();
        public void Commit();
        public Task CommitPasswordStorageAsync();
    }
}
