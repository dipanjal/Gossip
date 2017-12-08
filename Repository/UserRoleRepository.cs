using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Project_Gossip
{
    public class UserRoleRepository : iRepository<UserRole>
    {
        GossipDBContext db = new GossipDBContext();
        public int add(UserRole obj)
        {
            db.UserRoles.Add(obj);
            return db.SaveChanges();
            //throw new NotImplementedException();
        }
        public UserRole getById(int id)
        {
            return db.UserRoles.SingleOrDefault(x => x.Id == id);
        }
        public UserRole getById(string id)
        {
            throw new NotImplementedException();
        }
        public int edit(UserRole obj)
        {
            throw new NotImplementedException();
        }
        public int delete(string id)
        {
            throw new NotImplementedException();
        }
        public List<UserRole> getAll()
        {
            throw new NotImplementedException();
        }
        public UserRole getByName(string name)
        {
            throw new NotImplementedException();
        }
        public List<UserRole> search(string str)
        {
            throw new NotImplementedException();
        }

        public UserRole getByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public int delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<UserRole> getAllNotFriend(string id)
        {
            throw new NotImplementedException();
        }
    }
}