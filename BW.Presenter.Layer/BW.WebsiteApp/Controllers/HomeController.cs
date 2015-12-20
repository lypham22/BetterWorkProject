using BW.Website.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BW.WebsiteApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [AuthorizedUser(AccessLevels = "AllowAnonymous")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}