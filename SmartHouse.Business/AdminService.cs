using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Microsoft.EntityFrameworkCore;
using SmartHouse.Abstractions.Data;
using SmartHouse.Abstractions.Services;
using SmartHouse.Core.DTOs;
using SmartHouse.Data;
using SmartHouse.Data.PassworStorage;
using SmartHouse.Data.Entities;
using System.Security.Cryptography;

namespace SmartHouse.Business
{
    public class AdminService : IAdminService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public AdminService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
             _unitOfWork = unitOfWork;
        }

        public async Task AddUserAsync(UserDTO userDTO)
        {
            var password = GetRandomPassword(10);
            var passwordHash = "0"; //CreatePasswordHash(password);
            userDTO.PasswordHash = passwordHash;
            var entityEntry = await _unitOfWork.Users.AddAsync(_mapper.Map<User>(userDTO));
            await _unitOfWork.CommitAsync();
            var userId = entityEntry.Entity.Id;
            var passwordModel = new Password()
            {
                PasswordString = password,
                UserId = userId
            };
            userDTO.Devices.Select(dto => dto.Id = userId);
            await _unitOfWork.Devices.AddRangeAsync(userDTO?.Devices.Select(dto => _mapper.Map<Device>(dto)));
            await _unitOfWork.CommitAsync();
        }

        public async Task<int> GetTotalUsersCountAsync()
        {
            var count = await _unitOfWork.Users.CountAsync();
            return count;
        }

        public IQueryable<User> GetUsersAsIQueryable()
        {
            return _unitOfWork.Users.GetDbSetAsQueryable()
                .Include(user => user.Rooms)
                .Include(room => room.Devices)
                .AsNoTracking();
        }

        public async Task<List<UserDTO>> GetUsersByPageAsync(int currentPage, int PageSize)
        {
            var users = (await _unitOfWork.Users
                .GetUsersByPageAsync(currentPage, PageSize))
                .Select(user => _mapper.Map<UserDTO>(user))
                .ToList();
            return users;
        }
        public static string GetRandomPassword(int length)
        {
            byte[] rgb = new byte[length];
            RNGCryptoServiceProvider rngCrypt = new RNGCryptoServiceProvider();
            rngCrypt.GetBytes(rgb);
            return Convert.ToBase64String(rgb);
        }
    }
}