using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Project_Gossip
{
    public class FriendshipStatusRepository : iRepository<FriendshipStatus>
    {
        GossipDBContext db = new GossipDBContext();
        public int add(FriendshipStatus obj)
        {
            db.FriendshipStatuses.Add(obj);
            return db.SaveChanges();
        }

        public FriendshipStatus getById(int id)
        {
            return db.FriendshipStatuses.SingleOrDefault(x => x.Id == id);
        }

        public int delete(string id)
        {
            throw new NotImplementedException();
        }
        public int delete(int id)
        {
            throw new NotImplementedException();
        }
        public int edit(FriendshipStatus obj)
        {
            throw new NotImplementedException();
        }

        public List<FriendshipStatus> getAll()
        {
            throw new NotImplementedException();
        }

        public FriendshipStatus getByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public FriendshipStatus getById(string id)
        {
            throw new NotImplementedException();
        }

        public FriendshipStatus getByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<FriendshipStatus> search(string str)
        {
            throw new NotImplementedException();
        }

        public List<FriendshipStatus> getAllNotFriend(string id)
        {
            throw new NotImplementedException();
        }
    }
}