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
    public class T_siteController : Controller
    {
        private Mobilite_Pro_BDDEntities db = new Mobilite_Pro_BDDEntities();

        // GET: T_site
        public ActionResult Index()
        {
            return View(db.T_site.ToList());
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: T_site/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,adresse,code_postal,ville")] T_site t_site)
        {
            if (ModelState.IsValid)
            {
                db.T_site.Add(t_site);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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
            return View(t_site);
        }

        // POST: T_site/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,adresse,code_postal,ville")] T_site t_site)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_site).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
