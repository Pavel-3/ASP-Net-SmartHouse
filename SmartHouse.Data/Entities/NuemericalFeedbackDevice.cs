using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHouse.Data.Entities
{
    public class NuemericalFeedbackDevice : Device //setting the desired temperature and humidity in a room, electric kettle with temperature control ...
    {
        public float? Value { get; set; }
    }

}
