using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jungle.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public double Total { get; set; }
        public int TouristId { get; set; }
        public int ParkId { get; set; }
        public int SafariId { get; set; }
        public int VehicleId { get; set; }
        //public string Name { get; set; }
        //public TypeP Type { get; set; }
    }
}
