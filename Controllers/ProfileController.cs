using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace Project_Gossip.Controllers
{
    public class ProfileController : BaseController
    {
        RepositoryFactory factory = RepositoryFactory.getFactoryInstance();
        iUserRepository userRepo = RepositoryFactory.getFactoryInstance().getUserRepositoryInstance();
        iRepository<AccountStatus> accRepo = RepositoryFactory.getFactoryInstance().getAccountStatusRepositoryInstance();
        iRepository<UserRole> roleRepo = RepositoryFactory.getFactoryInstance().getUserRoleRepositoryInstance();
        iLoginRepository<Userlogin> uLoginRepo = RepositoryFactory.getFactoryInstance().getUserLoginRepositoryInstance();

        string DESTINATION = "/Uploads/";

        // GET: Profile
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                string uname = Session["user"].ToString();
                User user = this.userRepo.getById(uname);
                user.AccountStatus = this.accRepo.getById(user.AccountStatusId);
                user.UserRole = this.roleRepo.getById(user.UserRoleId);
                return View(user);
            }
            else
            {
                TempData["msg"] = "You Must Login First";
                return RedirectToAction("Index","Login");
            }
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (Session["user"] != null)
            {
                return View(this.userRepo.getById(id));
            }
            else
            {
                TempData["msg"] = "You Must Login First";
                return RedirectToAction("Index", "Login");
            }

        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            this.userRepo.edit(user);
            //this.uLoginRepo.UpdateEmail(user.Username,user.Email);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ActionName("ChangePassword")]
        public ActionResult ConfirmChangePassword(string password)
        {
            string uname = Session["user"].ToString();
            uLoginRepo.ChangePassword(uname, password);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ChangeProfilePicture()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangeProfilePicture(FormCollection coll)
        {
            string username = Session["user"].ToString();
            string ImageLocation = this.UploadFile();
            if(userRepo.UpdateProfilePictire(username, ImageLocation)>0)
            {
                TempData["success"] = "Profile Picture Updated";
            }
            else
            {
                TempData["msg"] = "Profile Picture Not Updated";
            }
            //return Content(ImageLocation);
            return RedirectToAction("Index");
        }

        [NonAction]
        public string UploadFile()
        {
            HttpFileCollectionBase filecollection = Request.Files;
            string FileNameString = null;
            if (filecollection.Count > 0)
            {
                var file = filecollection.Get(0);
                if (file != null)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath(this.DESTINATION), fileName);
                    file.SaveAs(path);

                    FileNameString = fileName.ToString();
                }
            }
            else
            {
                TempData["msg"] = "File Collection Is Null";
            }

            return FileNameString;
        }

    }
}