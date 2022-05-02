using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SLBFE.Controllers
{
    /// <summary>
    /// This controllder is to load the home page
    /// </summary>
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            Models.DataStore.InitializeDatabase(); // This line initializes the database on the first run

            return View();
        }
    }
}
