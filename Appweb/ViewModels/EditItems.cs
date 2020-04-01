using Appweb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appweb.ViewModels
{
    public class EditPlantViewModel
    {
        public string PlantID { get; set; }
        public string CollectionID { get; set; }
        public string Name { get; set; }
        public string Kind { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }

    }
    public class CreatePlantViewModel
    {
        public string CollectionID { get; set; }
        public string Name { get; set; }
        public string Kind { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
    }
    public class EditPhoneViewModel
    {
        public string PhoneID { get; set; }
        public string CollectionID { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
    }
    public class CreatePhoneViewModel
    {
        public string CollectionID { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
    }
    public class EditCarViewModel
    {
        public string CarID { get; set; }
        public string CollectionID { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
    }
    public class CreateCarViewModel
    {
        public string CollectionID { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
    }
    public class EditBookViewModel
    {
        public string BookID { get; set; }
        public string CollectionID { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }

        public string Description { get; set; }
    }
    public class CreateBookViewModel
    {
        public string CollectionID { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }
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
        public bool? Plantb { get; set; }
        public bool? Bookb { get; set; }
        public bool? Carb { get; set; }
        public bool? Phoneb { get; set; }
        public string Text { get; set; }
        public ICollection<Book> Books { get; set; }
        public ICollection<Phone> Phones { get; set; }
        public ICollection<Car> Cars { get; set; }
        public ICollection<Plant> Plants { get; set; }
    }
    public class CommentViewModel
    {
        public List<Comment> Comments { get; set; }
        public List<User> Users { get; set; }
        public string UserID { get; set; }
        public string ItemID { get; set; }
        public string Text { get; set; }
    }
    public class ItemTagCol
    {
        public List<Tag> Tag { get; set; }
        public List<Collection> Collection { get; set; }
        public Plant Plant { get; set; }
        public Book Book { get; set; }
        public Phone Phone { get; set; }
        public Car Car { get; set; }
    }

}
