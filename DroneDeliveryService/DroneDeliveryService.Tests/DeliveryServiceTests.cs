using DroneDeliveryService.Entity;
using DroneDeliveryService.Exception;
using DroneDeliveryService.Interface;
using DroneDeliveryService.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DroneDeliveryService.Tests
{
    [TestClass]
    public class DeliveryServiceTests
    {
        IDeliveryService _deliveryService;

        public DeliveryServiceTests()
        {
            _deliveryService = new DeliveryService();
        }

        [TestMethod]
        public void CreateSquadWith100Drones_Success()
        {
            StringBuilder sb = new StringBuilder();
            string[] input = new string[1];
            for (int i = 0; i < 100; i++)
            {
                if (i > 0) sb.Append(",");
                sb.Append("Drone" + i + ", 50");
            }
            input[0] = sb.ToString();
            var squad = _deliveryService.GenerateSquadFromInputData(input);

            Assert.AreEqual(100, squad.Drones.Count);
        }

        [TestMethod]
        public void CreateSquadWith101Drones_ThrowException()
        {
            StringBuilder sb = new StringBuilder();
            string[] input = new string[1];
            for (int i = 0; i < 101; i++)
            {
                if (i > 0) sb.Append(",");
                sb.Append("Drone" + i + ", 50");
            }
            input[0] = sb.ToString();
            Assert.ThrowsException<MaximumSquadSizeReachedException>(() => _deliveryService.GenerateSquadFromInputData(input));
        }

        [TestMethod]
        public void CreateSquadWithBadNumberFormatting_ThrowFormatException()
        {
            StringBuilder sb = new StringBuilder();
            string[] input = new string[1];
            for (int i = 0; i < 100; i++)
            {
                if (i > 0) sb.Append(",");
                sb.Append("Drone" + i + ", abc");
            }
            input[0] = sb.ToString();
            Assert.ThrowsException<FormatException>(() => _deliveryService.GenerateSquadFromInputData(input));
        }

        [TestMethod]
        public void CreateDeliveryWith100Locations_Success()
        {
            string[] input = new string[101];
            input[0] = string.Empty;
            for (int i = 1; i < 101; i++)
            {
                input[i] = "Location" + i + ", 50";
            }

            var deliveries = _deliveryService.GenerateDeliveryFromInputData(input);

            Assert.AreEqual(100, deliveries.Locations.Count);
        }

        [TestMethod]
        public void CalculateDelivery_2Drones_2Locations_1Trip_Success()
        {
            var squad = new Squad(new List<Drone> { new Drone { Name = "D1", MaximumWeight = 50 }, new Drone { Name = "D2", MaximumWeight = 100 } });
            var delivery = new Delivery(new List<Location> { new Location { Name = "L1", Weight = 25 }, new Location { Name = "L2", Weight = 75 } });

            var result = _deliveryService.CalculateDeliveries(squad, delivery);

            Assert.AreEqual(1, result.DeliveriesPerDrone.Count);
            var drone = result.DeliveriesPerDrone.First().Key;
            Assert.AreEqual("D2", drone.Name);
        }

        [TestMethod]
        public void CalculateDelivery_2Drones_3Locations_2Trips_Success()
        {
            var squad = new Squad(new List<Drone> { 
                new Drone { Name = "D1", MaximumWeight = 50 }, 
                new Drone { Name = "D2", MaximumWeight = 100 } 
            });

            var delivery = new Delivery(new List<Location> { 
                new Location { Name = "L1", Weight = 25 }, 
                new Location { Name = "L2", Weight = 75 },
                new Location { Name = "L3", Weight = 50 },
            });

            var result = _deliveryService.CalculateDeliveries(squad, delivery);

            Assert.AreEqual(2, result.DeliveriesPerDrone.Count);
            var drone = result.DeliveriesPerDrone.First().Key;
            Assert.AreEqual("D2", drone.Name);

            var otherDrone = result.DeliveriesPerDrone.Last().Key;
            Assert.AreEqual("D1", otherDrone.Name);
        }
    }
}
