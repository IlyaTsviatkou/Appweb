using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Appweb.Hubs
{
    public class CommentHub : Hub
    {
        
            public async Task Send(string UserID,string Text)
            {
                await Clients.All.SendAsync("Send",UserID , Text);
            }
      
        
    }
}
