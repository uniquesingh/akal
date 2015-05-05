using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AkalTrucking.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About Akal Trucking LLC.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Informations";

            return View();
        }
    }
}