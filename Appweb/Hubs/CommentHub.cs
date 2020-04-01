using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appweb.ViewModels;
using Microsoft.AspNetCore.SignalR;

namespace Appweb.Models
{
    public class CommentHub : Hub
    {
        
            public async Task SendMesssage(CommentM Comment)
            {
                await Clients.All.SendAsync("receiveMessage", Comment);
            }
        public class CommentM
        {
            public string UserID { get; set; }
            public string ItemID { get; set; }
            public string Text { get; set; }
        }
        
    }
}
