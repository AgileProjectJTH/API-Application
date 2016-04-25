using CorridorAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CorridorAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            kronox.getSchedule("E2420", null);
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
