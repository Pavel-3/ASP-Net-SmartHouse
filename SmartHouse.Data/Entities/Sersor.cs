using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHouse.Data.Entities
{
    public class Sensor : Device // smoke sensor, gas sensor, leakage sensor, various security detectors ... 
    {
        private bool? _value;
        [Column("Value")]
        public bool? Value
        {
            get { return _value; }
            private set { _value = value; }
        }
    }
}
