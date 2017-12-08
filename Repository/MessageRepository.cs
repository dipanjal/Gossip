using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Project_Gossip
{
    public class MessageRepository : iMessageRepository<Message>
    {
        GossipDBContext db = new GossipDBContext();
        public int add(Message obj)
        {
            db.Messages.Add(obj);
            return db.SaveChanges();
        }
        public List<Message> getFriendsMessage(string username,string fusername)
        {
            var empList = from obj in db.Messages
                          where obj.Username==username && obj.FriendsUsername==fusername
                          select obj;
            return empList.ToList();
            //throw new NotImplementedException();
        }

        public int delete(int id) //delete single message
        {
            Message mDel = db.Messages.SingleOrDefault(x=>x.Id==id);
            db.Messages.Remove(mDel);
            return db.SaveChanges();
            //throw new NotImplementedException();
        }

        public int deleteAll(string username,string fusername) //delete all msg from that user
        {
            List<Message> mListToDel = db.Messages.Where(x=>x.Username==username && x.FriendsUsername==fusername).ToList();
            foreach(Message mDel in mListToDel)
            {
                db.Messages.Remove(mDel);
            }
            return db.SaveChanges();
        }

        public List<Message> search(string keyword) //search all message in the inbox
        {
            var results = from c in db.Messages
                          where c.MessageBody.Contains(keyword)
                          select c;
            return results.ToList();
        }

        public List<Message> searchFriendsMessage(string username,string fusername,string key) //search a friend's message in the inbox
        {
            var results = from c in db.Messages
                          where c.Username==username 
                          && c.FriendsUsername==fusername 
                          && c.MessageBody.Contains(key)
                          select c;
            return results.ToList();
        }

    }
}