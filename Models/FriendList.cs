using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Gossip
{
    public class FriendList
    {
        [Key]
        public int Id { set; get; }
        public string Username { set; get; }
        public string FriendsUsername { set; get; }
        //public bool IsAccepted { set; get; }
        //public bool IsBlocked { set; get; }
        public int FriendshipStatusId { set; get; }
        [ForeignKey("FriendshipStatusId")]
        public FriendshipStatus FriendshipStatus { set; get; }
    }
}