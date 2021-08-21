using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jungle.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }
        public vType VehicleType { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
        public Capacity capacity { get; set; }
        public int ParkId { get; set; }
    }
}
