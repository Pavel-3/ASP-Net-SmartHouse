using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse.Core.DTOs
{
    public class AdminDTO
    {
        public int Id { get; set; }
        public string PasswordHash { get; set; }
    }
}
