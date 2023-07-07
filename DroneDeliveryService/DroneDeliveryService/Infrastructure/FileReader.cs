using DroneDeliveryService.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DroneDeliveryService.Infrastructure
{
    public class FileReader : IFileReader
    {
        public string[] ReadFile(string filename)
        {
            return File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + filename);
        }
    }
}
