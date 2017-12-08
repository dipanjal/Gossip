using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Project_Gossip
{
    public class Message
    {
        [Key]
        public int Id { set; get; } //PK
        public string Username { set; get; }
        public string FriendsUsername { set; get; }
        public string MessageBody { set; get; }
        public int FriendshipStatusId { set; get; } //FK
        [ForeignKey("FriendshipStatusId")]
        public FriendshipStatus FriendshipStatus { set; get; }
    }
}