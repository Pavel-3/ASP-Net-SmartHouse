using SmartHouse.Core.DTOs;

namespace SmartHouse.Abstractions.Services
{
    public interface IAdminService
    {
        Task<List<UserDTO>> GetUsersByPageAsync(int currentPage, int PageSize);
        Task<int> GetTotalUsersCountAsync();
    }
}