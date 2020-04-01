using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Appweb.Models
{
    public class User : IdentityUser
    {
        
        public DateTime DateReg { get; set; }
        public DateTime DateLog { get; set; }
        public string Name { get; set; }

        
        public ICollection<Collection> Collections { get; set; }
      
        /*public List<Book> Books { get; set; }
        public User()
        {
            Books = new List<Book>();
        }*/
    }
}
 
