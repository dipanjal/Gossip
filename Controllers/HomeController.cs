using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace Project_Gossip.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        RepositoryFactory factory = RepositoryFactory.getFactoryInstance();
        iRepository<User> userRepo = RepositoryFactory.getFactoryInstance().getUserRepositoryInstance();

        [HttpGet]
        public ActionResult Index()
        {
            this.userRepo = factory.getUserRepositoryInstance();
            iFriendListRepository fRepo = factory.getFriendListRepositoryInstance();
            List<User> uListToShow = new List<User>();
            string username = Session["user"].ToString();
            List<User> users = userRepo.getAll();
            foreach(User u in users)
            {
                FriendList friend = fRepo.getById(u.Username, username);
                FriendList friendInverse = fRepo.getById(username, u.Username);
                if (friend==null && friendInverse==null)
                {
                    uListToShow.Add(u);
                }
            }
            return View(uListToShow);
        }

        [HttpGet]
        public ActionResult ViewUserProfile(string id)
        {
            this.userRepo = factory.getUserRepositoryInstance();
            //return Content(id);
            User user = this.userRepo.getById(id);
            return View(user);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Remove("user");
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public ActionResult AddAsFriend(string id)
        {
            string uname = Session["user"].ToString();
            FriendList makeFriend = new FriendList {
                Username = id, //he/she is the username you want to connect
                FriendsUsername = uname, //because you are adding yourself as his or her friend
                FriendshipStatusId =1 // 1:pending, 2:accepted, 3:blocked
            };

            iFriendListRepository friendshipRepo = factory.getFriendListRepositoryInstance();
            if(friendshipRepo.getById(id,uname)==null)
            {
                friendshipRepo.add(makeFriend);
            }
            return RedirectToAction("FriendRequestSend");
        }

        [HttpGet]
        public ActionResult FriendRequestSend()
        {
            iFriendListRepository fListRepo = factory.getFriendListRepositoryInstance();
            iRepository<FriendshipStatus> fStatusRepo = factory.getFriendshipStatusRepositoryInstance();

            string uname = Session["user"].ToString();
            List<FriendList> fReqSendList = fListRepo.getFriendRequestSend(uname);
            foreach (FriendList f in fReqSendList)
            {
                f.FriendshipStatus = fStatusRepo.getById(f.FriendshipStatusId);
            }
            return View(fReqSendList);
        }

        [HttpGet]
        public ActionResult FriendRequestGet()
        {
            iFriendListRepository fListRepo = factory.getFriendListRepositoryInstance();
            iRepository<User> userRepo = factory.getUserRepositoryInstance();

            string uname = Session["user"].ToString();
            List<FriendList> pendingRequestList = fListRepo.getFriendRequestPending(uname);
            List<User> uListToView = new List<User>();
            foreach(FriendList f in pendingRequestList)
            {
                User u = userRepo.getById(f.FriendsUsername); //get friends details
                uListToView.Add(u);
                //f.FriendshipStatus = fStatusRepo.getById(f.Id);
            }
            return View(uListToView);
        }

        [HttpGet]
        public ActionResult AcceptFriendRequest(string id)
        {
            iFriendListRepository fListRepo = factory.getFriendListRepositoryInstance();
            string uname = Session["user"].ToString();
            fListRepo.acceptRequest(uname, id);
            return RedirectToAction("FriendRequestGet");
        }

        [HttpGet]
        public ActionResult MyFriendList()
        {
            iFriendListRepository fRepo = factory.getFriendListRepositoryInstance();
            iRepository<User> userRepo = factory.getUserRepositoryInstance();
            
            string uname = Session["user"].ToString();
            //List<FriendList> myFList = fRepo.getAll();
            //foreach(FriendList f in myFList)
            //{
            //    FriendList friend = fRepo.getById(f.FriendsUsername, uname);
            //    FriendList friendInverse = fRepo.getById(uname, f.Username);
            //    if (friend == null && friendInverse == null)
            //    {
            //        myFList.Remove(f);
            //    }
            //}
            List<FriendList> myFList = fRepo.getFriendList(uname);
            List<FriendList> myRevFList = fRepo.getReversedFriendList(uname);
            List<User> uListToShow = new List<User>(); //LIST OF USERS WHO HAVE FRIENDSHIP WITH YOU
            if (myFList != null)
            {
                foreach (FriendList f in myFList)
                {
                    uListToShow.Add(userRepo.getById(f.FriendsUsername));
                }
            }
            if(myRevFList!=null)
            {
                foreach (FriendList f in myRevFList)
                {
                    uListToShow.Add(userRepo.getById(f.Username));
                }
            }
            //if (myFList == null)
            //{
            //    myRevFList = fRepo.getReversedFriendList(uname);
            //}
            //else
            //{
            //    foreach (FriendList f in myFList)
            //    {
            //        uListToShow.Add(userRepo.getById(f.FriendsUsername));
            //    }
            //}


            return View(uListToShow);
        }

        [HttpGet]
        public ActionResult DeleteFriendRequest(string id)
        {
            iRepository<User> repo = factory.getUserRepositoryInstance();
            return View(repo.getById(id));
        }

        [HttpPost]
        [ActionName("DeleteFriendRequest")]
        public ActionResult DeleteFriendRequestConfirm(string id)
        {
            iFriendListRepository repo = factory.getFriendListRepositoryInstance();
            string uname = Session["user"].ToString();
            repo.unfriend(uname,id);
            //repo.deleteRequest(uname,id); 
            return RedirectToAction("MyFriendList");
        }

        [HttpGet]
        public ActionResult CancelFriendRequest(string id)
        {
            iRepository<User> repo = factory.getUserRepositoryInstance();
            return View(repo.getById(id));
        }

        [HttpPost]
        [ActionName("CancelFriendRequest")]
        public ActionResult CancelFriendRequestConfirm(string id)
        {
            iFriendListRepository repo = factory.getFriendListRepositoryInstance();
            string uname = Session["user"].ToString();
            repo.deleteRequest(id, uname); //yourfriend's username | your username --> username(your friend),fusername(you)
            return RedirectToAction("MyFriendList");
        }

    }
}