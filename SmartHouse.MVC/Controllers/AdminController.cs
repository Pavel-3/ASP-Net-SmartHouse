using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using SmartHouse.Abstractions.Services;
using SmartHouse.Core.DTOs;
using SmartHouse.MVC.Models;

namespace SmartHouse.MVC.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IAuthenticatorService _authenticatorService;

        public AdminController(IAdminService adminService, IConfiguration configuration, IMapper mapper, IUserService userService, IAuthenticatorService authenticatorService)
        {
            _mapper = mapper;
            _adminService = adminService;
            _configuration = configuration;
            _userService = userService;
            _authenticatorService = authenticatorService;
        }

        [HttpGet]
        public async Task<IActionResult> Home(int page = 1, int pageSize = 2)
        {
            try
            {
            var totalItems = await _adminService.GetTotalUsersCountAsync();
            var pageInfo = new PageInfoModel()
            {
                PageSize = pageSize,
                PageNumber = page,
                TotalItems = totalItems
            };
            var users = (await _adminService.GetUsersByPageAsync(page, pageSize)).Select(user => _mapper.Map<UserPreviewModel>(user)).ToList();

            return View(new UsersWithPaginationModel()
            {
                Users = users,
                PageInfo = pageInfo
            });
            }
            catch(Exception ex)
            {
                Log.Error(ex, ex.Message);
                return StatusCode(500, new { Message = ex.Message });
            }

        }
        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserModel user)
        {
            if (ModelState.IsValid)
            {
                await _authenticatorService.RegistrateUserAsync(_mapper.Map<UserDTO>(user));
                return RedirectToAction("Home");
            }
            else
            {
                return View(user);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var user = await _adminService.GetUserByIdAsync(id);
                return View(_mapper.Map<UserDetailModel>(user));
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return StatusCode(500, new { ex.Message });
            }

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id) 
        {
            await _adminService.RemoveByIdAsync(id);
            return RedirectToAction("Home");
        }
        [HttpPost]
        public async Task<IActionResult> ChangeName(UserDetailModel model)
        {
            await _adminService.PatchAsync(model.Id, new PatchDTO()
            {
                PropertyName = "Name",
                PropertyValue = model.Name
            });
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ChangeEmail(UserDetailModel model)
        {
            await _adminService.PatchAsync(model.Id, new PatchDTO()
            {
                PropertyName = "Email",
                PropertyValue = model.Email
            });
            return View(model);
        }
    }
}
