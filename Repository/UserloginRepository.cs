using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Project_Gossip
{
    public class UserloginRepository : iLoginRepository<Userlogin>
    {
        GossipDBContext db = new GossipDBContext();
        public int Add(Userlogin obj)
        {
            db.Userlogins.Add(obj);
            return db.SaveChanges();
        }

        public int Delete(string id)
        {
            throw new NotImplementedException();
        }

        public int ChangePassword(string username,string password)
        {
            Userlogin objToUpdate = db.Userlogins.SingleOrDefault(x => x.Username == username);
            objToUpdate.Password = password;
            return db.SaveChanges();
        }
        public int UpdateEmail(string username,string email)
        {
            Userlogin objToUpdate = db.Userlogins.SingleOrDefault(x=> x.Username==username);
            objToUpdate.Email = email;
            return db.SaveChanges();
        }
        public Userlogin VerifyByEmail(string email, string password)
        {
            Userlogin login = db.Userlogins.SingleOrDefault(u => u.Email == email && u.Password==password);
            return login;
        }

        public Userlogin VerifyByUsername(string username, string password)
        {
            Userlogin login = db.Userlogins.SingleOrDefault(u => u.Username == username && u.Password == password);
            return login;
        }
    }
}