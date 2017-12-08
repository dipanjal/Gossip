using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Gossip
{
    public class Userlogin
    {
        [Key]
        public int Id { set; get; }

        public string Email { set; get; }

        [MinLength(5)]
        public string Password { set; get; }

        public string Username { set; get; }

        //public User user { set; get; }


    }
}