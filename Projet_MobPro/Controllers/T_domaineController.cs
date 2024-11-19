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
    public class T_domaineController : Controller
    {
        private Mobilite_Pro_BDDEntities db = new Mobilite_Pro_BDDEntities();

        // GET: T_domaine
        public ActionResult Index()
        {
            return View(db.T_domaine.ToList());
        }

        // GET: T_domaine/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_domaine t_domaine = db.T_domaine.Find(id);
            if (t_domaine == null)
            {
                return HttpNotFound();
            }
            return View(t_domaine);
        }

        // GET: T_domaine/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: T_domaine/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,domaine")] T_domaine t_domaine)
        {
            if (ModelState.IsValid)
            {
                db.T_domaine.Add(t_domaine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(t_domaine);
        }

        // GET: T_domaine/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_domaine t_domaine = db.T_domaine.Find(id);
            if (t_domaine == null)
            {
                return HttpNotFound();
            }
            return View(t_domaine);
        }

        // POST: T_domaine/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,domaine")] T_domaine t_domaine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_domaine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_domaine);
        }

        // GET: T_domaine/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_domaine t_domaine = db.T_domaine.Find(id);
            if (t_domaine == null)
            {
                return HttpNotFound();
            }
            return View(t_domaine);
        }

        // POST: T_domaine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_domaine t_domaine = db.T_domaine.Find(id);
            db.T_domaine.Remove(t_domaine);
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
