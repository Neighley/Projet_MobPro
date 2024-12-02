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
    public class T_offre_emploi_languesController : Controller
    {
        private Mobilite_Pro_BDDEntities db = new Mobilite_Pro_BDDEntities();

        // GET: T_offre_emploi_langues
        public ActionResult Index()
        {
            var t_offre_emploi_langues = db.T_offre_emploi_langues.Include(t => t.T_langues).Include(t => t.T_offre_emploi);
            return View(t_offre_emploi_langues.ToList());
        }

        // GET: T_offre_emploi_langues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_offre_emploi_langues t_offre_emploi_langues = db.T_offre_emploi_langues.Find(id);
            if (t_offre_emploi_langues == null)
            {
                return HttpNotFound();
            }
            return View(t_offre_emploi_langues);
        }

        // GET: T_offre_emploi_langues/Create
        public ActionResult Create()
        {
            ViewBag.langues_id = new SelectList(db.T_langues, "id", "nom_langue");
            ViewBag.offre_emploi_id = new SelectList(db.T_offre_emploi, "id", "nom");
            return View();
        }

        // POST: T_offre_emploi_langues/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,offre_emploi_id,langues_id,commentaire")] T_offre_emploi_langues t_offre_emploi_langues)
        {
            if (ModelState.IsValid)
            {
                db.T_offre_emploi_langues.Add(t_offre_emploi_langues);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.langues_id = new SelectList(db.T_langues, "id", "nom_langue", t_offre_emploi_langues.langues_id);
            ViewBag.offre_emploi_id = new SelectList(db.T_offre_emploi, "id", "nom", t_offre_emploi_langues.offre_emploi_id);
            return View(t_offre_emploi_langues);
        }

        // GET: T_offre_emploi_langues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_offre_emploi_langues t_offre_emploi_langues = db.T_offre_emploi_langues.Find(id);
            if (t_offre_emploi_langues == null)
            {
                return HttpNotFound();
            }
            ViewBag.langues_id = new SelectList(db.T_langues, "id", "nom_langue", t_offre_emploi_langues.langues_id);
            ViewBag.offre_emploi_id = new SelectList(db.T_offre_emploi, "id", "nom", t_offre_emploi_langues.offre_emploi_id);
            return View(t_offre_emploi_langues);
        }

        // POST: T_offre_emploi_langues/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,offre_emploi_id,langues_id,commentaire")] T_offre_emploi_langues t_offre_emploi_langues)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_offre_emploi_langues).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.langues_id = new SelectList(db.T_langues, "id", "nom_langue", t_offre_emploi_langues.langues_id);
            ViewBag.offre_emploi_id = new SelectList(db.T_offre_emploi, "id", "nom", t_offre_emploi_langues.offre_emploi_id);
            return View(t_offre_emploi_langues);
        }

        // GET: T_offre_emploi_langues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_offre_emploi_langues t_offre_emploi_langues = db.T_offre_emploi_langues.Find(id);
            if (t_offre_emploi_langues == null)
            {
                return HttpNotFound();
            }
            return View(t_offre_emploi_langues);
        }

        // POST: T_offre_emploi_langues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_offre_emploi_langues t_offre_emploi_langues = db.T_offre_emploi_langues.Find(id);
            db.T_offre_emploi_langues.Remove(t_offre_emploi_langues);
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
