using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Project_Gossip
{
    class UserRepository : iRepository<User>,iUserRepository
    {
        GossipDBContext db = new GossipDBContext();
        public int add(User emp) 
        {
            db.Users.Add(emp);
            return db.SaveChanges();
        }
        public User getByName(string name)
        {
            throw new NotImplementedException();
        }

        public User getById(string username)
        {
            User user = db.Users.SingleOrDefault(d => d.Username == username);
            return user;
        }
        public User getByEmail(string email)
        {
            User user = db.Users.SingleOrDefault(d => d.Email == email);
            return user;
        }
        public User getById(int id)
        {
            throw new NotImplementedException();
        }

        public int edit(User emp)
        {
            User empToUpdate = db.Users.SingleOrDefault(d => d.Username == emp.Username);
            empToUpdate.Name = emp.Name;
            empToUpdate.Email = emp.Email;
            empToUpdate.DateofBirth = emp.DateofBirth;
            return db.SaveChanges();
        }

        public int delete(string username)
        {
            User empToDelete = db.Users.SingleOrDefault(d => d.Username == username);
            db.Users.Remove(empToDelete);
            return db.SaveChanges();
        }
        public int delete(int id)
        {
            throw new NotImplementedException();
        }

        //@override
        public List<User> getAll()
        {
            var results = from c in db.Users
                          where c.UserRoleId==2 
                          select c;
            return results.ToList();
        }

        public List<User> search(string key)
        {
            var results = from c in db.Users
                          where c.Name.Contains(key)
                          select c;
            return results.ToList();
        }

        public int UpdateProfilePictire(string username,string imgUrl)
        {
            User userToUpdate = db.Users.SingleOrDefault(d => d.Username == username);
            userToUpdate.ProfilePicture = imgUrl;
            return db.SaveChanges();
        }

    }
}