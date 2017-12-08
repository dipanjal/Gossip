using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Project_Gossip
{
    public class AccountStatusRepository : iRepository<AccountStatus>
    {
        GossipDBContext db = new GossipDBContext();
        public int add(AccountStatus obj)
        {
            db.AccountStatuses.Add(obj);
            return db.SaveChanges();
        }
        public AccountStatus getById(int id)
        {
            return db.AccountStatuses.SingleOrDefault(x=>x.Id==id);
        }
        public AccountStatus getById(string id)
        {
            throw new NotImplementedException();
        }
        public int edit(AccountStatus obj)
        {
            throw new NotImplementedException();
        }
        public int delete(int id)
        {
            throw new NotImplementedException();
        }
        public int delete(string id)
        {
            throw new NotImplementedException();
        }

        public List<AccountStatus> getAll()
        {
            throw new NotImplementedException();
        }

        public AccountStatus getByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<AccountStatus> search(string str)
        {
            throw new NotImplementedException();
        }

        public AccountStatus getByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public List<AccountStatus> getAllNotFriend(string id)
        {
            throw new NotImplementedException();
        }
    }
}