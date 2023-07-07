using DroneDeliveryService.Entity;
using DroneDeliveryService.Exception;
using DroneDeliveryService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DroneDeliveryService.Service
{
    public class DeliveryService : IDeliveryService
    {
        IKnapSackService<Location> _knapSackService;

        public DeliveryService()
        {
            _knapSackService = new KnapSackService<Location>();
        }

        /// <summary>
        /// the drone input line has the following pattern: [DroneA], [WeightA], [DroneB], [WeightB], etc;
        /// therefore, we are going to create the drone entities following that pattern
        /// if any unexpected value shows up, an exception will be thrown
        /// if the number of drones exceeds 100, we throw a MaximumSquadSizeReachedException
        /// </summary>
        /// <param name="input">a line containing all the drones</param>
        /// <returns>a Squad instance</returns>
        public Squad GenerateSquadFromInputData(string[] inputData)
        {
            var drones = new List<Drone>();

            // the first line is the drone data
            var droneTxtArray = inputData[0].Split(",");

            for (int i = 0; i < droneTxtArray.Length; i += 2)
            {
                string name = droneTxtArray[i].Trim();
                int weight = Convert.ToInt32(droneTxtArray[i + 1].Trim());
                Drone drone = new Drone { Name = name, MaximumWeight = weight };
                if (drones.Count >= 100)
                    throw new MaximumSquadSizeReachedException();

                drones.Add(drone);
            }
            return new Squad(drones);
        }

        private Location GenerateLocationFromInputData(string input)
        {
            
            var locationTxtArray = input.Split(",");
            string name = locationTxtArray[0].Trim();
            int packageWeight = Convert.ToInt32(locationTxtArray[1].Trim());
            return new Location { Name = name, Weight = packageWeight };
        }

        /// <summary>
        /// the location input line has the following pattern:
        /// <para>line 1 [LocationA], [PackageWeightA] </para>
        /// <para>line 2 [LocationB], [PackageWeightB]</para>
        /// etc;
        /// therefore, we are going to create the location entities following that pattern
        /// if any unexpected value shows up, an exception will be thrown
        /// </summary>
        /// <param name="inputData">the array with all the data from the file</param>
        /// <returns></returns>
        public Delivery GenerateDeliveryFromInputData(string[] inputData)
        {
            var locations = new List<Location>();
            bool skipFirstLine = true;
            foreach (var line in inputData)
            {
                if (skipFirstLine)
                {
                    skipFirstLine = false;
                    continue;
                }
                var location = GenerateLocationFromInputData(line);
                locations.Add(location);
            }
            return new Delivery(locations);
        }


        public Worksheet GenerateWorksheet(string[] inputData)
        {
            var squad = GenerateSquadFromInputData(inputData);
            var delivery = GenerateDeliveryFromInputData(inputData);
            var result = CalculateDeliveries(squad, delivery);
            return new Worksheet { Squad = squad, Delivery = delivery, Result = result };
        }

        /// <summary>
        /// to calculate the deliveries we are going to use the Knapsack algorithm
        /// </summary>
        /// <param name="squad"></param>
        /// <param name="delivery"></param>
        /// <returns></returns>
        public Result CalculateDeliveries(Squad squad, Delivery delivery)
        {
            Result result = new Result();

            // clone the original list to keep the original safe
            var locations = delivery.Locations.ToList();

            while (locations.Count > 0)
            {
                var solutions = new Dictionary<Drone, KnapSackResult<Location>>();
                foreach (var drone in squad.Drones)
                {
                    var solution = _knapSackService.Solve(locations.ToArray(), drone.MaximumWeight);
                    solutions.Add(drone, solution);
                }

                // order the solutions by order of items then the least unused capacity
                var orderedSolutions = solutions
                    .OrderByDescending(x => x.Value.Items.Count)
                    .ThenBy(y => y.Value.UnusedCapacity)
                    .ToList();

                // pick the best solution
                var trip = new Delivery(orderedSolutions[0].Value.Items);
                if (result.DeliveriesPerDrone.TryGetValue(orderedSolutions[0].Key, out var deliveries))
                    deliveries.Add(trip);
                else
                    result.DeliveriesPerDrone.Add(orderedSolutions[0].Key, new List<Delivery> { trip });

                // remove the locations from the list
                foreach (var location in orderedSolutions[0].Value.Items)
                {
                    locations.Remove(location);
                }
            }

            return result;
        }
    }
}
