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

        public async Task RemoveByIdAsync(int id)
        {
            await _unitOfWork.Users.RemoveAsync(id);
            await _unitOfWork.CommitAsync();
        }

        public async Task<int> GetTotalUsersCountAsync()
        {
            var count = await _unitOfWork.Users.CountAsync();
            return count;
        }

        public async Task<List<UserDTO>> GetUsersByPageAsync(int currentPage, int PageSize)
        {
            var users = (await _unitOfWork.Users.GetUsersByPageAsync(currentPage, PageSize)).Select(user => _mapper.Map<UserDTO>(user)).ToList();
            return users;
        }
        public async Task PatchAsync(int id, PatchDTO patchDTO)
        {
            await _unitOfWork.Users.PatchAsync(id, new List<PatchDTO>() { patchDTO });
            await _unitOfWork.CommitAsync();
        }

        public async Task<UserWithDeviceValueDTO> GetUserByIdAsync(int id)
        {
            var user = _mapper.Map<UserWithDeviceValueDTO>(await _unitOfWork.Users.GetByIdAsync(id));
            return user;
        }
    }
}