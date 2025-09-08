using AutoMapper;
using UserTransactions.Application.DTOs;
using UserTransactions.Application.Interfaces;
using UserTransactions.Domain.Entities;
using UserTransactions.Domain.Exceptions;
using UserTransactions.Infrastructure.Interfaces;

namespace UserTransactions.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO?> GetUserByIdAsync(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserDTO>(user);
        }
        public async Task<UserDTO> AddUserAsync(UserUpsertDTO userUpsertDto)
        {
            var userDto = _mapper.Map<UserDTO>(userUpsertDto);
            userDto.UserId = Guid.NewGuid().ToString();
            
            var user = _mapper.Map<User>(userDto);
            var newUser = await _userRepository.AddAsync(user);

            return _mapper.Map<UserDTO>(newUser);
        }

        public async Task<UserDTO> EditUserAsync(string id, UserUpsertDTO userUpsertDto)
        {
            if ((await GetUserByIdAsync(id)) == null)
            {
                throw new UserNotFoundException($"[USER_UPDATE] User with id: {id} not found.");
            }

            var userDto = _mapper.Map<UserDTO>(userUpsertDto);
            userDto.UserId = id;

            var user = _mapper.Map<User>(userDto);
            await _userRepository.UpdateAsync(user);

            return userDto;
        }
        public async Task DeleteUserByIdAsync(string id)
        {
            if ((await GetUserByIdAsync(id)) == null)
            {
                throw new UserNotFoundException($"[DELETE_USER] User with id: {id} not found.");
            }

            await _userRepository.DeleteByIdAsync(id);
        }
    }
}
