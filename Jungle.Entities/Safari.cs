using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jungle.Entities
{
    public class Safari
    {
        public int SafariId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public slot SafariTime { get; set; }
        public double Cost { get; set; }
        public int ParkId { get; set; }
    }
}

