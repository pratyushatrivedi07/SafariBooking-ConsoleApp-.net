using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jungle.Entities
{
    public class Gate
    {
        public int GateId { get; set; }
        public string Name { get; set; }
        public int ParkId { get; set; }

        public override string ToString()
        {
            return $"\n{GateId}\t{Name}\t{ParkId}";
        }
    }
}
