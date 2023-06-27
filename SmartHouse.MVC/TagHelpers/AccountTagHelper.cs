using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SmartHouse.MVC.Models;
using System.Runtime.CompilerServices;
using SmartHouse.Business;
using Microsoft.AspNetCore.Authorization;
using SmartHouse.Abstractions.Services;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace SmartHouse.MVC.TagHelpers
{
    public class AccountTagHelper : TagHelper
    {
        //private readonly UserManager<IdentityUser> _userManager;
        public PageInfoModel PageInfo { get; set; }
        private IUrlHelperFactory _urlHelperFactory;
        private readonly IAuthenticatorService _authenticatorService;
        public string PageAction { get; set; }
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public AccountTagHelper(IUrlHelperFactory urlHelperFactory, IAuthenticatorService authenticatorService)
        {
            _urlHelperFactory = urlHelperFactory;
            _authenticatorService = authenticatorService;
        }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var userIdentity = ViewContext.HttpContext.User.Identity;
            if (userIdentity.IsAuthenticated)
            {
                output.TagName = "div";
                output.Attributes.SetAttribute("class", "btn-group dropstart");

                var buttonTag = new TagBuilder("button");
                buttonTag.Attributes.Add("type", "button");
                buttonTag.Attributes.Add("class", "btn btn-secondary dropdown-toggle");
                buttonTag.Attributes.Add("data-bs-toggle", "dropdown");
                buttonTag.Attributes.Add("aria-expanded", "false");
                buttonTag.InnerHtml.Append(userIdentity.Name);

                var ulTag = new TagBuilder("ul");
                ulTag.Attributes.Add("class", "dropdown-menu");

                var profileItem = new TagBuilder("li");
                var profileLink = new TagBuilder("a");
                profileLink.Attributes.Add("class", "dropdown-item");
                if ((await _authenticatorService.GetRoleByIdAsync(int.Parse(userIdentity.Name))) == "Admin")
                {
                    profileLink.Attributes.Add("href", "/Admin/Home");
                }
                else
                {
                    profileLink.Attributes.Add("href", "/User/Index");
                }
                profileLink.InnerHtml.Append("Profile");
                profileItem.InnerHtml.AppendHtml(profileLink);
                ulTag.InnerHtml.AppendHtml(profileItem);

                var settingsItem = new TagBuilder("li");
                var settingsLink = new TagBuilder("a");
                settingsLink.Attributes.Add("class", "dropdown-item");
                settingsLink.Attributes.Add("href", "#");
                settingsLink.InnerHtml.Append("Settings");
                settingsItem.InnerHtml.AppendHtml(settingsLink);
                ulTag.InnerHtml.AppendHtml(settingsItem);

                var logoutItem = new TagBuilder("li");
                var logoutLink = new TagBuilder("a");
                logoutLink.Attributes.Add("class", "dropdown-item text-danger");
                logoutLink.Attributes.Add("href", "/Login/LogOut");
                logoutLink.InnerHtml.Append("Log out");
                logoutItem.InnerHtml.AppendHtml(logoutLink);
                ulTag.InnerHtml.AppendHtml(logoutItem);

                output.Content.AppendHtml(buttonTag);
                output.Content.AppendHtml(ulTag);
            }
            else
            {
                output.TagName = "a";
                output.Attributes.SetAttribute("asp-controller", "Account");
                output.Attributes.Add("href", "/Login/Index");
                output.Content.SetContent("Login");
                output.Attributes.Add("class", "btn btn-primary");

            }
        }
    }
}
