using DroneDeliveryService.Infrastructure;
using DroneDeliveryService.Interface;
using DroneDeliveryService.Service;
using System;
using System.Linq;

namespace DroneDeliveryService
{
    class Program
    {
        static void Main(string[] args)
        {
            IFileReader reader = new FileReader();
            var input = reader.ReadFile("..\\..\\..\\input.txt");

            IDeliveryService deliveryService = new DeliveryService();
            var worksheet = deliveryService.GenerateWorksheet(input);

            // print results
            foreach (var row in worksheet.Result.DeliveriesPerDrone)
            {
                Console.WriteLine(row.Key.Name);
                int i = 1;
                foreach (var trip in row.Value)
                {
                    Console.WriteLine("Trip #" + i++);
                    string[] tripString = trip.Locations.Select(x => x.Name).ToArray();
                    Console.WriteLine(string.Join(", ", tripString));
                }
                Console.WriteLine(string.Empty);
            }

        }
    }
}
