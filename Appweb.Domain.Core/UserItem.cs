using System;
using System.Collections.Generic;
using System.Text;

namespace Appweb.Domain.Core
{
    public class UserItem
    {
        public string ItemId { get; set; }
        public Item Item { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
