using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Appweb.Domain.Core
{
    public class Item
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ItemID { get; set; }
        public string CollectionID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Field1 { get; set; }
        public string? Field2 { get; set; }

        public ICollection<Like> Likes { get; set; }
        public Collection Collections { get; set; }

        public List<UserItem> UserItems { get; set; }
       // public virtual ICollection<User> Users { get; set; }
        public Item()
        {
            this.UserItems = new List<UserItem>();
        }



    }
}
