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
        public string Theme { get; set; }
        public string? Type1 { get; set; }
        public string? Type2 { get; set; }
        public string? Type3 { get; set; }
        public string? Type4 { get; set; }
        public string? Type5 { get; set; }
        public string? Type6 { get; set; }
        public string? Type7 { get; set; }
        public string? Type8 { get; set; }
        public string? Type9 { get; set; }

        public ICollection<Item> Items { get; set; }
        public User Users { get; set; }
    }
}
