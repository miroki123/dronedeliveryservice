using DroneDeliveryService.Interface;

namespace DroneDeliveryService.Entity
{
    public class Location : IWeight
    {
        public string Name { get; set; }
        public int Weight { get; set; }
    }
}
