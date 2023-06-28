using AspNetSamples.WebAPI.Responses;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartHouse.Abstractions.Services;
using SmartHouse.Core.DTOs;
using SmartHouse.WebAPI.Requests;
using SmartHouse.WebAPI.Responses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartHouse.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IAuthenticatorService _authenticatorService;

        public AdminController(IAdminService adminService,
            IConfiguration configuration,
            IMapper mapper,
            IUserService userService,
            IAuthenticatorService authenticatorService)
        {
            _adminService = adminService;
            _configuration = configuration;
            _mapper = mapper;
            _userService = userService;
            _authenticatorService = authenticatorService;
        }
        [HttpGet]
        [ProducesResponseType(typeof(UserResponse[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [Route("api/GetUsersByPage")]
        public async Task<IActionResult> GetUsersByPage([FromQuery] GetUsersByPagingInfoRequest getUsersByPagingInfoRequest)
        {
            var users = (await _adminService.GetUsersByPageAsync(getUsersByPagingInfoRequest.Page, getUsersByPagingInfoRequest.PageSize))
                .Select(user => _mapper.Map<UserResponse>(user)).ToArray();
            return Ok(users);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        //[Route("api/GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = _mapper.Map<UserResponse>(await _adminService.GetUserByIdAsync(id));
            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [Route("api/CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserRequest user)
        {
            var userDTO = _mapper.Map<UserDTO>(user);
            var userId = await _authenticatorService.RegistrateUserAsync(userDTO);
            var userResponse = await _adminService.GetUserByIdAsync(userId);
            return Ok(userResponse);
        }
        [HttpPost]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [Route("api/CreateAdmin")]
        public async Task<IActionResult> CreateAdmin([FromBody] AdminRequest user)
        {
            var adminDTO = _mapper.Map<AdminDTO>(user);
            var adminId = await _authenticatorService.RegisterAdminAsync(adminDTO);
            var admin = await _adminService.GetAdminByIdAsync(adminId);
            var adminResponse = _mapper.Map<AdminResponse>(admin);
            return Ok(adminResponse);
        }
        // PUT api/<AdminController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<AdminController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
