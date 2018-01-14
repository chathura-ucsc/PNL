using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PassengersAPI.Controllers
{   
    /// <summary>
    /// Home controller
    /// </summary>
    public class HomeController : Controller
    {   
        /// <summary>
        /// This will give the UI for Home page.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
