using UserTransactions.Application.DTOs;

namespace UserTransactions.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO?> GetUserByIdAsync(string id);
        Task<UserDTO> AddUserAsync(UserUpsertDTO userUpsertDto);
        Task<UserDTO> EditUserAsync(string id, UserUpsertDTO userUpsertDto);
        Task DeleteUserByIdAsync(string id);
    }
}
