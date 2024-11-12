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
    public class T_languesController : Controller
    {
        private Mobilite_Pro_BDDEntities db = new Mobilite_Pro_BDDEntities();

        // GET: T_langues
        public ActionResult Index()
        {
            return View(db.T_langues.ToList());
        }

        // GET: T_langues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_langues t_langues = db.T_langues.Find(id);
            if (t_langues == null)
            {
                return HttpNotFound();
            }
            return View(t_langues);
        }

        // GET: T_langues/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: T_langues/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nom_langue")] T_langues t_langues)
        {
            if (ModelState.IsValid)
            {
                db.T_langues.Add(t_langues);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(t_langues);
        }

        // GET: T_langues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_langues t_langues = db.T_langues.Find(id);
            if (t_langues == null)
            {
                return HttpNotFound();
            }
            return View(t_langues);
        }

        // POST: T_langues/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nom_langue")] T_langues t_langues)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_langues).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_langues);
        }

        // GET: T_langues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_langues t_langues = db.T_langues.Find(id);
            if (t_langues == null)
            {
                return HttpNotFound();
            }
            return View(t_langues);
        }

        // POST: T_langues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_langues t_langues = db.T_langues.Find(id);
            db.T_langues.Remove(t_langues);
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
