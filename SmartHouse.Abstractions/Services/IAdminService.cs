using SmartHouse.Core.DTOs;

namespace SmartHouse.Abstractions.Services
{
    public interface IAdminService
    {
        Task<List<UserDTO>> GetUsersByPageAsync(int currentPage, int PageSize);
        Task<int> GetTotalUsersCountAsync();
        Task RemoveByIdAsync(int id);
        public Task PatchAsync(int id, PatchDTO patchDTO);
        public Task<UserWithDeviceValueDTO> GetUserByIdAsync(int id);
    }
}