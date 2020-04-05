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
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Appweb.Controllers
{
    public class UsersController : Controller
    {
        UserManager<User> _userManager;
        SignInManager<User> _signInManager;
        ApplicationContext _context;
        IWebHostEnvironment _appEnvironment;

        public UsersController(UserManager<User> userManager, ApplicationContext context, SignInManager<User> signInManager, IWebHostEnvironment appEnvironment)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index() => View(_userManager.Users.ToList());

        public async Task<IActionResult> Index1()
        {
            var collection = _context.Collections.ToList();

            var id = _userManager.GetUserId(User);
            User user = await _userManager.FindByIdAsync(id);

            Collection col = new Collection { Name = "", Description = "", CountItem = 0, ImageUrl = "", UserID = "" };
            if (user != null)
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
            else
            {
                User user2 = new User();
                user2.Collections = collection;
                return View(user2);
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
        public IActionResult ToCollction() => View();
        [HttpPost]
        public IActionResult ToCollection(string id)
        {
            var a = _context.Collections.Find(id);

            return NotFound();
        }
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


        public async Task<ActionResult> DeleteCollection(string id)
        {
            Collection col = _context.Collections.Find(id);


            var a = _context.Items.ToList();
            var b = _context.Likes.ToList();

            var c = _context.Items.Where(cc => cc.CollectionID == id).ToList();
            foreach (var it in c)
            {
                var cc = _context.Likes.Where(x => x.ItemID == it.ItemID).ToList();
                it.Likes = cc;

            }
            col.Items = c;
            // _context.Items.Remove(col);
            // _context.Items.Remove(s);


            
            foreach (var it in c)
            {
                foreach (var itt in it.Likes)
                    _context.Likes.Remove(itt);
                _context.Items.Remove(it);
                
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
                    Collection col = new Collection { Name = model[0].Name, Description = model[0].Description, CountItem = 0, UserID = model[i].UserId };
                    _context.Collections.Add(col);
                    _context.SaveChanges();
                    return RedirectToAction("Index1");
                }



            }
            return RedirectToAction("Index1");
        }



        ////////////////////////////////////////////////////////////////////Comment
        public async Task<IActionResult> EditComment(string id)
        {
            var a = _context.Comments.ToList();
            List<Comment> b = new List<Comment>();


            CommentViewModel model = new CommentViewModel();
            foreach (Comment s in a)
            {
                if (s.ItemID == id)
                {
                    b.Add(s);

                }
            }

            var sortedUsers = from u in b
                              orderby u.Count
                              select u;


            b = sortedUsers.ToList();



            model.Comments = b;
            model.Comments = b;
            User user2 = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
            model.UserID = user2.Id;
            model.ItemID = id;
            model.UserName = user2.UserName;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditComment(CommentViewModel model, string id)
        {
            Comment model2 = new Comment { ItemID = id, UserID = _userManager.GetUserId(User), Text = model.Text };

            var a = _context.Comments.ToList();
            List<Comment> b = new List<Comment>();

            foreach (Comment s in a)
            {
                if (s.ItemID == id)
                {
                    b.Add(s);
                }
            }
            if (b.Count != 0)
            {
                var sortedUsers = from u in b
                                  orderby u.Count
                                  select u;

                var ss = sortedUsers.Last();
                int L = Convert.ToInt32(ss.Count);
                L++;
                model2.Count = Convert.ToString(L);
                _context.Comments.Add(model2);
                _context.SaveChanges();
                b = sortedUsers.ToList();
                b.Add(model2);
            }
            else
            {
                model2.Count = "1";
                _context.Comments.Add(model2);
                _context.SaveChanges();
                b.Add(model2);
            }


            model.Comments = b;
            model.UserID = _userManager.GetUserId(User);
            model.ItemID = id;
            var user = await _userManager.FindByIdAsync(model.UserID);
            model.UserName = user.UserName;


            return View(model);
        }

        ////////////////////////////////////////////////////////////////////ColToItem
        [HttpGet]
        public async Task<IActionResult> CreateNewCol()
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
        public async Task<IActionResult> CreateNewCol(List<UserColViewModel> model, string field1, IFormFile uploadedFile, string field2, string Theme, string field3, string field4, string field5, string field6, string field7, string field8, string field9)
        {
            string path = "";
            if (uploadedFile != null)
            {
                // путь к папке Files
                path = "/Files/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }


            }
            else
            {
                path = "/Files/notfound.jpg";
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;

                if (model[i].IsSelected)
                {
                    Collection col = new Collection
                    {
                        Type1 = field1,
                        Type2 = field2,
                        Type3 = field3,
                        Type4 = field4,
                        Type5 = field5,
                        Type6 = field6,
                        Type7 = field7,
                        Type8 = field8,
                        Type9 = field9,
                        Theme = Theme,
                        ImageUrl = path,
                        Name = model[0].Name,
                        Description = model[0].Description,
                        CountItem = 0,

                        UserID = model[i].UserId
                    };
                    _context.Collections.Add(col);
                    _context.SaveChanges();
                    return RedirectToAction("Index1");
                }



            }
            return RedirectToAction("Index1");
        }

        public IActionResult CreateItem(string id)
        {
            try
            {
                List<string> aa = new List<string>();

                CreateItemViewModel a = new CreateItemViewModel();
                var b = _context.Collections.Find(id);

                aa.Add(b.Type1);

                aa.Add(b.Type2);

                aa.Add(b.Type3);

                aa.Add(b.Type4);

                aa.Add(b.Type5);

                aa.Add(b.Type6);

                aa.Add(b.Type7);

                aa.Add(b.Type8);

                aa.Add(b.Type9);
                a.Type = aa;

                return View(a);
            }
            catch
            {
                return NotFound();
            }

        }
        [HttpPost]
        public async Task<IActionResult> CreateItem(CreateItemViewModel model1, string Id, string field1, string field2, string field3, string field4, string field5, string field6, string field7, string field8, string field9)
        {
            if (ModelState.IsValid)
            {
                Item item = new Item { CollectionID = Id };

                item.Field1 = field1;

                item.Field2 = field2;

                item.Field3 = field3;

                item.Field4 = field4;

                item.Field5 = field5;

                item.Field6 = field6;

                item.Field7 = field7;

                item.Field8 = field8;

                item.Field9 = field9;



                item.Name = model1.Name;
                item.Description = model1.Description;
                _context.Items.Add(item);
                _context.SaveChanges();
                var v = _context.Collections.Find(item.CollectionID);
                v.CountItem++;
                _context.Update(v);
                _context.SaveChanges();
                return RedirectToAction("Collections", new { Id = item.CollectionID });

            }
            return View(model1);
        }
        public async Task<ActionResult> Collections(string id)
        {

            if (id == null)
            {
                return NotFound();
            }
            Collection col = _context.Collections.Find(id);
            var l = _context.Likes.ToList();
            var plant = _context.Items.ToList();
            List<Like> Likes = new List<Like>();
            var items = _context.Items.Where(i => i.CollectionID == id).ToList();
            foreach (var it in items)
            {
                var likes = _context.Likes.Where(x => x.ItemID == it.ItemID).ToList();
                it.Likes = likes;
                col.Items.Add(it);
            }

            List<string> As = new List<string>();
            As.Add(col.Type1);
            As.Add(col.Type2);
            As.Add(col.Type3);
            As.Add(col.Type4);
            As.Add(col.Type5);
            As.Add(col.Type6);
            As.Add(col.Type7);
            As.Add(col.Type8);
            As.Add(col.Type9);
            CollectionViewModel model = new CollectionViewModel { ImageUrl = col.ImageUrl, Type = As, CountItem = col.CountItem, Name = col.Name, CollectionID = col.CollectionID, Description = col.Description, UserID = col.UserID, Items = col.Items };
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
            // return View();
        }

        [HttpPost]
        public async Task<ActionResult> Collections(CollectionViewModel model, string id)
        {
            List<Item> a = new List<Item>();
            List<Item> b = new List<Item>();
            List<Comment> c = new List<Comment>();
            Collection col = _context.Collections.Find(id);
            if (!string.IsNullOrEmpty(model.Text))
            {

                a = _context.Items.FullTextSearchQuery(model.Text).ToList();
                c = _context.Comments.FullTextSearchQuery(model.Text).ToList();
                Tag tag = new Tag { CollectionID = id, Text = model.Text, Main = false };
                var tags = _context.Tags.Where(t => t.Text == tag.Text).FirstOrDefault();
                if (tags == null)
                {
                    _context.Tags.Add(tag);
                    _context.SaveChanges();
                }

            }
            else
            {
                a = _context.Items.ToList();
            }

            foreach (Item s in a)
            {
                if (s.CollectionID == id)
                {
                    b.Add(s);
                }
            }
            model.Items = b;
            foreach (Comment com in c)
            {
                var x = _context.Items.Find(com.ItemID);
                if (!model.Items.Contains(x))
                {
                    model.Items.Add(x);
                }
            }
            List<string> As = new List<string>();
            As.Add(col.Type1);
            As.Add(col.Type2);
            As.Add(col.Type3);
            As.Add(col.Type4);
            As.Add(col.Type5);
            As.Add(col.Type6);
            As.Add(col.Type7);
            As.Add(col.Type8);
            As.Add(col.Type9);
            model.Type = As;
            model.ImageUrl = col.ImageUrl;
            model.Description = col.Description;
            return View(model);
        }

        public async Task<IActionResult> EditItem(string id)
        {
            Item plant = _context.Items.Find(id);
            if (plant == null)
            {
                return NotFound();
            }
            var a = _context.Collections.Find(plant.CollectionID);


            EditItemViewModel model = new EditItemViewModel { ItemID = plant.ItemID, CollectionID = plant.CollectionID };
            List<string> modelType = new List<string>();
            List<string> modelT = new List<string>();
            if (plant.Field1 != "null")
            {
                modelType.Add(plant.Field1);
                modelT.Add(a.Type1);
            }
            if (plant.Field2 != "null")
            {
                modelType.Add(plant.Field2); modelT.Add(a.Type2);
            }
            if (plant.Field3 != "null")
            {
                modelType.Add(plant.Field3); modelT.Add(a.Type3);
            }
            if (plant.Field4 != "null")
            {
                modelType.Add(plant.Field4); modelT.Add(a.Type4);
            }
            if (plant.Field5 != "null")
            {
                modelType.Add(plant.Field5); modelT.Add(a.Type5);
            }
            if (plant.Field6 != "null")
            {
                modelType.Add(plant.Field6); modelT.Add(a.Type6);
            }
            if (plant.Field7 != "null")
            {
                modelType.Add(plant.Field7); modelT.Add(a.Type7);
            }
            if (plant.Field8 != "null")
            {
                modelType.Add(plant.Field8); modelT.Add(a.Type8);
            }
            if (plant.Field9 != "null")
            {
                modelType.Add(plant.Field9);
                modelT.Add(a.Type9);
            }
            model.Type = modelT;
            model.Field = modelType;
            model.Name = plant.Name;
            model.Description = plant.Description;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditItem(EditItemViewModel model1, string Id, string field1, string field2, string field3, string field4, string field5, string field6, string field7, string field8, string field9)
        {
            if (ModelState.IsValid)
            {
                Item plant = _context.Items.Find(model1.ItemID);
                if (plant != null)
                {

                    plant.Field1 = field1;

                    plant.Field2 = field2;

                    plant.Field3 = field3;

                    plant.Field4 = field4;

                    plant.Field5 = field5;

                    plant.Field6 = field6;

                    plant.Field7 = field7;

                    plant.Field8 = field8;

                    plant.Field9 = field9;


                    plant.Name = model1.Name;
                    plant.Description = model1.Description;
                    _context.Items.Update(plant);
                    _context.SaveChanges();
                    return RedirectToAction("Collections", new { Id = model1.CollectionID });


                }
            }
            return View(model1);
        }

        public async Task<ActionResult> DeleteItem(string id)
        {
            Item plant = _context.Items.Find(id);
            if (plant != null)
            {
                var c = _context.Likes.Where(x => x.ItemID == id).FirstOrDefault();
                if (c != null)
                {
                    _context.Likes.Remove(c);
                }
                _context.Items.Remove(plant);
                _context.SaveChanges();
            }
            var a = _context.Collections.Find(plant.CollectionID);
            a.CountItem--;
            _context.Update(a);
            _context.SaveChanges();
            return RedirectToAction("Collections", new { Id = plant.CollectionID });
        }
        [HttpGet]
        [HttpPost]
        public async Task<ActionResult> GetLike(string id)
        {

            var a = _context.Items.Find(id);
            var col = _context.Collections.Find(a.CollectionID);
            var l = _context.Likes.ToList();
            var c = _context.Users.ToList();
            List<Like> like = new List<Like>();
            List<User> user = new List<User>();
            foreach (var ll in l)
            {
                if (ll.ItemID == id)
                {
                    like.Add(ll);
                    user.Add(_context.Users.Find(ll.UserID));
                }
            }
            var user2 = await _userManager.GetUserAsync(User);

            if (user.Contains(user2))
            {
                foreach (var lll in like)
                {
                    if (lll.UserID == user2.Id)
                    {

                        _context.Likes.Remove(lll);
                    }
                    else
                    {
                        lll.Count--;
                        _context.Likes.Update(lll);
                    }
                }
            }
            else
            {
                int i = 1;
                foreach (var lll in like)
                {

                    lll.Count++;
                    _context.Likes.Update(lll);
                    i = lll.Count;

                }
                Like Like = new Like { Count = i, UserID = user2.Id, ItemID = id };
                _context.Likes.Add(Like);


            }

            _context.SaveChanges();

            return RedirectToAction("Collections", new { Id = a.CollectionID });
        }


    }
}
