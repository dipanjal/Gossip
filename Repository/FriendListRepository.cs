using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Project_Gossip
{
    public class FriendListRepository : iFriendListRepository
    {
        GossipDBContext db = new GossipDBContext();
        public int add(FriendList obj)
        {
            db.FriendLists.Add(obj);
            return db.SaveChanges();
        }

        public List<FriendList> getFriendRequestSend(string username)
        {
            var List = from obj in db.FriendLists
                       where obj.FriendsUsername==username 
                       && obj.FriendshipStatusId==1
                       select obj;
            return List.ToList();
        }
        public List<FriendList> getFriendList(string username)
        {
            var List = from obj in db.FriendLists
                       where obj.Username == username
                       && obj.FriendshipStatusId == 2
                       select obj;
            return List.ToList();
        }
        public List<FriendList> getReversedFriendList(string fusername)
        {
            var List = from obj in db.FriendLists
                       where obj.FriendsUsername == fusername
                       && obj.FriendshipStatusId == 2
                       select obj;
            return List.ToList();
        }
        public List<FriendList> getFriendRequestPending(string username)
        {
            var List = from obj in db.FriendLists
                       where obj.Username == username
                       && obj.FriendshipStatusId == 1
                       select obj;
            return List.ToList();
        }

        public FriendList getById(string username,string fusername)
        {
            return db.FriendLists.SingleOrDefault(x => x.Username == username && x.FriendsUsername==fusername);
        }
        public int deleteRequest(string username, string fusername)
        {
            FriendList objToDelete = db.FriendLists.SingleOrDefault(x => x.Username == username && x.FriendsUsername == fusername);
            db.FriendLists.Remove(objToDelete);
            return db.SaveChanges();
        }
        public int unfriend(string username,string fusername)
        {
            FriendList objToDelete1 = db.FriendLists.SingleOrDefault(x => x.Username == username && x.FriendsUsername == fusername);
            FriendList objToDelete2 = db.FriendLists.SingleOrDefault(x => x.Username == fusername && x.FriendsUsername == username);
            if(objToDelete1!=null)
            {
                db.FriendLists.Remove(objToDelete1);
            }
            if(objToDelete2!=null)
            {
                db.FriendLists.Remove(objToDelete2);
            }
            return db.SaveChanges();
        }

        public int acceptRequest(string username, string fusername)
        {
            FriendList objToUpdate= db.FriendLists.SingleOrDefault(x => x.Username == username && x.FriendsUsername==fusername);
            objToUpdate.FriendshipStatusId = 2;
            return db.SaveChanges();
        }

        public FriendList getByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<FriendList> search(string username,string key)
        {
            var results = from c in db.FriendLists
                          where c.Username==username 
                          && c.FriendsUsername.Contains(key)
                          select c;
            return results.ToList();
        }

        public List<FriendList> getAll()
        {
            var results = from c in db.FriendLists
                          select c;
            return results.ToList();
        }


    }
}