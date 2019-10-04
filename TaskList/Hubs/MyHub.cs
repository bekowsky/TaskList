using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using TaskList.Models;

namespace TaskList.Hubs
{
    
    public class MyHub : Hub
    {
       
       
       public static Dictionary<string, string> Users = new Dictionary<string, string>();
        public TaskContext db = new TaskContext();
        public void Create(string name, string description)
        {

            string key = GenRandomString(7) + Convert.ToString(db.Projects.OrderByDescending(x => x.Id).FirstOrDefault().Id);          
            Project project = new Project { Name = name, Describtion = description, IsDone = false,  Key = key, Creator = Context.User.Identity.Name, SettingsPermission = "1" , ChangePermission = "1", ReadPermission = "1"};        
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

       public void ProjectsInfo (int num, string watch, string change, string setting)
        {
            Project project = db.Projects.Find(num);
            project.ReadPermission = watch;
            project.ChangePermission = change;
            project.SettingsPermission = setting;
            db.SaveChanges();
        }

        public void AddFriend (string name)
        {

            User user = db.FindByName(Context.User.Identity.Name);
            bool AllowAdding = true;
            foreach (Friend friend in db.FindByName(name).Friends)
                if (friend.Name == Context.User.Identity.Name)
                    AllowAdding = false;
            if (AllowAdding)
            {
                db.FindByName(name).Friends.Add(new Friend { Name = user.Name, Online = DateTime.MinValue, IsAssept = false, Image = user.Image });
                db.SaveChanges();
                if (Users.ContainsKey(name))
                {
                    Clients.Client(Users[name]).notification(Context.User.Identity.Name + " подписался на вас !");
                    Clients.Client(Users[name]).appendSubscriber(Context.User.Identity.Name);
                }
            }
        }

        public void ConfirmFriend(string name)
        {

            User user = db.FindByName(Context.User.Identity.Name);
            bool AllowAdding = true;
            foreach (Friend friend in db.FindByName(name).Friends)
                if (friend.Name == Context.User.Identity.Name)
                    AllowAdding = false;
            if (AllowAdding)
            {
                db.FindByName(name).Friends.Add(new Friend { Name = user.Name, Online = DateTime.MinValue, IsAssept = false, Image = user.Image });
               db.SaveChanges();
                if (Users.ContainsKey(name))
                    Clients.Client(Users[name]).appendSubscriber(Context.User.Identity.Name);
                FriendshipAccepted(name);


            }
        }

        public void FriendshipAccepted(string name)
        {
            db.FindByName(Context.User.Identity.Name).Friends.Single(x => x.Name == name).IsAssept = true;
            db.FindByName(name).Friends.Single(x => x.Name == Context.User.Identity.Name).IsAssept = true;
            db.SaveChanges();
            string str = " теперь ваш друг!";
            if (Users.ContainsKey(name))
            {
                Clients.Client(Users[name]).notification(Context.User.Identity.Name + str);
                Clients.Client(Users[name]).removeSubscriber(Context.User.Identity.Name);
                Clients.Client(Users[name]).appendFriend(Context.User.Identity.Name);
            }
            Clients.Caller.notification(name + str);

            Clients.Caller.removeSubscriber(name);
            Clients.Caller.appendFriend(name);
        }


        public void DeleteFriend(string name)
        {
            db.FindByName(Context.User.Identity.Name).Friends.Single(x => x.Name == name).IsAssept = false;
            db.FindByName(name).Friends.Remove(db.FindByName(name).Friends.Single(x => x.Name == Context.User.Identity.Name));
            db.SaveChanges();
            if (Users.ContainsKey(name))
            {
                Clients.Client(Users[name]).notification(Context.User.Identity.Name + " и вы больше не друзья!");
                Clients.Client(Users[name]).removeFriend(Context.User.Identity.Name);

            }
            Clients.Caller.notification(name + " и вы больше не друзья!");
            Clients.Caller.removeFriend(name);
            Clients.Caller.appendSubscriber(name);
        }
      
        public void FindUsers(string text)
        {
            List<string> users = new List<string>();
            User Me = db.FindByName(Context.User.Identity.Name);
            foreach (User user in db.Users)
                if (user.Name.Contains(text) && user.Name != Context.User.Identity.Name )
                    users.Add(user.Name);
           

            Clients.Caller.returnUsers(users);
        }

        public void AddStep(string step,string key)
        {

           Row row = new Row { Text = step };
          db.FindByKey(key).Rows.Add(row);
            db.SaveChanges();
        }

       public void  saveSettings (string key, string [] arr)
        {
            Project project = db.FindByKey(key);
            User user = db.FindByName(project.Creator);
            project.Users.Clear();
            project.Users.Add(user);
            for (int i = 0; i < arr.Length; i++)
                project.Users.Add(db.FindByName(arr[i]));
            db.SaveChanges();
           
        }



        public override Task OnConnected()
        {
          
            if (Context.User.Identity.IsAuthenticated && !Users.ContainsKey(Context.User.Identity.Name))
            {
                Users.Add(Context.User.Identity.Name, Context.ConnectionId);
                Clients.Caller.enter();
            }
                

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Users.Remove(Context.User.Identity.Name);
            return base.OnDisconnected(stopCalled);
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