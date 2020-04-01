using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Appweb.Models;
using Appweb.Views;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Data.Entity;
using Appweb.ViewModels;
using Korzh.EasyQuery.Linq;

namespace Appweb.Controllers
{
    public class UsersController : Controller
    {
        UserManager<User> _userManager;
        SignInManager<User> _signInManager;
        ApplicationContext _context;
        public UsersController(UserManager<User> userManager, ApplicationContext context, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }
        //[Authorize(Roles = "Admin")]
        public IActionResult Index() => View(_userManager.Users.ToList());
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index1()
        {
            var collection = _context.Collections.ToList();
            
            var id = _userManager.GetUserId(User);
            User user = await _userManager.FindByIdAsync(id);
            Collection col = new Collection { Name = "",Description="", CountItem=0,ImageUrl="", Plantb = false, Bookb = false, Phoneb = false, Carb = false, UserID = user.Id };
            foreach (Collection s in collection)
            {
                if (user.Collections == null)
                {                  
                    _context.Collections.Add(col);
                    _context.SaveChanges();
                   
                    user.Collections.Add(s);
                }
                else
                user.Collections.Add(s);
            }
           
            return View(user);
        }

        public async Task<IActionResult> Index2()
        {
            var collection = _context.Collections.ToList();
        
            var id = _userManager.GetUserId(User);
            User user = await _userManager.FindByIdAsync(id);
            foreach (Collection s in collection)
            {
                if (user.Collections == null)
                    return View(user);
                if (user.Id == s.UserID)
                    user.Collections.Add(s);
            }
            return View(user);
        }
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                DateTime a = new DateTime();
                a = DateTime.Now;
                User user = new User { Email = model.Email, UserName = model.Email, Name = model.Name, };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            EditUserViewModel model = new EditUserViewModel { Id = user.Id, Email = user.Email, Name = user.Name, };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;

