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
    public class T_profilController : Controller
    {
        private Mobilite_Pro_BDDEntities db = new Mobilite_Pro_BDDEntities();

        // GET: T_profil
        public ActionResult Affichage()
        {
            // Récupération de l'ID de l'utilisateur actuel
            var currentUserId = User.Identity.GetUserId();

            // Récupération du nombre de profils créés pour l'utilisateur actuel
            int profilCount = db.T_profil.Count(p => p.AspNetUser_id == currentUserId);
            ViewBag.ProfilCount = profilCount;

            // Récupération du rôle de l'utilisateur
            var currentUserRole = db.AspNetUsers
                                    .Where(u => u.Id == currentUserId)
                                    .Select(u => u.role_id)
                                    .FirstOrDefault();
            ViewBag.CurrentUserRoleId = currentUserRole;

            IEnumerable<T_profil> t_profil;

            /* Si role_id == 3, afficher le profil de l'utilisateur actuel
             * Sinon (role_id == 1 ou 2 soit admin ou rh), afficher tous les profils) */
            if (currentUserRole == 3)
            {
                // Afficher uniquement le profil de l'utilisateur connecté
                t_profil = db.T_profil
                             .Include(t => t.AspNetUsers)
                             .Include(t => t.T_role)
                             .Include(t => t.T_type_contrat)
                             .Where(p => p.AspNetUser_id == currentUserId)
                             .ToList();
            }
            else
            {
                // Afficher tous les profils pour les autres rôles
                t_profil = db.T_profil
                             .Include(t => t.AspNetUsers)
                             .Include(t => t.T_role)
                             .Include(t => t.T_type_contrat)
                             .ToList();
            }

            return View(t_profil);
        }

        public ActionResult Index()
        {
            // Récupération de l'ID de l'utilisateur actuel
            var currentUserId = User.Identity.GetUserId();

            // Récupération du rôle de l'utilisateur
            var currentUserRole = db.AspNetUsers
                                    .Where(u => u.Id == currentUserId)
                                    .Select(u => u.role_id)
                                    .FirstOrDefault();
            ViewBag.CurrentUserRoleId = currentUserRole;

            IEnumerable<T_profil> t_profil;

            t_profil = db.T_profil
                         .Include(t => t.AspNetUsers)
                         .Include(t => t.T_role)
                         .Include(t => t.T_type_contrat)
                         .ToList();

            return View(t_profil);
        }

        // GET: T_profil/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var t_profil = db.T_profil.Include(p => p.T_niveau_experience.Select(n => n.T_domaine)).FirstOrDefault(p => p.id == id);

            //T_profil t_profil = db.T_profil.Find(id);
            if (t_profil == null)
            {
                return HttpNotFound();
            }

            ViewBag.NiveauxExperience = t_profil.T_niveau_experience.ToList();
            ViewBag.Domaines = new SelectList(db.T_domaine, "id", "nom_domaine");
            ViewBag.ProfilId = t_profil.id;

            return View(t_profil);
        }

        // GET: T_profil/Create
        public ActionResult Create()
        {
            var profil = new T_profil();

            // Récupération de l'ID de l'utilisateur actuel pour gérer ses droits
            var currentUserId = User.Identity.GetUserId();
            var currentUser = db.AspNetUsers.Find(currentUserId);
            ViewBag.CurrentUserRoleId = currentUser?.role_id ?? 0;

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
            var currentUserId = User.Identity.GetUserId();
            t_profil.AspNetUser_id = currentUserId;

            if (db.T_profil.Any(p => p.AspNetUser_id == currentUserId))
            {
                ModelState.AddModelError("", "Un profil existe déjà pour cet utilisateur.");
                return View(t_profil);
            }

            if (ModelState.IsValid)
            {
                db.T_profil.Add(t_profil);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = t_profil.id });
            }

            ViewBag.AspNetUser_id = new SelectList(db.AspNetUsers, "Id", "Email", t_profil.AspNetUser_id);
            ViewBag.role_id = new SelectList(db.T_role, "id", "nom_role", t_profil.role_id);
            ViewBag.type_contrat_id = new SelectList(db.T_type_contrat, "id", "nom_type_contrat", t_profil.type_contrat_id);
            return View(t_profil);
        }

        // GET: T_profil/Edit/5
        public ActionResult Edit(int? id)
        {
            var currentUserId = User.Identity.GetUserId();

            var currentUser = db.AspNetUsers.Where(u => u.Id == currentUserId).ToList();

            var currentUserRole = db.AspNetUsers
                                    .Where(u => u.Id == currentUserId)
                                    .Select(u => u.role_id)
                                    .FirstOrDefault();
            ViewBag.CurrentUserRoleId = currentUserRole;

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
            ViewBag.AspNetUser_id2 = new SelectList(currentUser, "Id", "Email", t_profil.AspNetUser_id);
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
                return RedirectToAction("Affichage");
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

            // Si un profil est supprimé, on supprime aussi les niveaux d'expérience
            var niveauxExperience = db.T_niveau_experience.Where(ne => ne.profil_id == t_profil.id).ToList();
            db.T_niveau_experience.RemoveRange(niveauxExperience);
            db.T_profil.Remove(t_profil);
            db.SaveChanges();
            return RedirectToAction("Affichage");
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
