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
       
        public static LinkedList<User> Users = new LinkedList<User> ();

        public void Send(string name)
        {
         
            string s = Context.User.Identity.Name;
            Clients.Caller.addMessage(name);
        }

        public void LogIn(string name, string password)
        {
            Users.AddLast(new User (name,password,Context.ConnectionId));
        }
        public void LogOut ()
        {
            
        }


        public override Task OnConnected()
        {

            if (Context.User.Identity.IsAuthenticated && !IsExist(Users, Context.User.Identity.Name))
            {
                Users.AddLast(new User ("samp","kovp",""));
                Clients.Caller.Enter();
            }
                

            return base.OnConnected();
        }

        public bool IsExist(IEnumerable<User> users, string name)
        {
            foreach (User u in users)
                if (u.Name == name)
                    return true;
            return false;
        }
    }
}