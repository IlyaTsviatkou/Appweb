using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Korzh.EasyQuery.Linq;
using Microsoft.AspNetCore;
using Appweb.Domain.Core;
using Appweb.Infrastructure.Data;
using Appweb.Services.Business;
using Org.BouncyCastle.Asn1.Cms;

namespace Appweb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationContext db;
        private UserManager<User> _userManager;
        ApplicationContext _context;
        RoleManager<IdentityRole> roleManager;


        public HomeController( UserManager<User> userManager, ApplicationContext context)
        {
            
            _userManager = userManager;
            _context = context;
        
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
        [Authorize]
        public async Task<IActionResult> Profile()
        {
           // var model = await _userManager.FindByEmailAsync(User.Identity.Name);
            User user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
            // UserItem ui = await _context.
            var model = _context.Users.Include(c => c.UserItems)
                            .ThenInclude(sc => sc.Item)
                            .Single(id => id.Id == user.Id);

            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> MailRequest(string id)
        {
            Item item = _context.Items.Include(col => col.Collections)
                .ThenInclude(u => u.Users)
                .Single(i => i.ItemID == id);
            User user = item.Collections.Users;
            User user2 = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
            //User user = await _userManager.FindByIdAsync(id);
            EmailService emailService = new EmailService();

            // return RedirectToAction("Index");
            if (user != null)
            {
                await emailService.SendEmailAsync(user.Email, "Уведомление о покупке", "Аккаунт " + user2.UserName + " хотел бы купить ваш " + item.Name);
                
            }
            return RedirectToAction("DeleteFromBasket",new { id = id});
        }
        [HttpGet]       
        [HttpPost]
        public async Task<IActionResult> DeleteFromBasket(string id)
        {
            Item item = _context.Items.Include(ui=>ui.UserItems)
                .ThenInclude(u => u.User)
                .Where(iid=>iid.ItemID==id)
                .Single(i => i.ItemID == id);
            User user = item.UserItems.ElementAt(0).User;
            User user2 = _context.Users.Include(ui => ui.UserItems)
                .Single(i => i.Id == user.Id);
            if (user != null)
            {
                user2.UserItems.Remove(user.UserItems.ElementAt(0));
                _context.SaveChanges();
            }
            return RedirectToAction("Profile");
        }

        public IActionResult TableUsers()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return base.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
