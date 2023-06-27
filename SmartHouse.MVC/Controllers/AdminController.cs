using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartHouse.Abstractions.Services;
using SmartHouse.Core.DTOs;
using SmartHouse.MVC.Models;

namespace SmartHouse.MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AdminController(IAdminService adminService, IConfiguration configuration, IMapper mapper)
        {
            _mapper = mapper;
            _adminService = adminService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Home(int page = 1, int pageSize = 2)
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
        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserModel user)
        {
            //user.Devices = new List<DeviceModel>();
            //user.Devices.Add(new DeviceModel()
            //{
            //    Name = "newDevice",
            //    DeviceType = DeviceType.Sensor
            //});
            await _adminService.AddUserAsync(_mapper.Map<UserDTO>(user));
            return RedirectToAction("Home");
        }


    }
}
