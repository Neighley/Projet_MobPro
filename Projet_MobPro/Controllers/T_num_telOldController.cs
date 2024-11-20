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
    public class T_num_telOldController : Controller
    {
        private Mobilite_Pro_BDDEntities db = new Mobilite_Pro_BDDEntities();

        // GET: T_num_tel
        public ActionResult Index()
        {
            return View(db.T_num_tel.ToList());
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: T_num_tel/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,telephone")] T_num_tel t_num_tel)
        {
            if (ModelState.IsValid)
            {
                db.T_num_tel.Add(t_num_tel);
                db.SaveChanges();
                return RedirectToAction("Create", "T_entreprise");
            }

            return View(t_num_tel);
        }

        // GET: T_num_tel/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: T_num_tel/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,telephone")] T_num_tel t_num_tel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_num_tel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_num_tel);
        }

        // GET: T_num_tel/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: T_num_tel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_num_tel t_num_tel = db.T_num_tel.Find(id);
            db.T_num_tel.Remove(t_num_tel);
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
