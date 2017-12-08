using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;



namespace Project_Gossip
{
    public class GossipDBSeeder : DropCreateDatabaseIfModelChanges<GossipDBContext>
    {
        string DateTimeFormat = "dd/MM/yyyy";
        RepositoryFactory factory;

        //because this is a virtual methode and we are modifying this so need to user ovverride
        protected override void Seed(GossipDBContext context) 
        {
            factory = RepositoryFactory.getFactoryInstance();
            iRepository<User> userRepo = factory.getUserRepositoryInstance();
            iLoginRepository<Userlogin> loginRepo = factory.getUserLoginRepositoryInstance();
            iRepository<UserRole> roleRepo = factory.getUserRoleRepositoryInstance();
            iRepository<AccountStatus> statusRepo = factory.getAccountStatusRepositoryInstance();
            iRepository<FriendshipStatus> friendshipRepo = factory.getFriendshipStatusRepositoryInstance();
            iFriendListRepository friendListRepo = factory.getFriendListRepositoryInstance();
            iMessageRepository<Message> messageRepo = factory.getMessageRepositoryInstance();

            UserRole role = new UserRole();
            role.Name = "admin";
            role.Description = "Gossip Administrator";
            roleRepo.add(role);
            role.Name = "user";
            role.Description = "Gossip Social User";
            roleRepo.add(role);

            AccountStatus status = new AccountStatus();
            status.Name = "active";
            status.Description = "Confirm Your Account By Email Address";
            statusRepo.add(status);
            status.Name = "inactive";
            status.Description = "Varified Gossip User";
            statusRepo.add(status);
            status.Name = "deactive";
            status.Description = "Deactivated Gossip User";
            statusRepo.add(status);
            status.Name = "banned";
            status.Description = "Ban A Gossip User For A Specific Time ";
            statusRepo.add(status);

            User user = new User();
            user.Email = "admin@gossip.com";
            user.Username = "admin";
            user.Name = "Gossip Admin";
            user.UserRoleId = 1;
            user.AccountStatusId = 1;
            user.OpeningDate = DateTime.Now.ToString(DateTimeFormat);
            userRepo.add(user);

            Userlogin login = new Userlogin();
            login.Email = user.Email;
            login.Username = user.Username;
            login.Password = "admin";
            loginRepo.Add(login);

            FriendshipStatus friendship = new FriendshipStatus();

            friendship.Name = "pending";
            friendship.Description = "Pending Friend";
            friendshipRepo.add(friendship);

            friendship.Name = "accepted";
            friendship.Description = "Friend";
            friendshipRepo.add(friendship);

            friendship.Name = "blocked";
            friendship.Description = "Blocked Friend";
            friendshipRepo.add(friendship);

            base.Seed(context);
        }

    }
}