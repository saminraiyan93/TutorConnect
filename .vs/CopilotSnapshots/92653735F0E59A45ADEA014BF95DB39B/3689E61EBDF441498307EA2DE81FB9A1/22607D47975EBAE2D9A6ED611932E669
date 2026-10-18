using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using DAL.EF.Tables;

namespace DAL.Repository
{
    public class UserRepo
    {
        TutorConnectContext db;
        public UserRepo(TutorConnectContext db)
        {
            this.db = db;
        }

        public List<User> GetAllUsers()
        {
            return db.Users.ToList();
        }

        public User GetUserById(int id)
        {
            var data = db.Users.Find(id);
            return data;
        }

        public bool CreateUser(User user)
        {
            var data = db.Users.Add(user);
            return db.SaveChanges() > 0;
        }

        public bool UpdateUser(User user)
        {
            var exUser = db.Users.Find(user.UserId);

            if(exUser == null)
            {
                return false;
            }

            exUser.UserName = user.UserName;
            exUser.Email = user.Email;
            exUser.Password = user.Password;
            exUser.Role = user.Role;
            exUser.AccountStatus = user.AccountStatus;

            return db.SaveChanges() > 0;
        }

        public bool DeleteUser(int id)
        {
            var data = db.Users.Find(id);
            db.Users.Remove(data);
            return db.SaveChanges() > 0;
        }


        // FOR MVC Login
        public User GetUserByEmailAndPassword(string email, string password)
        {
            return db.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }
    }
}
