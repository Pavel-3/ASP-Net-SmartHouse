using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SmartHouse.MVC.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="You should enter  a ID")]
        [Remote("IsIdExists",
        "Login",
        ErrorMessage = "Id not found")]
        public int Id { get; set; }
        [Required]
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
