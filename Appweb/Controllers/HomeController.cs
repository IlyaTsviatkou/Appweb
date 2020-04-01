using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Appweb.Models;
using NHibernate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Appweb.ViewModels;

namespace Appweb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationContext db;
        private UserManager<User> _userManager;
        ApplicationContext _context;

        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, ApplicationContext context)
        {
            _logger = logger;
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
                              orderby u.CollectionID
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
            }
            cv.Collection = col;
            cv.Tag = _context.Tags.ToList();
            return View(cv);
        }
        [HttpPost]
        public async Task<IActionResult> TagSearch(string modelka)
        {
            List<Plant> a = new List<Plant>();
            List<Plant> b = new List<Plant>();
            var ab = ViewBag.colID;
            var ac = modelka;
          
            return View();
        }
      
        public IActionResult Register()
        {
            return View();
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
