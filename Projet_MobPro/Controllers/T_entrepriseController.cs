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
    public class T_entrepriseController : Controller
    {
        private Mobilite_Pro_BDDEntities db = new Mobilite_Pro_BDDEntities();

        // GET: T_entreprise
        public ActionResult Index()
        {
            var t_entreprise = db.T_entreprise.Include(t => t.T_num_tel).Include(t => t.T_site);
            return View(t_entreprise.ToList());
        }

        // GET: T_entreprise/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_entreprise t_entreprise = db.T_entreprise.Find(id);
            if (t_entreprise == null)
            {
                return HttpNotFound();
            }
            return View(t_entreprise);
        }

        // GET: T_entreprise/Create
        public ActionResult Create()
        {
            ViewBag.num_tel_id = new SelectList(db.T_num_tel, "id", "telephone");
            ViewBag.site_id = new SelectList(db.T_site, "id", "adresse");
            return View();
        }

        // POST: T_entreprise/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nom,num_tel_id,site_id")] T_entreprise t_entreprise)
        {
            if (ModelState.IsValid)
            {
                db.T_entreprise.Add(t_entreprise);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.num_tel_id = new SelectList(db.T_num_tel, "id", "telephone", t_entreprise.num_tel_id);
            ViewBag.site_id = new SelectList(db.T_site, "id", "adresse", t_entreprise.site_id);
            return View(t_entreprise);
        }

        // GET: T_entreprise/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_entreprise t_entreprise = db.T_entreprise.Find(id);
            if (t_entreprise == null)
            {
                return HttpNotFound();
            }
            ViewBag.num_tel_id = new SelectList(db.T_num_tel, "id", "telephone", t_entreprise.num_tel_id);
            ViewBag.site_id = new SelectList(db.T_site, "id", "adresse", t_entreprise.site_id);
            return View(t_entreprise);
        }

        // POST: T_entreprise/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nom,num_tel_id,site_id")] T_entreprise t_entreprise)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_entreprise).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.num_tel_id = new SelectList(db.T_num_tel, "id", "telephone", t_entreprise.num_tel_id);
            ViewBag.site_id = new SelectList(db.T_site, "id", "adresse", t_entreprise.site_id);
            return View(t_entreprise);
        }

        // GET: T_entreprise/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_entreprise t_entreprise = db.T_entreprise.Find(id);
            if (t_entreprise == null)
            {
                return HttpNotFound();
            }
            return View(t_entreprise);
        }

        // POST: T_entreprise/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_entreprise t_entreprise = db.T_entreprise.Find(id);
            db.T_entreprise.Remove(t_entreprise);
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
