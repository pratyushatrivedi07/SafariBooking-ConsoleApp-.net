using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jungle.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public int ParkId { get; set; }
        public int SafariId { get; set; }
        public int GateId { get; set; }
        public int VehicleId { get; set; }
        public int TouristId { get; set; }
        public string PName { get; set; }
        public string SName { get; set; }
        public string Vname { get; set; }
        public String TName { get; set; }
        public string VType { get; set; }
        public DateTime Date { get; set; }
        //public int PayId { get; set; }
        public double TotalCost { get; set; }
        //public double TotalCost { get; set; }

        public string Status { get; set; }
    }
}
