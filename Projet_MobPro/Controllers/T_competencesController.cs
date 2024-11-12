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
    public class T_competencesController : Controller
    {
        private Mobilite_Pro_BDDEntities db = new Mobilite_Pro_BDDEntities();

        // GET: T_competences
        public ActionResult Index()
        {
            return View(db.T_competences.ToList());
        }

        // GET: T_competences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_competences t_competences = db.T_competences.Find(id);
            if (t_competences == null)
            {
                return HttpNotFound();
            }
            return View(t_competences);
        }

        // GET: T_competences/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: T_competences/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nom_competence")] T_competences t_competences)
        {
            if (ModelState.IsValid)
            {
                db.T_competences.Add(t_competences);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(t_competences);
        }

        // GET: T_competences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_competences t_competences = db.T_competences.Find(id);
            if (t_competences == null)
            {
                return HttpNotFound();
            }
            return View(t_competences);
        }

        // POST: T_competences/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nom_competence")] T_competences t_competences)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_competences).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_competences);
        }

        // GET: T_competences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_competences t_competences = db.T_competences.Find(id);
            if (t_competences == null)
            {
                return HttpNotFound();
            }
            return View(t_competences);
        }

        // POST: T_competences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_competences t_competences = db.T_competences.Find(id);
            db.T_competences.Remove(t_competences);
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
