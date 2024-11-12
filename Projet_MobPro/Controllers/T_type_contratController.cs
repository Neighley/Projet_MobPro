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
    public class T_type_contratController : Controller
    {
        private Mobilite_Pro_BDDEntities db = new Mobilite_Pro_BDDEntities();

        // GET: T_type_contrat
        public ActionResult Index()
        {
            return View(db.T_type_contrat.ToList());
        }

        // GET: T_type_contrat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_type_contrat t_type_contrat = db.T_type_contrat.Find(id);
            if (t_type_contrat == null)
            {
                return HttpNotFound();
            }
            return View(t_type_contrat);
        }

        // GET: T_type_contrat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: T_type_contrat/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nom_type_contrat")] T_type_contrat t_type_contrat)
        {
            if (ModelState.IsValid)
            {
                db.T_type_contrat.Add(t_type_contrat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(t_type_contrat);
        }

        // GET: T_type_contrat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_type_contrat t_type_contrat = db.T_type_contrat.Find(id);
            if (t_type_contrat == null)
            {
                return HttpNotFound();
            }
            return View(t_type_contrat);
        }

        // POST: T_type_contrat/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nom_type_contrat")] T_type_contrat t_type_contrat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_type_contrat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_type_contrat);
        }

        // GET: T_type_contrat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_type_contrat t_type_contrat = db.T_type_contrat.Find(id);
            if (t_type_contrat == null)
            {
                return HttpNotFound();
            }
            return View(t_type_contrat);
        }

        // POST: T_type_contrat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_type_contrat t_type_contrat = db.T_type_contrat.Find(id);
            db.T_type_contrat.Remove(t_type_contrat);
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
