using System;
using System.Collections.Generic;
using System.Text;

namespace DroneDeliveryService.Entity
{
    public class Result
    {
        public Dictionary<Drone, IList<Delivery>> DeliveriesPerDrone { get; set; } = new Dictionary<Drone, IList<Delivery>>();
    }
}
