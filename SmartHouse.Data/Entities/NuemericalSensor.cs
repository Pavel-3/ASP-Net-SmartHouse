using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHouse.Data.Entities
{
    public class NuemericalSensor : Device // temperature sensor, humidity sensor, light sensor ...
    {
        private float? _value;
        [Column("Value")]
        public float? Value
        {
            get { return _value; }
            private set { _value = value; }
        }
    }

}
