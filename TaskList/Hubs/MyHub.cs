using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
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
        public TaskContext db = new TaskContext();
        public void Create(string name, string description)
        {

            string key = GenRandomString(7) + Convert.ToString(db.Projects.OrderByDescending(x => x.Id).FirstOrDefault().Id);          
            Project project = new Project { Name = name, Describtion = description, IsDone = false,  Key = key };        
            User user =  db.FindByName(Context.User.Identity.Name);
            user.Projects.Add(project);
            db.SaveChanges();
            Clients.Caller.createReady(key);
        }

        public void Remove (int num)
        {
            db.Projects.Find(num).Users.Remove(db.FindByName(Context.User.Identity.Name));
            db.FindByName(Context.User.Identity.Name).Projects.Remove(db.Projects.Find(num));
            db.SaveChanges();
            Clients.Caller.removeReady();
        }
        
        public void getProjects(int mounth)
        {

        }

        public void AddStep(string step,string key)
        {

           Row row = new Row { Text = step };
          db.FindByKey(key).Rows.Add(row);
            db.SaveChanges();
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
                Users.AddLast(db.FindByName(Context.User.Identity.Name));
                Clients.Caller.enter();
            }
                

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Users.Remove(db.FindByName(Context.User.Identity.Name));
            return base.OnDisconnected(stopCalled);
        }

        public bool IsExist(IEnumerable<User> users, string name)
        {
            foreach (User u in users)
                if (u.Name == name)
                    return true;
            return false;
        }

        string GenRandomString(int Length)
        {
            string Alphabet = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm";
            Random rnd = new Random();
            StringBuilder sb = new StringBuilder(Length - 1);
            int Position = 0;
            for (int i = 0; i < Length; i++)
            {
                Position = rnd.Next(0, Alphabet.Length - 1);
                sb.Append(Alphabet[Position]);
            }


            return sb.ToString();
        }
    }
}