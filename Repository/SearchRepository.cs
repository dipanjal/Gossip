using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Gossip
{
    public class SearchRepository : iSearchRepository<User>
    {
        GossipDBContext db = new GossipDBContext();

        public List<string> SearchFriendsName(string key)
        {
            var results = from c in db.Users
                        where c.Name.Contains(key)
                        select c.Name;
            return results.ToList();
        }
    }
}