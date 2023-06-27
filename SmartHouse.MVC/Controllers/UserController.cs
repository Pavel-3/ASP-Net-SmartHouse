using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHouse.Abstractions.Services;
using SmartHouse.Business;
using SmartHouse.Data.Entities;
using SmartHouse.MVC.Models;

namespace SmartHouse.MVC.Controllers
{
    [Authorize(Roles ="User")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;


        public UserController(IUserService userService, IMapper mapper)
        {
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<IActionResult> Index(int page = 1, int pageSize = 5)
        {
            var userName = HttpContext.User.Identity?.Name;
            if (int.TryParse(userName, out int id))
            {
                var totalItems = await _userService.GetTotalDevicesCountAsync(id);
                var pageInfo = new PageInfoModel()
                {
                    PageSize = pageSize,
                    PageNumber = page,
                    TotalItems = totalItems
                };
                var devices = (await _userService.GetDeicesByPageAsync(id, page, pageSize)).Select(device => _mapper.Map<DeviceWithValueModel>(device)).ToList();
                var devicesWithPagination = new DevicesWithPaginationModel()
                {
                    PageInfo = pageInfo,
                    Devices = devices
                };
                return View(devicesWithPagination);
            }
            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        public async Task<IActionResult> ChangeDeviceValue(DevicesWithPaginationModel model)
        {
            return RedirectToAction("Index");
        }
    }
}
