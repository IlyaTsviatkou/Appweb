using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace Appweb.Models
{
    public class Plant
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string PlantID { get; set; }
        public string CollectionID { get; set; }
        
        public string Name { get; set; }
        
        public string Kind { get; set; }
        
        public string Color { get; set; }
        
        public string Description { get; set; }
        public Collection Collections { get; set; }

    }
}
