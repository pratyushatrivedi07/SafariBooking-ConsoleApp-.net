using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jungle.Entities
{
    public class Parks
    {
        public int ParkId { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
        public double Fee { get; set; }

        public override string ToString()
        {
            return $"\n{ParkId}\t{Name}\t{Location}";
        }
    }
}
