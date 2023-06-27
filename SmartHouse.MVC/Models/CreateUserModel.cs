using Azure.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using SmartHouse.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace SmartHouse.MVC.Models
{
    public class CreateUserModel
    {
        [Required]
        [StringLength(500, MinimumLength = 1, ErrorMessage ="Name should be between 1 to 500 symbols")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        //[ValidateNever]
        //public List<RoomModel>? Rooms { get; set; }
        [ValidateNever]
        public List<DeviceModel>? Devices { get; set; }
    }
}
