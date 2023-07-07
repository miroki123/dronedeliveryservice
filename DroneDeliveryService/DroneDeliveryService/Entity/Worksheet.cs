using System;
using System.Collections.Generic;
using System.Text;

namespace DroneDeliveryService.Entity
{
    /// <summary>
    /// this entity has all the information required to calculate the deliveries
    /// as well as the result object from the calculation
    /// </summary>
    public class Worksheet
    {
        public Squad Squad { get; set; }
        public Delivery Delivery { get; set; }
        public Result Result { get; set; }
    }
}
