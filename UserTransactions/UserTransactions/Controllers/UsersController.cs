using Microsoft.AspNetCore.Mvc;
using UserTransactions.Application.DTOs;
using UserTransactions.Application.Interfaces;

namespace UserTransactions.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();
            return users.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return user;
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> Add(UserUpsertDTO userUpsertDto)
        {
            var user = await _userService.AddUserAsync(userUpsertDto);
            return user;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserDTO>> Put(string id, UserUpsertDTO userUpsertDto)
        { 
            var user = await _userService.EditUserAsync(id, userUpsertDto);
            return user;
        }

        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await _userService.DeleteUserByIdAsync(id);
        }
    }
}
