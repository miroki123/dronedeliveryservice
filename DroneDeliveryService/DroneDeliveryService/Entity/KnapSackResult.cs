using DroneDeliveryService.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DroneDeliveryService.Entity
{
    public class KnapSackResult<T> where T : IWeight
    {
        public List<T> Items { get; set; } = new List<T>();
        public int UnusedCapacity { get; set; }
    }
}
