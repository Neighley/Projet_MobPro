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
    public class T_profil_competencesController : Controller
    {
        private Mobilite_Pro_BDDEntities db = new Mobilite_Pro_BDDEntities();

        // GET: T_profil_competences
        public ActionResult Index()
        {
            var t_profil_competences = db.T_profil_competences.Include(t => t.T_competences).Include(t => t.T_profil);
            return View(t_profil_competences.ToList());
        }

        // GET: T_profil_competences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_profil_competences t_profil_competences = db.T_profil_competences.Find(id);
            if (t_profil_competences == null)
            {
                return HttpNotFound();
            }
            return View(t_profil_competences);
        }

        // GET: T_profil_competences/Create
        public ActionResult Create()
        {
            ViewBag.competences_id = new SelectList(db.T_competences, "id", "nom_competence");
            ViewBag.profil_id = new SelectList(db.T_profil, "id", "nom");
            return View();
        }

        // POST: T_profil_competences/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "profil_id,competences_id,level_competences,commentaire,marge_erreur,restriction_level_competences")] T_profil_competences t_profil_competences)
        {
            if (ModelState.IsValid)
            {
                db.T_profil_competences.Add(t_profil_competences);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.competences_id = new SelectList(db.T_competences, "id", "nom_competence", t_profil_competences.competences_id);
            ViewBag.profil_id = new SelectList(db.T_profil, "id", "nom", t_profil_competences.profil_id);
            return View(t_profil_competences);
        }

        // GET: T_profil_competences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_profil_competences t_profil_competences = db.T_profil_competences.Find(id);
            if (t_profil_competences == null)
            {
                return HttpNotFound();
            }
            ViewBag.competences_id = new SelectList(db.T_competences, "id", "nom_competence", t_profil_competences.competences_id);
            ViewBag.profil_id = new SelectList(db.T_profil, "id", "nom", t_profil_competences.profil_id);
            return View(t_profil_competences);
        }

        // POST: T_profil_competences/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "profil_id,competences_id,level_competences,commentaire,marge_erreur,restriction_level_competences")] T_profil_competences t_profil_competences)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_profil_competences).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.competences_id = new SelectList(db.T_competences, "id", "nom_competence", t_profil_competences.competences_id);
            ViewBag.profil_id = new SelectList(db.T_profil, "id", "nom", t_profil_competences.profil_id);
            return View(t_profil_competences);
        }

        // GET: T_profil_competences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_profil_competences t_profil_competences = db.T_profil_competences.Find(id);
            if (t_profil_competences == null)
            {
                return HttpNotFound();
            }
            return View(t_profil_competences);
        }

        // POST: T_profil_competences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_profil_competences t_profil_competences = db.T_profil_competences.Find(id);
            db.T_profil_competences.Remove(t_profil_competences);
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
