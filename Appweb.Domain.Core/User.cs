using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Appweb.Domain.Core
{
    public class User : IdentityUser
    {
        
        public DateTime DateReg { get; set; }
        public DateTime DateLog { get; set; }
        public string Name { get; set; }

        
        public ICollection<Collection> Collections { get; set; }

       // public virtual ICollection<Item> Purchases { get; set; }
       public List<UserItem> UserItems { get; set; }
        public User()
        {
            this.UserItems = new List<UserItem>();
        }



    }
}
 