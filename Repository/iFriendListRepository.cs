
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Gossip
{
    public interface iFriendListRepository
    {
        int add(FriendList obj);
        List<FriendList> getFriendRequestSend(string username);
        List<FriendList> getFriendList(string username);
        List<FriendList> getReversedFriendList(string fusername);
        List<FriendList> getFriendRequestPending(string username);
        FriendList getById(string username, string fusername);
        int deleteRequest(string username, string fusername);
        int unfriend(string username,string fusername);
        int acceptRequest(string username, string fusername);
        FriendList getByName(string name);
        List<FriendList> getAll();
        List<FriendList> search(string username, string key);
    }
}
