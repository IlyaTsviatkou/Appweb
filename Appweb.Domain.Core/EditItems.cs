using Appweb.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appweb.Domain.Core
{
    public class CreateCollectionViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CountItem { get; set; }
        public string ImageUrl { get; set; }
        public string UserID { get; set; }
        public bool? Plantb { get; set; }
        public bool? Bookb { get; set; }
        public bool? Carb { get; set; }
        public bool? Phoneb { get; set; }
    }
    public class CollectionViewModel
    {
        public string CollectionID { get; set; }
        public string Name { get; set; }
        public string UserID { get; set; }
        public string ImageUrl { get; set; }
        public int CountItem { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public List<string?> Type { get; set; }

        public ICollection<Item> Items { get; set; }
    }
    public class CommentViewModel
    {
        public List<Comment> Comments { get; set; }
        public string UserName { get; set; }
        public string UserID { get; set; }
        public string ItemID { get; set; }
        public string Text { get; set; }

        public string CollectionID { get; set; }
    }
    public class ItemTagCol
    {
        public List<Tag> Tag { get; set; }
        public List<Collection> Collection { get; set; }

    }
    public class Items
    {
        public List<Item> item { get; set; }

    }
    public class CreateItemViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CollectionID { get; set; }
        public List<string?> Field { get; set; }

        public List<string?> Type { get; set; }


    }
    public class EditItemViewModel
    {
        public string ItemID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CollectionID { get; set; }
        public List<string?> Field { get; set; }

        public List<string?> Type { get; set; }


    }
}
