using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appweb.Domain.Core
{
   
        public class CreateUserViewModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string Name { get; set; }
            public string Mail { get; set; }
            public string Status { get; set; }
        public DateTime DateReg { get; set; }
        public DateTime DateLog { get; set; }

    }
        public class EditUserViewModel
        {
            public string Id { get; set; }
            public string Email { get; set; }
            public string Name { get; set; }
            public string Mail { get; set; }
            public string Status { get; set; }
        public DateTime DateReg { get; set; }
        public DateTime DateLog { get; set; }

    }
    
}
