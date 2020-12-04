using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Appweb.Domain.Core
{
    public class Like
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string LikeID { get; set; }
        public string UserID { get; set; }
        public string ItemID { get; set; }
        public int Count { get; set; }
    }
}
