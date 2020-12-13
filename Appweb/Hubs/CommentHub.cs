using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appweb.Domain.Core;
using Appweb.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace Appweb.Hubs
{
    public class CommentHub : Hub
    {
        UserManager<User> _userManager;
        
        ApplicationContext _context;

        public CommentHub(UserManager<User> userManager, ApplicationContext applicationContext)
        {
            _userManager = userManager;
            _context = applicationContext;
        }
        public async Task Send(string UserName,string Text,string id, string UserID)
            {
                await Clients.All.SendAsync("Send", UserName , Text, id,UserID);
            Comment model2 = new Comment { ItemID = id, UserID =UserID, Text = Text };

            var a = _context.Comments.ToList();
            List<Comment> b = new List<Comment>();

            foreach (Comment s in a)
            {
                if (s.ItemID == id)
                {
                    b.Add(s);
                }
            }
            if (Text != null || Text != "")
            {
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
            }

        }
      
        
    }
}
