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
    public class T_offre_emploi_competencesController : Controller
    {
        private Mobilite_Pro_BDDEntities db = new Mobilite_Pro_BDDEntities();

        // GET: T_offre_emploi_competences
        public ActionResult Index()
        {
            var t_offre_emploi_competences = db.T_offre_emploi_competences.Include(t => t.T_competences).Include(t => t.T_offre_emploi);
            return View(t_offre_emploi_competences.ToList());
        }

        // GET: T_offre_emploi_competences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_offre_emploi_competences t_offre_emploi_competences = db.T_offre_emploi_competences.Find(id);
            if (t_offre_emploi_competences == null)
            {
                return HttpNotFound();
            }
            return View(t_offre_emploi_competences);
        }

        // GET: T_offre_emploi_competences/Create
        public ActionResult Create()
        {
            ViewBag.competences_id = new SelectList(db.T_competences, "id", "nom_competence");
            ViewBag.offre_emploi_id = new SelectList(db.T_offre_emploi, "id", "nom");
            return View();
        }

        // POST: T_offre_emploi_competences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,offre_emploi_id,competences_id,commentaire")] T_offre_emploi_competences t_offre_emploi_competences)
        {
            if (ModelState.IsValid)
            {
                db.T_offre_emploi_competences.Add(t_offre_emploi_competences);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.competences_id = new SelectList(db.T_competences, "id", "nom_competence", t_offre_emploi_competences.competences_id);
            ViewBag.offre_emploi_id = new SelectList(db.T_offre_emploi, "id", "nom", t_offre_emploi_competences.offre_emploi_id);
            return View(t_offre_emploi_competences);
        }

        // GET: T_offre_emploi_competences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_offre_emploi_competences t_offre_emploi_competences = db.T_offre_emploi_competences.Find(id);
            if (t_offre_emploi_competences == null)
            {
                return HttpNotFound();
            }
            ViewBag.competences_id = new SelectList(db.T_competences, "id", "nom_competence", t_offre_emploi_competences.competences_id);
            ViewBag.offre_emploi_id = new SelectList(db.T_offre_emploi, "id", "nom", t_offre_emploi_competences.offre_emploi_id);
            return View(t_offre_emploi_competences);
        }

        // POST: T_offre_emploi_competences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,offre_emploi_id,competences_id,commentaire")] T_offre_emploi_competences t_offre_emploi_competences)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_offre_emploi_competences).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.competences_id = new SelectList(db.T_competences, "id", "nom_competence", t_offre_emploi_competences.competences_id);
            ViewBag.offre_emploi_id = new SelectList(db.T_offre_emploi, "id", "nom", t_offre_emploi_competences.offre_emploi_id);
            return View(t_offre_emploi_competences);
        }

        // GET: T_offre_emploi_competences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_offre_emploi_competences t_offre_emploi_competences = db.T_offre_emploi_competences.Find(id);
            if (t_offre_emploi_competences == null)
            {
                return HttpNotFound();
            }
            return View(t_offre_emploi_competences);
        }

        // POST: T_offre_emploi_competences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_offre_emploi_competences t_offre_emploi_competences = db.T_offre_emploi_competences.Find(id);
            db.T_offre_emploi_competences.Remove(t_offre_emploi_competences);
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
