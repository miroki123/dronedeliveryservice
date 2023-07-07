using System;
using System.Collections.Generic;
using System.Text;

namespace DroneDeliveryService.Entity
{
    public class Delivery
    {
        private IList<Location> _locations;
        public IList<Location> Locations
        {
            get => _locations;
        }

        public Delivery(IList<Location> locations)
        {
            _locations = locations;
        }
    }
}
