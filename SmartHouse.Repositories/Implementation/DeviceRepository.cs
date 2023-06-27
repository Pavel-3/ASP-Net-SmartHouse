using Microsoft.EntityFrameworkCore;
using SmartHouse.Abstractions.Data.Repositories;
using SmartHouse.Data;
using SmartHouse.Data.Entities;

namespace SmartHouse.Repositories.Implementation
{
    public class DeviceRepository : Repository<Device>, IDeviceRepository
    {
        public DeviceRepository(HouseContext dbcontext) : base(dbcontext) { }
        public async Task<int> AddAsync(Device device)
        {
            switch (device)
            {
                case Sensor Sensor:
                    return (await DbContext.Sensors.AddAsync(Sensor)).Entity.Id;
                case NuemericalSensor nuemericalSensor:
                    return (await DbContext.NuemericalSensors.AddAsync(nuemericalSensor)).Entity.Id;
                case FeedbackDevice feedbackDevice:
                    return (await DbContext.FeedbackDevices.AddAsync(feedbackDevice)).Entity.Id;
                case NuemericalFeedbackDevice nuemericalFeedbackDevice:
                    return (await DbContext.NuemericalFeedbackDevices.AddAsync(nuemericalFeedbackDevice)).Entity.Id;
                default:
                    throw new ArgumentException();
            }
        }
        public override async Task AddRangeAsync(IEnumerable<Device?> entities)
        {
            if (entities.Count() == 0)
                return;
            var sensors = new List<Sensor>();
            var nuemericalSensors = new List<NuemericalSensor>();
            var feedbackDevices = new List<FeedbackDevice>();
            var nuemericalFeedbackDevices = new List<NuemericalFeedbackDevice>();
            foreach (var entity in entities)
            {
                switch (entity)
                {
                    case Sensor Sensor:
                        sensors.Add(Sensor);
                        break;
                    case NuemericalSensor nuemericalSensor:
                        nuemericalSensors.Add(nuemericalSensor);
                        break;
                    case FeedbackDevice feedbackDevice:
                        feedbackDevices.Add(feedbackDevice);
                        break;
                    case NuemericalFeedbackDevice nuemericalFeedbackDevice:
                        nuemericalFeedbackDevices.Add(nuemericalFeedbackDevice);
                        break;
                    default:
                        throw new ArgumentException();
                }
            }
            await DbContext.Sensors.AddRangeAsync(sensors);
            await DbContext.NuemericalSensors.AddRangeAsync(nuemericalSensors);
            await DbContext.FeedbackDevices.AddRangeAsync(feedbackDevices);
            await DbContext.NuemericalFeedbackDevices.AddRangeAsync(nuemericalFeedbackDevices);
        }
        public async Task<IEnumerable<Device>> GetDeviceByPageAsync(int currentPage, int pageSize)
        {
            var result = await DbSet
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return result;
        }
    }
}