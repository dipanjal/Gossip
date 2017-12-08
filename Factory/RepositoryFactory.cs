using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace Project_Gossip
{
    //singletone applied
    public class RepositoryFactory
    {
        private static RepositoryFactory factory = null;
        private RepositoryFactory() { }
        public static RepositoryFactory getFactoryInstance()
        {
            if(factory==null)
            {
                factory = new RepositoryFactory();
            }
            return factory;
        }


        public iUserRepository getUserRepositoryInstance()
        {
            return new UserRepository();
        }
        public iRepository<UserRole> getUserRoleRepositoryInstance()
        {
            return new UserRoleRepository();
        }
        public iRepository<AccountStatus> getAccountStatusRepositoryInstance()
        {
            return new AccountStatusRepository();
        }
        public iLoginRepository<Userlogin> getUserLoginRepositoryInstance()
        {
            return new UserloginRepository();
        }
        public iRepository<FriendshipStatus> getFriendshipStatusRepositoryInstance()
        {
            return new FriendshipStatusRepository();
        }
        public iFriendListRepository getFriendListRepositoryInstance()
        {
            return new FriendListRepository();
        }

        public iMessageRepository<Message> getMessageRepositoryInstance()
        {
            return new MessageRepository();
        }

        public iSearchRepository<User> getSearchRepositoryInstance()
        {
            return new SearchRepository();
        }

    }
}