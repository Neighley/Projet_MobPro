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
    public class T_profilController : Controller
    {
        private Mobilite_Pro_BDDEntities db = new Mobilite_Pro_BDDEntities();

        // GET: T_profil
        public ActionResult Index()
        {
            var t_profil = db.T_profil.Include(t => t.AspNetUsers).Include(t => t.T_role).Include(t => t.T_type_contrat);
            return View(t_profil.ToList());
        }

        // GET: T_profil/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_profil t_profil = db.T_profil.Find(id);
            if (t_profil == null)
            {
                return HttpNotFound();
            }
            return View(t_profil);
        }

        // GET: T_profil/Create
        public ActionResult Create()
        {
            ViewBag.AspNetUser_id = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.role_id = new SelectList(db.T_role, "id", "nom_role");
            ViewBag.type_contrat_id = new SelectList(db.T_type_contrat, "id", "nom_type_contrat");
            return View();
        }

        // POST: T_profil/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nom,prenom,date_naissance,adresse,code_postal,ville,ruelle_p,role_id,type_contrat_id,AspNetUser_id")] T_profil t_profil)
        {
            if (ModelState.IsValid)
            {
                db.T_profil.Add(t_profil);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AspNetUser_id = new SelectList(db.AspNetUsers, "Id", "Email", t_profil.AspNetUser_id);
            ViewBag.role_id = new SelectList(db.T_role, "id", "nom_role", t_profil.role_id);
            ViewBag.type_contrat_id = new SelectList(db.T_type_contrat, "id", "nom_type_contrat", t_profil.type_contrat_id);
            return View(t_profil);
        }

        // GET: T_profil/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_profil t_profil = db.T_profil.Find(id);
            if (t_profil == null)
            {
                return HttpNotFound();
            }
            ViewBag.AspNetUser_id = new SelectList(db.AspNetUsers, "Id", "Email", t_profil.AspNetUser_id);
            ViewBag.role_id = new SelectList(db.T_role, "id", "nom_role", t_profil.role_id);
            ViewBag.type_contrat_id = new SelectList(db.T_type_contrat, "id", "nom_type_contrat", t_profil.type_contrat_id);
            return View(t_profil);
        }

        // POST: T_profil/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nom,prenom,date_naissance,adresse,code_postal,ville,ruelle_p,role_id,type_contrat_id,AspNetUser_id")] T_profil t_profil)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_profil).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AspNetUser_id = new SelectList(db.AspNetUsers, "Id", "Email", t_profil.AspNetUser_id);
            ViewBag.role_id = new SelectList(db.T_role, "id", "nom_role", t_profil.role_id);
            ViewBag.type_contrat_id = new SelectList(db.T_type_contrat, "id", "nom_type_contrat", t_profil.type_contrat_id);
            return View(t_profil);
        }

        // GET: T_profil/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_profil t_profil = db.T_profil.Find(id);
            if (t_profil == null)
            {
                return HttpNotFound();
            }
            return View(t_profil);
        }

        // POST: T_profil/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_profil t_profil = db.T_profil.Find(id);
            db.T_profil.Remove(t_profil);
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
