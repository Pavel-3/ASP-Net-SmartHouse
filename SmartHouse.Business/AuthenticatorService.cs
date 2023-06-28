using AutoMapper;
using Microsoft.Extensions.Configuration;
using SmartHouse.Abstractions.Data;
using SmartHouse.Abstractions.Services;
using SmartHouse.Core.DTOs;
using SmartHouse.Data.Entities;
using SmartHouse.Data.PassworStorage;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse.Business
{
    public class AuthenticatorService : IAuthenticatorService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticatorService(IConfiguration configuration, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> IsUserExistsAsync(int id)
        {
            if ((await _unitOfWork.Users.GetByIdAsync(id)) != null)
            {
                return true;
            }
            else if ((await _unitOfWork.Admins.GetByIdAsync(id)) != null)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> IsUserModelCorrect(int id, string password)
        {
            var passwordHash = GetPasswordHash(password);
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            var admin = await _unitOfWork.Admins.GetByIdAsync(id);
            if (user != null)
            {
                return user.PasswordHash == passwordHash;
            }
            if (admin != null)
            {
                return admin.PasswordHash == passwordHash;
            }
            return false;
        }
        public async Task<int> RegistrateUserAsync(UserDTO userDTO)
        {
            var password = GetRandomPassword(10);
            var passwordHash = GetPasswordHash(password);
            userDTO.PasswordHash = passwordHash;
            var entityEntry = await _unitOfWork.Users.AddAsync(_mapper.Map<User>(userDTO));
            await _unitOfWork.CommitAsync();
            var userId = entityEntry.Entity.Id;
            var passwordModel = new Password()
            {
                PasswordString = password,
                UserId = userId
            };
            await _unitOfWork.PassworStorages.AddPasswordAsync(passwordModel);
            await _unitOfWork.CommitPasswordStorageAsync();
            userDTO.Devices.Select(dto => dto.Id = userId);
            await _unitOfWork.Devices.AddRangeAsync(userDTO?.Devices.Select(dto => _mapper.Map<Device>(dto)));
            await _unitOfWork.CommitAsync();
            return userId;
        }
        public async Task<string?> GetRoleByIdAsync(int id)
        {
            if ((await _unitOfWork.Admins.GetByIdAsync(id)) != null)
            {
                return "Admin";
            }
            if ((await _unitOfWork.Users.GetByIdAsync(id)) != null)
            {
                return "User";
            }
            throw new ArgumentException("User not found");
        }
        public async Task<int> RegisterAdminAsync(AdminDTO admin)
        {
            var password = GetRandomPassword(10);
            var passwordHash = GetPasswordHash(password);
            admin.PasswordHash = passwordHash;
            var entityEntry = await _unitOfWork.Admins.AddAsync(_mapper.Map<Admin>(admin));
            await _unitOfWork.CommitAsync();
            var adminId = entityEntry.Entity.Id;
            var passwordModel = new Password()
            {
                PasswordString = password,
                UserId = adminId
            };
            await _unitOfWork.PassworStorages.AddPasswordAsync(passwordModel);
            await _unitOfWork.CommitPasswordStorageAsync();
            return adminId;
        }

        private string GetRandomPassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
        private string GetPasswordHash(string password)
        {
            var sb = new StringBuilder();
            using (var hash = SHA256.Create())
            {
                var encoding = Encoding.UTF8;
                var result = hash
                    .ComputeHash(
                        encoding
                            .GetBytes($"{password}{_configuration["Secrets:Salt"]}"));
                foreach (var b in result)
                {
                    sb.Append(b.ToString("x2"));
                }
            }
            return sb.ToString();
        }
    }
}
