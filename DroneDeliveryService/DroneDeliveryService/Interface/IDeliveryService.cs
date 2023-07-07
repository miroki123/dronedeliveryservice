using DroneDeliveryService.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DroneDeliveryService.Interface
{
    public interface IDeliveryService
    {
        Squad GenerateSquadFromInputData(string[] inputData);
        Delivery GenerateDeliveryFromInputData(string[] inputData);
        Worksheet GenerateWorksheet(string[] inputData);
        Result CalculateDeliveries(Squad squad, Delivery delivery);
    }
}
