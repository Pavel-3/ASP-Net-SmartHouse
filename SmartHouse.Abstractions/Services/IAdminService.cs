using SmartHouse.Core.DTOs;
using SmartHouse.Data.Entities;

namespace SmartHouse.Abstractions.Services
{
    public interface IAdminService
    {
        Task<List<UserDTO>> GetUsersByPageAsync(int currentPage, int PageSize);
        Task<int> GetTotalUsersCountAsync();
        Task RemoveByIdAsync(int id);
        Task PatchAsync(int id, PatchDTO patchDTO);
        Task<UserWithDeviceValueDTO> GetUserByIdAsync(int id);
        Task<AdminDTO> GetAdminByIdAsync(int id);
    }
}