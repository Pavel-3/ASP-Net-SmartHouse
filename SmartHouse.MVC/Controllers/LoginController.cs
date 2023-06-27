using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHouse.Abstractions.Services;
using SmartHouse.Core.DTOs;
using SmartHouse.MVC.Models;
using System.Security.Claims;
using Serilog;

namespace SmartHouse.MVC.Controllers
{
    public class LoginController : Controller
    {

        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IAuthenticatorService _authenticatorService;

        public LoginController(IMapper mapper, IUserService userService, IConfiguration configuration, IAuthenticatorService authenticatorService)
        {
            _mapper = mapper;
            _userService = userService;
            _configuration = configuration;
            _authenticatorService = authenticatorService;
        }

        [HttpGet]
        public IActionResult Index([FromQuery]string? returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                var loginModel = new LoginModel()
                {
                    ReturnUrl = returnUrl
                };
                return View(loginModel);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginModel model)
        {
            try
            {
                if(await _authenticatorService.IsUserModelCorrect(model.Id, model.Password))
                {
                    await Authenticate(model.Id);
                    if (!string.IsNullOrEmpty(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    return RedirectToAction(actionName: "Index",controllerName: "Home");
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            try
            {
                await HttpContext.SignOutAsync();
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> IsIdExists(int id)
        {
            var result = await _authenticatorService.IsUserExistsAsync(id);
            return Ok(result);
            
        }

        private async Task Authenticate(int id)
        {
            try
            {
                const string authType = "Application Cookie";
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, id.ToString()),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, await _authenticatorService.GetRoleByIdAsync(id))
                };
                var claimsIdentity = new ClaimsIdentity(claims,
                    authType, 
                    ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }
}
