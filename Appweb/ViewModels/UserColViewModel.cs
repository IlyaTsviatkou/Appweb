using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appweb.ViewModels
{
    public class UserColViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CountItem { get; set; }
        public bool Plant { get; set; }
        public bool Book { get; set; }
        public bool Car { get; set; }
        public bool Phone { get; set; }
        public bool IsSelected { get; set; }
    }
}
