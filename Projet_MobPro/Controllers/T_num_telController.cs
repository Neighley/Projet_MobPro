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
    public class T_num_telController : Controller
    {
        private Mobilite_Pro_BDDEntities db = new Mobilite_Pro_BDDEntities();

        // GET: T_num_tel
        public ActionResult Index()
        {
            // Récupération de l'ID de l'utilisateur actuel
            var currentUserId = User.Identity.GetUserId();

            var entreprise = db.T_entreprise.FirstOrDefault(e => e.AspNetUser_id == currentUserId);

            var t_num_tel = db.T_num_tel
                            .Include(t => t.T_entreprise)
                            .Where(t => t.entreprise_id == entreprise.id)
                            .ToList();
            return View(t_num_tel);
        }

        // GET: T_num_tel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_num_tel t_num_tel = db.T_num_tel.Find(id);
            if (t_num_tel == null)
            {
                return HttpNotFound();
            }
            return View(t_num_tel);
        }

        // GET: T_num_tel/Create
        public ActionResult Create(int? entrepriseId)
        {
            var entreprise = db.T_entreprise.Find(entrepriseId);

            ViewBag.EntrepriseId = entrepriseId;
            ViewBag.entreprise_id = new SelectList(db.T_entreprise, "id", "nom");
            return View();
        }

        // POST: T_num_tel/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int entrepriseId, [Bind(Include = "id,telephone,entreprise_id")] T_num_tel t_num_tel)
        {
            if (ModelState.IsValid)
            {
                t_num_tel.entreprise_id = entrepriseId;
                db.T_num_tel.Add(t_num_tel);
                db.SaveChanges();
                return RedirectToAction("Details", "T_entreprise", new { id = entrepriseId });
            }

            ViewBag.entreprise_id = new SelectList(db.T_entreprise, "id", "nom", t_num_tel.entreprise_id);
            return View(t_num_tel);
        }

        // GET: T_num_tel/Edit/5
        public ActionResult Edit(int? entrepriseId, int? id)
        {
            var entreprise = db.T_entreprise.Find(entrepriseId);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_num_tel t_num_tel = db.T_num_tel.Find(id);
            if (t_num_tel == null)
            {
                return HttpNotFound();
            }
            ViewBag.EntrepriseId = entrepriseId;
            ViewBag.entreprise_id = new SelectList(db.T_entreprise, "id", "nom", t_num_tel.entreprise_id);
            return View(t_num_tel);
        }

        // POST: T_num_tel/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int entrepriseId, [Bind(Include = "id,telephone,entreprise_id")] T_num_tel t_num_tel)
        {
            if (ModelState.IsValid)
            {
                t_num_tel.entreprise_id = entrepriseId;
                db.Entry(t_num_tel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "T_entreprise", new { id = entrepriseId });
            }
            ViewBag.entreprise_id = new SelectList(db.T_entreprise, "id", "nom", t_num_tel.entreprise_id);
            return View(t_num_tel);
        }

        // GET: T_num_tel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_num_tel t_num_tel = db.T_num_tel.Include(t => t.T_entreprise).FirstOrDefault(t => t.id == id);
            if (t_num_tel == null)
            {
                return HttpNotFound();
            }
            ViewBag.EntrepriseId = t_num_tel.entreprise_id;
            return View(t_num_tel);
        }

        // POST: T_num_tel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int entrepriseId)
        {
            T_num_tel t_num_tel = db.T_num_tel.Find(id);
            db.T_num_tel.Remove(t_num_tel);
            db.SaveChanges();
            return RedirectToAction("Details", "T_entreprise", new { id = entrepriseId });
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