                    user.Name = model.Name;


                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Note(NoteViewModel model)
        {
            string id = User.Identity.Name;
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByEmailAsync(id);
                if (user != null)
                {


                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index1");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }

        public IActionResult Note()
        {
            return View();
        }

        public async Task<ActionResult> PlantAdd(string id)
        {
            
            if (id == null)
            {
                return NotFound();
            }
            Collection col = _context.Collections.Find(id);
            var plant = _context.Plants.ToList();
            foreach (Plant s in plant)
            {
                if (s.CollectionID == id)
                {
                    col.Plants.Add(s);
                }
            }
            CollectionViewModel model = new CollectionViewModel { Bookb = col.Bookb,ImageUrl=col.ImageUrl,CountItem=col.CountItem, Carb = col.Carb, Name = col.Name, CollectionID = col.CollectionID, Phoneb = col.Phoneb, Plantb = col.Plantb,Description=col.Description, UserID = col.UserID, Plants = col.Plants };
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
            // return View();
        }
        [HttpPost]
        public async Task<ActionResult> PlantAdd(CollectionViewModel model,string id)
        {
            List<Plant> a = new List<Plant>();
            List<Plant> b = new List<Plant>();
           
            if (!string.IsNullOrEmpty(model.Text))
            {
                
                 a =_context.Plants.FullTextSearchQuery(model.Text).ToList();
                Tag tag = new Tag { CollectionID = id, Text = model.Text, Main = false };
                _context.Tags.Add(tag);
                _context.SaveChanges();
                    
            }
            else
            {
                a = _context.Plants.ToList();
            }
            
            foreach (Plant s in a)
            {
                if (s.CollectionID == id)
                {
                    b.Add(s);
                }
            }
            model.Plants = b;
            return View(model);
        }
        public async Task<ActionResult> BookAdd(string id)
        {

            if (id == null)
            {
                return NotFound();
            }
            Collection col = _context.Collections.Find(id);
            var book = _context.Books.ToList();
            foreach (Book s in book)
            {
                if (s.CollectionID == id)
                {
                    col.Books.Add(s);
                }
            }

            CollectionViewModel model = new CollectionViewModel { Bookb = col.Bookb, ImageUrl = col.ImageUrl, CountItem = col.CountItem, Carb = col.Carb, Name = col.Name, CollectionID = col.CollectionID, Phoneb = col.Phoneb, Plantb = col.Plantb, Description = col.Description, UserID = col.UserID, Books = col.Books };
            if (model== null)
            {
                return NotFound();
            }

            return View(model);

        }
        [HttpPost]
        public async Task<ActionResult> BookAdd(CollectionViewModel model, string id)
        {
            List<Book> a = new List<Book>();
            List<Book> b = new List<Book>();
            if (!string.IsNullOrEmpty(model.Text))
            {
                
                a = _context.Books.FullTextSearchQuery(model.Text).ToList();
                Tag tag = new Tag { CollectionID = id, Text = model.Text, Main = false };
                _context.Tags.Add(tag);
                _context.SaveChanges();
            }
            else
            {
                a = _context.Books.ToList();
            }
            foreach (Book s in a)
            {
                if (s.CollectionID == id)
                {
                   b.Add(s);
                }
            }
            model.Books = b;
            return View(model);
        }
        public async Task<ActionResult> PhoneAdd(string id)
        {

            if (id == null)
            {
                return NotFound();
            }
            Collection col = _context.Collections.Find(id);
            var phone = _context.Phones.ToList();
            foreach (Phone s in phone)
            {
                if (s.CollectionID == id)
                {
                    col.Phones.Add(s);
                }
            }

            CollectionViewModel model = new CollectionViewModel { Bookb = col.Bookb,CountItem=col.CountItem,ImageUrl=col.ImageUrl, Carb = col.Carb, Name = col.Name, CollectionID = col.CollectionID, Phoneb = col.Phoneb, Plantb = col.Plantb, Description = col.Description, UserID = col.UserID, Phones = col.Phones };
            if (model == null)
            {
                return NotFound();
            }

            return View(model);

        }
        [HttpPost]
        public async Task<ActionResult> PhoneAdd(CollectionViewModel model, string id)
        {
            List<Phone> a = new List<Phone>();
            List<Phone> b = new List<Phone>();
            if (!string.IsNullOrEmpty(model.Text))
            {
               
                a = _context.Phones.FullTextSearchQuery(model.Text).ToList();
                Tag tag = new Tag { CollectionID = id, Text = model.Text, Main = false };
                _context.Tags.Add(tag);
                _context.SaveChanges();
            }
            else
            {
                a = _context.Phones.ToList();
            }
            foreach (Phone s in a)
            {
                if (s.CollectionID == id)
                {
                    b.Add(s);
                }
            }
            model.Phones = b;
            return View(model);
        }
        public async Task<ActionResult> CarAdd(string id)
        {

            if (id == null)
            {
                return NotFound();
            }
            Collection col = _context.Collections.Find(id);
            var car = _context.Cars.ToList();
            foreach (Car s in car)
            {
                if (s.CollectionID == id)
                {
                    col.Cars.Add(s);
                }
            }

            CollectionViewModel model = new CollectionViewModel { Bookb = col.Bookb,CountItem=col.CountItem,ImageUrl=col.ImageUrl, Carb = col.Carb, Name = col.Name, CollectionID = col.CollectionID, Phoneb = col.Phoneb, Plantb = col.Plantb, Description = col.Description, UserID = col.UserID, Cars = col.Cars };
            if (model == null)
            {
                return NotFound();
            }

            return View(model);

        }
        [HttpPost]
        public async Task<ActionResult> CarAdd(CollectionViewModel model, string id)
        {
            List<Car> a = new List<Car>();
            List<Car> b = new List<Car>();
            if (!string.IsNullOrEmpty(model.Text))
            {
                a = _context.Cars.FullTextSearchQuery(model.Text).ToList();
                Tag tag = new Tag { CollectionID = id, Text = model.Text, Main = false };
                _context.Tags.Add(tag);
                _context.SaveChanges();
            }
            else
            {
                a = _context.Cars.ToList();
            }
            foreach (Car s in a)
            {
                if (s.CollectionID == id)
                {
                    b.Add(s);
                }
            }
            model.Cars = b;
            return View(model);
        }

        public async Task<ActionResult> DeleteCollection(string id)
        {
            Collection col = _context.Collections.Find(id);
            if (col.Plantb == true)
            {
                var a = _context.Plants.ToList();
                foreach (var s in a)
                {
                   if (s.CollectionID == id)
                        _context.Plants.Remove(s);
                }
            }
          
            _context.Collections.Remove(col);
            _context.SaveChanges();
            return RedirectToAction("Index1");
        }
        public IActionResult AddCollection() => View();

        [HttpPost]
        public async Task<IActionResult> AddCollection(CreateCollectionViewModel model, int Id)
        {
            if (ModelState.IsValid)
            {

                return RedirectToAction("Index1");

            }
            return View(model);
        }


        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);

            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditUserToCol()
        {

            var model = new List<UserColViewModel>();

            foreach (var user in _userManager.Users)
            {
                var userColViewModel = new UserColViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };


                userColViewModel.IsSelected = false;
                userColViewModel.Book = false;
                userColViewModel.Plant = false;
                userColViewModel.Phone = false;
                userColViewModel.Car = false;
                model.Add(userColViewModel);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserToCol(List<UserColViewModel> model)
        {

            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;

                if (model[i].IsSelected)
                {
                    Collection col = new Collection { Name = model[0].Name,Description=model[0].Description,CountItem=0, Plantb = model[0].Plant, Bookb = model[0].Book, Phoneb = model[0].Phone, Carb = model[0].Car, UserID = model[i].UserId };
                    _context.Collections.Add(col);
                    _context.SaveChanges();
                    return RedirectToAction("Index1");
                }



            }
            return RedirectToAction("Index1");
        }

        ///////////////////////////////////////////////////////////////////Book
        public async Task<IActionResult> EditBook(string id)
        {
            Book plant = _context.Books.Find(id);
            if (plant == null)
            {
                return NotFound();
            }
            EditBookViewModel model = new EditBookViewModel { BookID = plant.BookID,Type=plant.Type, Description = plant.Description, CollectionID = plant.CollectionID, Author = plant.Author, Name = plant.Name, };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditBook(EditBookViewModel model1)
        {
            if (ModelState.IsValid)
            {
                Book plant = _context.Books.Find(model1.BookID);
                if (plant != null)
                {

                    plant.Description = model1.Description;
                    plant.Author = model1.Author;
                    plant.Type = model1.Type;
                    plant.Name = model1.Name;


                    _context.Books.Update(plant);
                    _context.SaveChanges();
                    return RedirectToAction("BookAdd", new { Id = model1.CollectionID });


                }
            }
            return View(model1);
        }

        public IActionResult CreateBook() => View();

        [HttpPost]
        public async Task<IActionResult> CreateBook(CreateBookViewModel model1, string Id)
        {
            if (ModelState.IsValid)
            {

                Book plant = new Book { CollectionID = Id,Type=model1.Type, Description = model1.Description, Author = model1.Author, Name = model1.Name, };
                _context.Books.Add(plant);
                _context.SaveChanges();
                var a = _context.Collections.Find(plant.CollectionID);
                a.CountItem++;
                _context.Update(a);
                _context.SaveChanges();
                return RedirectToAction("BookAdd", new { Id = plant.CollectionID });

            }
            return View(model1);
        }

        public async Task<ActionResult> DeleteBook(string id)
        {
            Book plant = _context.Books.Find(id);
            if (plant != null)
            {
                _context.Books.Remove(plant);
                _context.SaveChanges();
            }
            var a = _context.Collections.Find(plant.CollectionID);
            a.CountItem--;
            _context.Update(a);
            _context.SaveChanges();
            return RedirectToAction("BookAdd", new { Id = plant.CollectionID });
        }
        ///////////////////////////////////////////////////////////////////Car
        public async Task<IActionResult> EditCar(string id)
        {
            Car car = _context.Cars.Find(id);
            if (car == null)
            {
                return NotFound();
            }
            EditCarViewModel model = new EditCarViewModel { CarID = car.CarID, Description = car.Description, CollectionID = car.CollectionID, Model = car.Model, Color = car.Color, Name = car.Name, };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditCar(EditCarViewModel model3)
        {
            if (ModelState.IsValid)
            {
                Car car = _context.Cars.Find(model3.CarID);
                if (car != null)
                {

                    car.Description = model3.Description;
                    car.Model = model3.Model;
                    car.Color = model3.Color;
                    car.Name = model3.Name;


                    _context.Cars.Update(car);
                    _context.SaveChanges();
                    return RedirectToAction("CarAdd", new { Id = model3.CollectionID });


                }
            }
            return View(model3);
        }

        public IActionResult CreateCar() => View();

        [HttpPost]
        public async Task<IActionResult> CreateCar(CreateCarViewModel model3, string Id)
        {
            if (ModelState.IsValid)
            {

                Car car = new Car { CollectionID = Id, Description = model3.Description, Color = model3.Color, Model = model3.Model, Name = model3.Name, };
                _context.Cars.Add(car);
                _context.SaveChanges();
                var a = _context.Collections.Find(car.CollectionID);
                a.CountItem++;
                _context.Update(a);
                _context.SaveChanges();
                return RedirectToAction("CarAdd", new { Id = car.CollectionID });

            }
            return View(model3);
        }

        public async Task<ActionResult> DeleteCar(string id)
        {
            Car car = _context.Cars.Find(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                _context.SaveChanges();
            }
            var a = _context.Collections.Find(car.CollectionID);
            a.CountItem--;
            _context.Update(a);
            _context.SaveChanges();
            return RedirectToAction("CarAdd", new { Id = car.CollectionID });
        }
        ///////////////////////////////////////////////////////////////////Phone
        public async Task<IActionResult> EditPhone(string id)
        {
            Phone phone = _context.Phones.Find(id);
            if (phone == null)
            {
                return NotFound();
            }
            EditPhoneViewModel model = new EditPhoneViewModel { PhoneID = phone.PhoneID, Description = phone.Description, CollectionID = phone.CollectionID, Model = phone.Model, Color = phone.Color, Name = phone.Name, };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditPhone(EditPhoneViewModel model2)
        {
            if (ModelState.IsValid)
            {
                Phone phone = _context.Phones.Find(model2.PhoneID);
                if (phone != null)
                {

                    phone.Description = model2.Description;
                    phone.Model = model2.Model;
                    phone.Color = model2.Color;
                    phone.Name = model2.Name;


                    _context.Phones.Update(phone);
                    _context.SaveChanges();
                    return RedirectToAction("PhoneAdd", new { Id = model2.CollectionID });


                }
            }
            return View(model2);
        }

        public IActionResult CreatePhone() => View();

        [HttpPost]
        public async Task<IActionResult> CreatePhone(CreatePhoneViewModel model2, string Id)
        {
            if (ModelState.IsValid)
            {

                Phone phone = new Phone { CollectionID = Id, Description = model2.Description, Color = model2.Color, Model = model2.Model, Name = model2.Name, };
                _context.Phones.Add(phone);
                _context.SaveChanges();
                var a = _context.Collections.Find(phone.CollectionID);
                a.CountItem++;
                _context.Update(a);
                _context.SaveChanges();
                return RedirectToAction("PhoneAdd", new { Id = phone.CollectionID });

            }
            return View(model2);
        }

        public async Task<ActionResult> DeletePhone(string id)
        {
            Phone phone = _context.Phones.Find(id);
            if (phone != null)
            {
                _context.Phones.Remove(phone);
                _context.SaveChanges();
            }
            var a = _context.Collections.Find(phone.CollectionID);
            a.CountItem--;
            _context.Update(a);
            _context.SaveChanges();
            return RedirectToAction("PhoneAdd", new { Id = phone.CollectionID });
        }
        ///////////////////////////////////////////////////////////////////Plant
        public async Task<IActionResult> EditPlant(string id)
        {
            Plant plant = _context.Plants.Find(id);
            if (plant == null)
            {
                return NotFound();
            }
            EditPlantViewModel model = new EditPlantViewModel { PlantID = plant.PlantID, Description = plant.Description, CollectionID = plant.CollectionID, Kind = plant.Kind, Color = plant.Color, Name = plant.Name, };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditPlant(EditPlantViewModel model)
        {
            if (ModelState.IsValid)
            {
                Plant plant = _context.Plants.Find(model.PlantID);
                if (plant != null)
                {

                    plant.Description = model.Description;
                    plant.Kind = model.Kind;
                    plant.Color = model.Color;
                    plant.Name = model.Name;


                    _context.Plants.Update(plant);
                    _context.SaveChanges();
                    return RedirectToAction("PlantAdd", new { Id = model.CollectionID });


                }
            }
            return View(model);
        }

        public IActionResult CreatePlant() => View();

        [HttpPost]
        public async Task<IActionResult> CreatePlant(CreatePlantViewModel model, string Id)
        {
            if (ModelState.IsValid)
            {

                Plant plant = new Plant { CollectionID = Id, Description = model.Description, Color = model.Color, Kind = model.Kind, Name = model.Name, };
                _context.Plants.Add(plant);
                _context.SaveChanges();
                var a = _context.Collections.Find(plant.CollectionID);
                a.CountItem++;
                _context.Update(a);
                _context.SaveChanges();
                return RedirectToAction("PlantAdd", new { Id = plant.CollectionID });

            }
            return View(model);
        }

        public async Task<ActionResult> DeletePlant(string id)
        {
            Plant plant = _context.Plants.Find(id);
            if (plant != null)
            {
                _context.Plants.Remove(plant);
                _context.SaveChanges();
            }
            var a = _context.Collections.Find(plant.CollectionID);
            a.CountItem--;
            _context.Update(a);
            _context.SaveChanges();
            return RedirectToAction("PlantAdd", new { Id = plant.CollectionID });
        }
        ////////////////////////////////////////////////////////////////////Comment
        public async Task<IActionResult> EditComment(string id)
        {
            var a = _context.Comments.ToList();
            List<Comment> b = new List<Comment>();
            List<User> c = new List<User>();
           
            CommentViewModel model = new CommentViewModel();
            foreach(Comment s in a)
            {
                if (s.ItemID == id)
                {
                    b.Add(s);
                    User user = await _userManager.FindByIdAsync(s.UserID);
                    c.Add(user);
                }
            }
            model.Comments = b;
            User user2 = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
            model.UserID = user2.Id;
            model.ItemID = id;
            model.Users = c;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditComment(CommentViewModel model,string id)
        {
            Comment model2 = new Comment { ItemID = id, UserID = _userManager.GetUserId(User), Text = model.Text };
            _context.Comments.Add(model2);
            _context.SaveChanges();
            var a = _context.Comments.ToList();
            List<Comment> b = new List<Comment>();
            List<User> c = new List<User>();
            foreach (Comment s in a)
            {
                if (s.ItemID == id)
                {
                    b.Add(s);
                    User user = await _userManager.FindByIdAsync(s.UserID);
                    c.Add(user);
                }
            }
            model.Comments = b;
            model.UserID = _userManager.GetUserId(User);
            model.ItemID = id;
            model.Users = c;
            return View(model);
        }
    }
}
