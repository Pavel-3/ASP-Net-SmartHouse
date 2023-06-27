using Azure.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using SmartHouse.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace SmartHouse.MVC.Models
{
    public class CreateUserModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [ValidateNever]
        public List<RoomModel>? Rooms { get; set; }
        [ValidateNever]
        public List<DeviceModel>? Devices { get; set; }
    }
}
