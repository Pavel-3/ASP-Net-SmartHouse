using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse.Core.DTOs
{
    public class DeviceDTOWithValue : DeviceDTO
    {
        public object Value { get; set; }
    }

}
