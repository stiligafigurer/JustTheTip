using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using JustTheTip.DAL;
using JustTheTip.Models;

namespace JustTheTip.Controllers {
    public class UsersController : Controller {
        private JTTContext db = new JTTContext();

        // GET: Users
        public ActionResult Index() {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null) {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Register() {
            ViewBag.CountryList = GetCountries();
            ViewBag.ZodiacList = GetZodiac();
            ViewBag.GenderList = GetGenders();
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "UserID,Password,FirstName,LastName,Email,Gender,SexualOrientation,BirthDate,ProfilePicUrl,ZodiacSign,Country,District,ActiveUser")] User user) {
            if (ModelState.IsValid) {
                user.ActiveUser = 1;
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ZodiacList = GetZodiac();
            ViewBag.CountryList = GetCountries();
            ViewBag.GenderList = GetGenders();
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null) {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Password,FirstName,LastName,Email,Gender,SexualOrientation,BirthDate,ProfilePicUrl,ZodiacSign,Country,District,ActiveUser")] User user) {
            if (ModelState.IsValid) {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null) {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private List<string> GetCountries() {
            List<string> CountryList = new List<string>();
            CultureInfo[] CInfoList = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (CultureInfo CInfo in CInfoList) {
                RegionInfo r = new RegionInfo(CInfo.LCID);
                if (!(CountryList.Contains(r.EnglishName))) {
                    CountryList.Add(r.EnglishName);
                }
            }

            CountryList.Sort();
            return CountryList;
        }

        private List<string> GetZodiac() {
            return new List<string> {
                "Aries",
                "Taurus",
                "Gemini",
                "Cancer",
                "Leo",
                "Virgo",
                "Libra",
                "Scorpio",
                "Sagittarius",
                "Capricorn",
                "Aquarius",
                "Pisces"
            };
        }

        private List<string> GetGenders() {
            return new List<string>() {
                "Male",
                "Female",
                "Other"
            };
        }
    }
}
