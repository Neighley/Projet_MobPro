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
    public class T_entrepriseController : Controller
    {
        private Mobilite_Pro_BDDEntities db = new Mobilite_Pro_BDDEntities();

        // GET: T_entreprise
        public ActionResult Index()
        {
            // Récupération de l'ID de l'utilisateur actuel
            var currentUserId = User.Identity.GetUserId();

            // Récupération du nombre d'entreprises créés pour l'utilisateur actuel
            int entrepriseCount = db.T_entreprise.Count(p => p.AspNetUser_id == currentUserId);
            ViewBag.EntrepriseCount = entrepriseCount;

            // Récupération du rôle de l'utilisateur
            var currentUserRole = db.AspNetUsers
                                    .Where(u => u.Id == currentUserId)
                                    .Select(u => u.role_id)
                                    .FirstOrDefault();
            ViewBag.CurrentUserRoleId = currentUserRole;

            IEnumerable<T_entreprise> t_entreprise;

            if (currentUserRole == 1)
            {
                t_entreprise = db.T_entreprise
                                 .Include(t => t.T_num_tel)
                                 .Include(t => t.T_site)
                                 .ToList();
            }
            else
            {
                t_entreprise = db.T_entreprise
                                 .Include(t => t.T_num_tel)
                                 .Include(t => t.T_site)
                                 .Where(e => e.AspNetUser_id == currentUserId)
                                 .ToList();
            }

            return View(t_entreprise);
        }

        // GET: T_entreprise/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var t_entreprise = db.T_entreprise.Include(p => p.T_site).Include(p => p.T_num_tel).FirstOrDefault(p => p.id == id);

            //T_entreprise t_entreprise = db.T_entreprise.Find(id);
            if (t_entreprise == null)
            {
                return HttpNotFound();
            }
            ViewBag.Sites = t_entreprise.T_site.ToList();
            ViewBag.NumTel = t_entreprise.T_num_tel.ToList();
            ViewBag.EntrepriseId = t_entreprise.id;
            return View(t_entreprise);
        }

        // GET: T_entreprise/Create
        public ActionResult Create()
        {
            var entreprise = new T_entreprise();

            // Récupération de l'ID de l'utilisateur actuel pour gérer ses droits
            var currentUserId = User.Identity.GetUserId();
            var currentUser = db.AspNetUsers.Find(currentUserId);
            ViewBag.CurrentUserRoleId = currentUser?.role_id ?? 0;

            ViewBag.AspNetUser_id = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.role_id = new SelectList(db.T_role, "id", "nom_role");
            return View();
        }

        // POST: T_entreprise/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nom,AspNetUser_id")] T_entreprise t_entreprise)
        {
            var currentUserId = User.Identity.GetUserId();
            t_entreprise.AspNetUser_id = currentUserId;

            if (ModelState.IsValid)
            {
                db.T_entreprise.Add(t_entreprise);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = t_entreprise.id });
            }

            ViewBag.AspNetUser_id = new SelectList(db.AspNetUsers, "Id", "Email", t_entreprise.AspNetUser_id);
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
            ViewBag.AspNetUser_id = new SelectList(db.AspNetUsers, "Id", "Email", t_entreprise.AspNetUser_id);
            return View(t_entreprise);
        }

        // POST: T_entreprise/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nom,AspNetUser_id")] T_entreprise t_entreprise)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_entreprise).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AspNetUser_id = new SelectList(db.AspNetUsers, "Id", "Email", t_entreprise.AspNetUser_id);
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

            // Si un profil est supprimé, on supprime aussi les sites
            var sites = db.T_site.Where(ne => ne.entreprise_id == t_entreprise.id).ToList();
            db.T_site.RemoveRange(sites);
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
