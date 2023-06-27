using SmartHouse.Abstractions.Services;
using SmartHouse.Core.DTOs;
using SmartHouse.Data.Entities;
using SmartHouse.Data.PassworStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse.Business
{
    public class UserSevice : IUserService
    {
        public Task<bool> IsUserExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task LoginAsync(UserDTO user)
        {
            throw new NotImplementedException();
        }
        public async Task RegistrateUserAsync(UserDTO userDTO)
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
    }
}
