using SmartHouse.Abstractions.Data;
using SmartHouse.Data;
using SmartHouse.Data.Entities;
using SmartHouse.Abstractions.Data.Repositories;
using SmartHouse.Repositories.Implementation;
//using SmartHouse.Data.AdminDb.Entities;
using SmartHouse.Data.PassworStorage;

namespace SmartHouse.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HouseContext _dbContext;
        private readonly PassworStorageDbContext _PassworStorageContext;
        private readonly IAdminRepository _adminRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDeviceRepository _deviceRepository;
        public UnitOfWork(HouseContext dbContext, 
            IAdminRepository adminRepository, 
            IUserRepository userRepository, 
            IDeviceRepository deviceRepository,
            PassworStorageDbContext adminDbContext) 
        { 
            _dbContext = dbContext;
            _adminRepository = adminRepository;
            _userRepository = userRepository;
            _deviceRepository = deviceRepository;
            _PassworStorageContext = adminDbContext;
        }

        public IAdminRepository Admins => _adminRepository;
        public IUserRepository Users => _userRepository;
        public IDeviceRepository Devices => _deviceRepository;

        public IPassworStorageRepository PassworStorages => throw new NotImplementedException();

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        public void Commit()
        {
            _dbContext.SaveChanges();
        }
        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public Task CommitPasswordStorageAsync()
        {
            _PassworStorageContext
        }
    }
}