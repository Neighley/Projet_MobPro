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
    public class T_statutController : Controller
    {
        private Mobilite_Pro_BDDEntities db = new Mobilite_Pro_BDDEntities();

        // GET: T_statut
        public ActionResult Index()
        {
            return View(db.T_statut.ToList());
        }

        // GET: T_statut/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_statut t_statut = db.T_statut.Find(id);
            if (t_statut == null)
            {
                return HttpNotFound();
            }
            return View(t_statut);
        }

        // GET: T_statut/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: T_statut/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,statut")] T_statut t_statut)
        {
            if (ModelState.IsValid)
            {
                db.T_statut.Add(t_statut);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(t_statut);
        }

        // GET: T_statut/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_statut t_statut = db.T_statut.Find(id);
            if (t_statut == null)
            {
                return HttpNotFound();
            }
            return View(t_statut);
        }

        // POST: T_statut/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,statut")] T_statut t_statut)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_statut).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_statut);
        }

        // GET: T_statut/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_statut t_statut = db.T_statut.Find(id);
            if (t_statut == null)
            {
                return HttpNotFound();
            }
            return View(t_statut);
        }

        // POST: T_statut/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_statut t_statut = db.T_statut.Find(id);
            db.T_statut.Remove(t_statut);
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
