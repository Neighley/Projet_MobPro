using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projet_MobPro.Models;

namespace Projet_MobPro.Controllers
{
    public class T_profil_languesController : Controller
    {
        private Mobilite_Pro_BDDEntities db = new Mobilite_Pro_BDDEntities();

        // GET: T_profil_langues
        public ActionResult Index()
        {
            var t_profil_langues = db.T_profil_langues.Include(t => t.T_langues).Include(t => t.T_profil);
            return View(t_profil_langues.ToList());
        }

        // GET: T_profil_langues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_profil_langues t_profil_langues = db.T_profil_langues.Find(id);
            if (t_profil_langues == null)
            {
                return HttpNotFound();
            }
            return View(t_profil_langues);
        }

        // GET: T_profil_langues/Create
        public ActionResult Create(int? profilId)
        {
            var profil = db.T_profil.Find(profilId);

            ViewBag.ProfilId = profilId;
            ViewBag.langues_id = new SelectList(db.T_langues, "id", "nom_langue");
            ViewBag.profil_id = new SelectList(db.T_profil, "id", "nom");
            return View();
        }

        // POST: T_profil_langues/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int profilId, [Bind(Include = "profil_id,langues_id,level_langues,commentaire")] T_profil_langues t_profil_langues)
        {
            if (ModelState.IsValid)
            {
                t_profil_langues.profil_id = profilId;
                db.T_profil_langues.Add(t_profil_langues);
                db.SaveChanges();
                return RedirectToAction("Details", "T_Profil", new { id = ViewBag.id_profil });
            }

            ViewBag.langues_id = new SelectList(db.T_langues, "id", "nom_langue", t_profil_langues.langues_id);
            ViewBag.profil_id = new SelectList(db.T_profil, "id", "nom", t_profil_langues.profil_id);
            ViewBag.id_profil = profilId;
            return View(t_profil_langues);
        }

        // GET: T_profil_langues/Edit/5
        public ActionResult Edit(int?profilId, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_profil_langues t_profil_langues = db.T_profil_langues.Find(id);
            if (t_profil_langues == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProfilId = profilId;
            ViewBag.langues_id = new SelectList(db.T_langues, "id", "nom_langue", t_profil_langues.langues_id);
            ViewBag.profil_id = new SelectList(db.T_profil, "id", "nom", t_profil_langues.profil_id);
            return View(t_profil_langues);
        }

        // POST: T_profil_langues/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int profilId, [Bind(Include = "id,profil_id,langues_id,level_langues,commentaire")] T_profil_langues t_profil_langues)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_profil_langues).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "T_Profil", new { id = profilId });
            }
            ViewBag.langues_id = new SelectList(db.T_langues, "id", "nom_langue", t_profil_langues.langues_id);
            ViewBag.profil_id = new SelectList(db.T_profil, "id", "nom", t_profil_langues.profil_id);
            ViewBag.id_profil = profilId;
            return View(t_profil_langues);
        }

        // GET: T_profil_langues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_profil_langues t_profil_langues = db.T_profil_langues.Find(id);
            if (t_profil_langues == null)
            {
                return HttpNotFound();
            }
            return View(t_profil_langues);
        }

        // POST: T_profil_langues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_profil_langues t_profil_langues = db.T_profil_langues.Find(id);
            db.T_profil_langues.Remove(t_profil_langues);
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
