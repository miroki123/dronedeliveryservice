using DroneDeliveryService.Exception;
using System;
using System.Collections.Generic;

namespace DroneDeliveryService.Entity
{
    public class Squad
    {
        private IList<Drone> _drones;
        public IList<Drone> Drones
        {
            get => _drones;
        }

        public Squad(IList<Drone> drones)
        {
            _drones = drones;
        }
    }
}
