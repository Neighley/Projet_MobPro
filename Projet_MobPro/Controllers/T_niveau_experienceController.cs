using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projet_MobPro.Models;

namespace Projet_MobPro.Controllers
{
    public class T_niveau_experienceController : Controller
    {
        private Mobilite_Pro_BDDEntities db = new Mobilite_Pro_BDDEntities();

        // GET: T_niveau_experience
        public ActionResult Index()
        {
            return View(db.T_niveau_experience.ToList());
        }

        // GET: T_niveau_experience/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_niveau_experience t_niveau_experience = db.T_niveau_experience.Find(id);
            if (t_niveau_experience == null)
            {
                return HttpNotFound();
            }
            return View(t_niveau_experience);
        }

        // GET: T_niveau_experience/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: T_niveau_experience/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nom_niveau_experience,domaine")] T_niveau_experience t_niveau_experience)
        {
            if (ModelState.IsValid)
            {
                db.T_niveau_experience.Add(t_niveau_experience);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(t_niveau_experience);
        }

        // GET: T_niveau_experience/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_niveau_experience t_niveau_experience = db.T_niveau_experience.Find(id);
            if (t_niveau_experience == null)
            {
                return HttpNotFound();
            }
            return View(t_niveau_experience);
        }

        // POST: T_niveau_experience/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nom_niveau_experience,domaine")] T_niveau_experience t_niveau_experience)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_niveau_experience).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_niveau_experience);
        }

        // GET: T_niveau_experience/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_niveau_experience t_niveau_experience = db.T_niveau_experience.Find(id);
            if (t_niveau_experience == null)
            {
                return HttpNotFound();
            }
            return View(t_niveau_experience);
        }

        // POST: T_niveau_experience/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_niveau_experience t_niveau_experience = db.T_niveau_experience.Find(id);
            db.T_niveau_experience.Remove(t_niveau_experience);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
