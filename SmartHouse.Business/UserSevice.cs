using AutoMapper;
using Microsoft.Extensions.Configuration;
using SmartHouse.Abstractions.Data;
using SmartHouse.Abstractions.Services;
using SmartHouse.Core.DTOs;
using SmartHouse.Data.Entities;
using SmartHouse.Data.PassworStorage;
using System.Security.Cryptography;
using System.Text;

namespace SmartHouse.Business
{
    public class UserSevice : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserSevice(IConfiguration configuration, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> GetTotalDevicesCountAsync(int id)
        {
            var count = await _unitOfWork.Devices.CountDeviceByUserId(id);
            return count;
        }

        public async Task<List<DeviceDTOWithValue>> GetDeicesByPageAsync(int id, int currentPage, int PageSize)
        {
            var devices = (await _unitOfWork.Devices.GetDeviceByPageAsync(id, currentPage, PageSize)).Select(device => _mapper.Map<DeviceDTOWithValue>(device)).ToList();
            return devices;
        }
    }
}
