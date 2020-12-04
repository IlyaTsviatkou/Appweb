using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appweb.Domain.Core
{
    public class PlantViewModel
    {
        public int PlantID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Kind { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
      
    }
}
