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
    public class T_roleController : Controller
    {
        private Mobilite_Pro_BDDEntities db = new Mobilite_Pro_BDDEntities();

        // GET: T_role
        public ActionResult Index()
        {
            return View(db.T_role.ToList());
        }

        // GET: T_role/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_role t_role = db.T_role.Find(id);
            if (t_role == null)
            {
                return HttpNotFound();
            }
            return View(t_role);
        }

        // GET: T_role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: T_role/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nom_role")] T_role t_role)
        {
            if (ModelState.IsValid)
            {
                db.T_role.Add(t_role);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(t_role);
        }

        // GET: T_role/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_role t_role = db.T_role.Find(id);
            if (t_role == null)
            {
                return HttpNotFound();
            }
            return View(t_role);
        }

        // POST: T_role/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nom_role")] T_role t_role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_role);
        }

        // GET: T_role/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_role t_role = db.T_role.Find(id);
            if (t_role == null)
            {
                return HttpNotFound();
            }
            return View(t_role);
        }

        // POST: T_role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_role t_role = db.T_role.Find(id);
            db.T_role.Remove(t_role);
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
