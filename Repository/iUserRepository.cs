using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Gossip
{
    //Additional Interface To Implement Additional Features which are not common to all repositories
    public interface iUserRepository : iRepository<User>
    {
        int UpdateProfilePictire(string username,string url);
    }
}
