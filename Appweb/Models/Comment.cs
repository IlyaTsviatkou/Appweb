using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Appweb.Models
{
    public class Comment
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CommentID { get; set; }
        public string Text { get; set; }
        public string UserID { get; set; }
        public string ItemID { get; set; }
    }
}
