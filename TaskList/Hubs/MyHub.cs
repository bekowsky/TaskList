using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using Microsoft.AspNet.SignalR;
using TaskList.Models;

namespace TaskList.Hubs
{
    public class MyHub : Hub
    {
       public static int hui = 5;
        public static LinkedList<User> Users = new LinkedList<User> ();

        public void Send(string name)
        {
         
            string s = Context.User.Identity.Name;
            Clients.Caller.addMessage(name);
        }

        public void Authorization(string name)
        {

        }

        public override Task OnConnected()
        {
            Users.AddLast(new User());

            return base.OnConnected();
        }
    }
}