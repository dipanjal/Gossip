using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Gossip.Controllers
{
    public class SearchController : Controller
    {
        iSearchRepository<User> searchRepo = RepositoryFactory.getFactoryInstance().getSearchRepositoryInstance();

        // GET: Search
        [HttpGet]
        public ActionResult Index(string term)
        {
            List<string> sortedNames = null;
            if (term == null)
            {
                sortedNames = searchRepo.SearchFriendsName(term);
            }
            else
            {
                sortedNames = searchRepo.SearchFriendsName(term);
            }
            return Json(sortedNames, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}