using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Facebook;

using System.Web.Security;


namespace Project_Gossip.Controllers
{
    public class LoginController : Controller
    {
        private string fbClientId = "1443540569057415";
        private string fbAppSecret = "2cb33b0c2f06aef8e8d28b6f06332757";

        //RepositoryFactory repoFactory = new RepositoryFactory();

        iRepository<User> userRepo = RepositoryFactory.getFactoryInstance().getUserRepositoryInstance(); //1
        iLoginRepository<Userlogin> userLoginRepo = RepositoryFactory.getFactoryInstance().getUserLoginRepositoryInstance(); //1


        // GET: Login
        [HttpGet]
        public ActionResult Index() //GET:Login
        {
            if (Session["user"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if(userRepo.getById("admin")!=null)
                {
                    TempData["success"] = "Default Data Added";
                }
                return View();
            }
            //return View();
        }


        [HttpPost]
        public ActionResult Index(FormCollection coll) //POST:Login
        {
            string uname = coll["username"];
            string password = coll["password"];

            if (userLoginRepo.VerifyByEmail(uname, password) != null)
            {
                User user = this.userRepo.getByEmail(uname);
                Session["user"] = user.Username;
                Session["user_object"] = user;
                return RedirectToAction("Index","Home");
                //ViewBag.success = "Login Seccess :: Verified By Email";
            }
            else if (userLoginRepo.VerifyByUsername(uname, password) != null)
            {
                User user = this.userRepo.getById(uname);
                Session["user"] = user.Username;
                Session["user_object"] = user;
                return RedirectToAction("Index", "Home");
                //ViewBag.success = "Login Success :: Verified By Username Success";
            }
            else {
                ViewBag.msg = "Username Password Incorrect";
            }
            return View();
            //return Content("POSTED To INDEX");
        }


        [NonAction]
        public string GetUsernameFromEmail(string email)
        {
            try
            {
                string[] arr = email.Split('@');
                return arr[0];
            }
            catch (Exception e)
            {
                return email;
            }

        }

        [HttpPost]
        public ActionResult Signup(User user)
        {
            user.Username = this.GetUsernameFromEmail(user.Email);
            user.DateofBirth = this.GetDob();
            user.OpeningDate = this.GetOpeningDate();
            user.AccountStatusId = 2; //inactive by default
            user.UserRoleId = 2; //user type User

            if (ModelState.IsValid)
            {
                if (userRepo.getByEmail(user.Email) == null) //if email exists or not
                {
                    if (userRepo.add(user) > 0)
                    {
                        Userlogin login = this.GetUserlogin(user);
                        if (new UserloginRepository().Add(login) > 0)
                        {
                            ViewBag.success = "Signup Successfully !";
                        }
                        else
                        {
                            ViewBag.msg = "Not inserted in Userlogin !";
                        }
                    }
                    else
                    {
                        ViewBag.msg = "Not Inserted In User-Table !";
                    }
                }
                else
                {
                    ViewBag.msg = "Email Already Taken, Try Something New";
                }
            }
            else
            {
                ViewBag.msg = "Model Invalidate";
            }

            return View("Index");
        }


        [NonAction]
        public string GetDob()
        {
            string dobday = Request["dobday"];
            string dobmonth = Request["dobmonth"];
            string dobyear = Request["dobyear"];

            return dobday + "/" + dobmonth + "/" + dobyear;
            
        }

        [NonAction]
        public string GetOpeningDate()
        {
            string DateTimeFormat = "dd/MM/yyyy";
            return DateTime.Now.ToString(DateTimeFormat);
        }

        [NonAction]
        public Userlogin GetUserlogin(User user)
        {
            Userlogin login = new Userlogin()
            {
                Password = Request["password"],
                Email = user.Email,
                Username = user.Username
            };
            return login;
        }


        //LOGIN WITH FACEBOOK
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        [AllowAnonymous]
        public ActionResult Facebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(
                new
                {
                    client_id = this.fbClientId,
                    client_secret = this.fbAppSecret,
                    redirect_uri = RedirectUri.AbsoluteUri,
                    response_type = "code",
                    scope = "email"
                });
            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code) //callback url
        {
            try
            {
                var fb = new FacebookClient();

                dynamic result = fb.Post("oauth/access_token", new
                {
                    client_id = this.fbClientId,
                    client_secret = this.fbAppSecret,
                    redirect_uri = RedirectUri.AbsoluteUri,
                    code = code
                });

                var accessToken = result.access_token;
                Session["AccessToken"] = accessToken;
                fb.AccessToken = accessToken;
                dynamic me = fb.Get("me?fields=link,first_name,middle_name,currency,last_name,email,gender,locale,timezone,verified,picture,birthday");

                User fbuser = this.FacebookUsers(me); //insert this user into userdb

                string email = fbuser.Email;
                FormsAuthentication.SetAuthCookie(email, false);


                if(this.userRepo.getByEmail(fbuser.Email)==null) //if the facebook user is new
                {
                    if(this.userRepo.add(fbuser)>0) //insert facebook user data into database
                    {
                        Session["user"] = fbuser.Username;
                        Session["user_object"] = fbuser;
                        //ViewBag.Success = "NEW FACEBOOK USER INSERTED";
                        return RedirectToAction("Index", "Home");
                    }
                }
                else //if the user is already registered
                {
                    Session["user"] = fbuser.Username;
                    Session["user_object"] = fbuser;
                    //ViewBag.Success = "OLD FACEBOOK USER HAS AN ACCOUNT";
                    return RedirectToAction("Index", "Home");
                }

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["msg"] = "Facebook Exception";
                return RedirectToAction("Index");
            }

        }

        [NonAction]
        public User FacebookUsers(dynamic me)
        {
            User u = new User()
            {
                Email = me.email,
                Username = this.GetUsernameFromEmail(me.email),
                Name = me.first_name + " " + me.middle_name + " " + me.last_name,
                //u.Picture = me.picture.data.url,
                DateofBirth = me.birthday,
                AccountStatusId = 1, //facebook user will be actived by default because email is already varified
                UserRoleId = 2, //user type regular User
                OpeningDate = this.GetOpeningDate()
            };

            return u;
        }



        //DONE; OK 


    }
}