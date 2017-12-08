using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Project_Gossip
{
    public class GossipDBContext : DbContext
    {
        public DbSet<UserRole> UserRoles { set; get; }
        public DbSet<AccountStatus> AccountStatuses { set; get; }
        public DbSet<User> Users { set; get; }
        public DbSet<Userlogin> Userlogins { set; get; }
        public DbSet<FriendshipStatus> FriendshipStatuses { set; get; }
        public DbSet<FriendList> FriendLists { set; get; }
        public DbSet<Message> Messages { set; get; }
    }
}