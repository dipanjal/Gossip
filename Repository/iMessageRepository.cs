using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Gossip
{
    public interface iMessageRepository<Type>
    {
        int add(Type Obj);
        List<Type> getFriendsMessage(string username, string fusername);
        int delete(int id);
        int deleteAll(string username, string fusername);
        List<Type> search(string key);
        List<Type> searchFriendsMessage(string username, string fusername, string key);
    }
}
