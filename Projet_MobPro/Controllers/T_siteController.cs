using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Projet_MobPro.Models;

namespace Projet_MobPro.Controllers
{
    public class T_siteController : Controller
    {
        private Mobilite_Pro_BDDEntities db = new Mobilite_Pro_BDDEntities();

        // GET: T_site
        public ActionResult Index()
        {
            // Récupération de l'ID de l'utilisateur actuel
            var currentUserId = User.Identity.GetUserId();
            
            var entreprise = db.T_entreprise.FirstOrDefault(e => e.AspNetUser_id == currentUserId);

            var t_site = db.T_site
                            .Include(t => t.T_entreprise)
                            .Where(t => t.entreprise_id == entreprise.id)
                            .ToList();
            return View(t_site);
        }

        // GET: T_site/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_site t_site = db.T_site.Find(id);
            if (t_site == null)
            {
                return HttpNotFound();
            }
            return View(t_site);
        }

        // GET: T_site/Create
        public ActionResult Create(int? entrepriseId)
        {
            var entreprise = db.T_entreprise.Find(entrepriseId);

            ViewBag.EntrepriseId = entrepriseId;
            ViewBag.entreprise_id = new SelectList(db.T_entreprise, "id", "nom");
            return View();
        }

        // POST: T_site/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,adresse,code_postal,ville,entreprise_id")] T_site t_site)
        {
            if (ModelState.IsValid)
            {
                db.T_site.Add(t_site);
                db.SaveChanges();
                return RedirectToAction("Details", "T_entreprise", new { id = t_site.entreprise_id});
            }

            ViewBag.entreprise_id = new SelectList(db.T_entreprise, "id", "nom", t_site.entreprise_id);
            return View(t_site);
        }

        // GET: T_site/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_site t_site = db.T_site.Find(id);
            if (t_site == null)
            {
                return HttpNotFound();
            }
            ViewBag.entreprise_id = new SelectList(db.T_entreprise, "id", "nom", t_site.entreprise_id);
            return View(t_site);
        }

        // POST: T_site/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,adresse,code_postal,ville,entreprise_id")] T_site t_site)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_site).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.entreprise_id = new SelectList(db.T_entreprise, "id", "nom", t_site.entreprise_id);
            return View(t_site);
        }

        // GET: T_site/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_site t_site = db.T_site.Find(id);
            if (t_site == null)
            {
                return HttpNotFound();
            }
            return View(t_site);
        }

        // POST: T_site/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_site t_site = db.T_site.Find(id);
            db.T_site.Remove(t_site);
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
