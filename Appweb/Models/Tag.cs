using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Appweb.Models
{
    public class Tag
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string TagID { get; set; }
        public string Text { get; set; }
        public string CollectionID { get; set; }
        public bool Main { get; set; }
    }
}
