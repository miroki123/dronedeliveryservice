using DroneDeliveryService.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DroneDeliveryService.Interface
{
    public interface IKnapSackService<T> where T : IWeight
    {
        KnapSackResult<T> Solve(T[] items, int weightCapacity);
    }
}
