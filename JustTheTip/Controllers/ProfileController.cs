using JustTheTip.DAL;
using JustTheTip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JustTheTip.Controllers
{
    public class ProfileController : Controller
    {
        public JTTContext db = new JTTContext();
        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Friends()
        {
                                                                      
            return View();
        }
    }
}