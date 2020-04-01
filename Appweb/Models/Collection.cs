using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Appweb.Models
{
    public class Collection
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CollectionID { get; set; }
        public string ImageUrl { get; set; }
        public int CountItem { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserID { get; set; }
        public bool? Plantb { get; set; }
        public bool? Bookb { get; set; }
        public bool? Carb { get; set; }
        public bool? Phoneb { get; set; }
        public ICollection<Book> Books { get; set; }
        public ICollection<Phone> Phones { get; set; }
        public ICollection<Car> Cars { get; set; }
        public ICollection<Plant> Plants { get; set; }
        public User Users { get; set; }
    }
}
