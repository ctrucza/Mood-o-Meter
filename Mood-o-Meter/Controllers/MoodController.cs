using System;
using System.Data;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Net;
using System.Web.Mvc;

using Mood_o_Meter.Models;

namespace Mood_o_Meter.Controllers
{
    public class MoodController : Controller
    {
        private MoMContext db = new MoMContext();

        //
        // GET: /Mood/
        public ActionResult Index()
        {
            return View(db.Moods.OrderByDescending(m=>m.Timestamp));
        }

        //
        // GET: /Mood/Details/5
        public ActionResult Details(Int32 id)
        {
            Mood mood = db.Moods.Find(id);
            if (mood == null)
            {
                return HttpNotFound();
            }
            return View(mood);
        }

        private string GetUsername()
        {
            try
            {
                return GetUsernameFromAD();
            }
            catch (Exception)
            {
                return User.Identity.Name;
            }
        }

        private string GetUsernameFromAD()
        {
            using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
            {
                UserPrincipal principal = UserPrincipal.FindByIdentity(context, User.Identity.Name);
                return principal.ToString();
            }
        }

        //
        // POST: /Mood/Create
        [HttpPost]
        public ActionResult Create(string moood)
        {
            Mood mood = new Mood
            {
                Moood = moood,
                Timestamp = DateTime.Now,
                Username = GetUsername()
            };

            db.Moods.Add(mood);
            db.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        //
        // GET: /Mood/Edit/5
        public ActionResult Edit(Int32 id)
        {
            Mood mood = db.Moods.Find(id);
            if (mood == null)
            {
                return HttpNotFound();
            }
            return View(mood);
        }

        //
        // POST: /Mood/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Mood mood)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mood).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mood);
        }

        //
        // GET: /Mood/Delete/5
        public ActionResult Delete(Int32 id)
        {
            Mood mood = db.Moods.Find(id);
            if (mood == null)
            {
                return HttpNotFound();
            }
            return View(mood);
        }

        //
        // POST: /Mood/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Int32 id)
        {
            Mood mood = db.Moods.Find(id);
            db.Moods.Remove(mood);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Record your mood";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "You can contact us via email:";

            return View();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            ViewBag.Username = GetUsername();
        }
    }
}
