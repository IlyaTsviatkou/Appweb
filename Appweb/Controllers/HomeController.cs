using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Appweb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Appweb.ViewModels;
using Korzh.EasyQuery.Linq;
using Microsoft.AspNetCore;

namespace Appweb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationContext db;
        private UserManager<User> _userManager;
        ApplicationContext _context;
        RoleManager<IdentityRole> roleManager;


        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, ApplicationContext context, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
            /* this.roleManager = roleManager;
             var a = _context.Users.ToList().Count;
             var r = _context.Roles.ToList().Count;

             if (a == 0)
             {
                 var b = DateTime.Now;
                 User user = new User { Email = "Admin@Admin.com", UserName = "Admin@Admin.com", Name = "Admin", DateLog = b, DateReg = b, };
                 _userManager.CreateAsync(user, "Admin123");

             }
             if (r == 0)
             {
                 IdentityRole identityRole = new IdentityRole
                 {
                     Name = "Admin"
                 };
                 var t = roleManager.CreateAsync(identityRole);



                 if (t.IsCanceled)
                 {
                     var s = _context.Users.ToList()[0];
                     _userManager.AddToRoleAsync(s, identityRole.Name);
                 }

             }*/
        }


        public IActionResult Index()
        {
            ViewBag.colID = "";
            ViewBag.text = "";
            var a = _context.Collections.ToList();
            ItemTagCol cv = new ItemTagCol();

            var sortedUsers = from u in a
                              orderby u.CountItem
                              descending
                              select u;

            List<Collection> col = new List<Collection>();

            int i = 0;
            foreach (var m in sortedUsers)
            {
                if (i < 4)
                {
                    col.Add(m);
                    i++;
                }
                else
                {
                    break;
                }
            }
            cv.Collection = col;
            cv.Tag = _context.Tags.ToList();
            return View(cv);
        }
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> TagSearch(string modelka)
        {
            Tag tag = _context.Tags.Find(modelka);
            List<Item> a = new List<Item>();
            List<Comment> c = new List<Comment>();
            if(tag==null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(tag.Text))
            {
                a = _context.Items.FullTextSearchQuery(tag.Text).ToList();
                c = _context.Comments.FullTextSearchQuery(tag.Text).ToList();
            }
            Items model = new Items();
            List<Item> b = new List<Item>();
            if (tag.CollectionID != "")
            {
                foreach (var s in a)
                {
                    if (s.CollectionID == tag.CollectionID)
                        b.Add(s);
                }
                model.item = b;
            }
            else
                model.item = a;
            foreach (Comment com in c)
            {
                var x = _context.Items.Find(com.ItemID);
                if (!model.item.Contains(x))
                {
                    model.item.Add(x);
                }
            }

            return View("Search", model);
        }
        [HttpPost]
        public IActionResult Search(string text, Items model)
        {
            List<Item> a = new List<Item>();
            List<Comment> c = new List<Comment>();
            if (!string.IsNullOrEmpty(text))
            {
                a = _context.Items.FullTextSearchQuery(text).ToList();
                c = _context.Comments.FullTextSearchQuery(text).ToList();
                Tag tag = new Tag { CollectionID = "", Text = text, Main = true };
                var tags = _context.Tags.Where(t => t.Text == tag.Text).FirstOrDefault();
                if (tags == null)
                {
                    _context.Tags.Add(tag);
                    _context.SaveChanges();
                }
            }

            model.item = a;
            foreach (Comment com in c)
            {
                var x = _context.Items.Find(com.ItemID);
                if (!model.item.Contains(x))
                {
                    model.item.Add(x);
                }
            }
            return View(model);
        }
        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> Profile()
        {
            var model = await _userManager.FindByEmailAsync(User.Identity.Name);

            return View(model);
        }

        public IActionResult TableUsers()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
