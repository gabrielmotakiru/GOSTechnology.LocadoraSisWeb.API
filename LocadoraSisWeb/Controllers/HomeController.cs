using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LocadoraSisWeb.Models;

namespace LocadoraSisWeb.Controllers
{
    public class HomeController : Controller
    {
        private LocadoraSisWebContext db = new LocadoraSisWebContext();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult ResetarBanco()
        {
            if (db.Database.Exists())
            {
                db.Database.Delete();
            }
            
            db.Database.CreateIfNotExists();
            return View();
        }
    }
}
