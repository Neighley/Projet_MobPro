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
        public ActionResult Index()
        {
            // Récupération de l'ID de l'utilisateur actuel
            var currentUserId = User.Identity.GetUserId();

            // Récupération du nombre de profils créés
            int profilCount = db.T_profil.Count();
            ViewBag.ProfilCount = profilCount;

            // Récupération du rôle de l'utilisateur
            var currentUserRole = db.AspNetUsers
                                    .Where(u => u.Id == currentUserId)
                                    .Select(u => u.role_id)
                                    .FirstOrDefault();

            IEnumerable<T_profil> t_profil;

            /* Si role_id == 3, afficher le profil de l'utilisateur actuel
             * Sinon (role_id == 1 ou 2 soit admin ou rh), afficher tous les profils) */
            if (currentUserRole == 3)
            {
                // Afficher uniquement le profil de l'utilisateur connecté
                t_profil = db.T_profil
                             .Include(t => t.AspNetUsers)
                             .Include(t => t.T_niveau_experience)
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
                             .Include(t => t.T_niveau_experience)
                             .Include(t => t.T_role)
                             .Include(t => t.T_type_contrat)
                             .ToList();
            }

            return View(t_profil);
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
        public ActionResult Create(int? niveau_experience_id)
        {
            var profil = new T_profil();

            if (niveau_experience_id.HasValue)
            {
                ViewBag.NiveauExperienceId = niveau_experience_id.Value;
            }

            // Restaurer les données depuis TempData
            if (TempData["Nom"] != null)
            {
                profil.nom = TempData["Nom"] as string;
                TempData.Keep("Nom");
            }

            if (TempData["Prenom"] != null)
            {
                profil.prenom = TempData["Prenom"] as string;
                TempData.Keep("Prenom");
            }

            if (TempData["Date_Naissance"] != null)
            {
                profil.date_naissance = (DateTime)TempData["Date_Naissance"];
                TempData.Keep("Date_Naissance");
            }

            if (TempData["Code_Postal"] != null)
            {
                profil.code_postal = TempData["Code_Postal"] as string;
                TempData.Keep("Code_Postal");
            }

            if (TempData["Ville"] != null)
            {
                profil.ville = TempData["Ville"] as string;
                TempData.Keep("Ville");
            }

            if (TempData["Role_Id"] != null)
            {
                profil.role_id = (int)TempData["Role_Id"];
                TempData.Keep("Role_Id");
            }

            if (TempData["Type_Contrat_Id"] != null)
            {
                profil.type_contrat_id = (int)TempData["Type_Contrat_Id"];
                TempData.Keep("Type_Contrat_Id");
            }

            if (TempData["Niveau_Experience_Id"] != null)
            {
                profil.niveau_experience_id = (int)TempData["Niveau_Experience_Id"];
                TempData.Keep("Niveau_Experience_Id");
            }

            if (TempData["AspNetUser_Id"] != null)
            {
                profil.AspNetUser_id = TempData["AspNetUser_Id"] as string;
                TempData.Keep("AspNetUser_Id");
            }

            var niveaux = db.T_niveau_experience.ToList();
            ViewBag.NiveauxExperience = niveaux;

            // Récupération de l'ID de l'utilisateur actuel pour gérer ses droits
            var currentUserId = User.Identity.GetUserId();
            var currentUser = db.AspNetUsers.Find(currentUserId);

            ViewBag.CurrentUserRoleId = currentUser?.role_id ?? 0;

            ViewBag.AspNetUser_id = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.niveau_experience_id = new SelectList(db.T_niveau_experience, "id", "nom_niveau_experience");
            ViewBag.role_id = new SelectList(db.T_role, "id", "nom_role");
            ViewBag.type_contrat_id = new SelectList(db.T_type_contrat, "id", "nom_type_contrat");

            return View();
        }


        // POST: T_profil/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nom,prenom,date_naissance,adresse,code_postal,ville,ruelle_p,role_id,type_contrat_id,niveau_experience_id,AspNetUser_id")] T_profil t_profil)
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
                // Ajouter le profil à la base et rediriger vers l'index
                db.T_profil.Add(t_profil);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            /* Sauvegarde des données saisies dans TempData pour gérer la redirection sur T_niv_exp
             Et ainsi garder en mémoire les champs précédemment entrés */
            TempData["Nom"] = t_profil.nom;
            TempData["Prenom"] = t_profil.prenom;
            TempData["Date_Naissance"] = t_profil.date_naissance;
            TempData["Code_Postal"] = t_profil.code_postal;
            TempData["Ville"] = t_profil.ville;
            TempData["Role_Id"] = t_profil.role_id;
            TempData["Type_Contrat_Id"] = t_profil.type_contrat_id;
            TempData["Niveau_Experience_Id"] = t_profil.niveau_experience_id;
            TempData["AspNetUser_Id"] = t_profil.AspNetUser_id;

            ViewBag.AspNetUser_id = new SelectList(db.AspNetUsers, "Id", "Email", t_profil.AspNetUser_id);
            ViewBag.niveau_experience_id = new SelectList(db.T_niveau_experience, "id", "nom_niveau_experience", t_profil.niveau_experience_id);
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
            ViewBag.niveau_experience_id = new SelectList(db.T_niveau_experience, "id", "nom_niveau_experience", t_profil.niveau_experience_id);
            ViewBag.role_id = new SelectList(db.T_role, "id", "nom_role", t_profil.role_id);
            ViewBag.type_contrat_id = new SelectList(db.T_type_contrat, "id", "nom_type_contrat", t_profil.type_contrat_id);
            return View(t_profil);
        }

        // POST: T_profil/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nom,prenom,date_naissance,adresse,code_postal,ville,ruelle_p,role_id,type_contrat_id,niveau_experience_id,AspNetUser_id")] T_profil t_profil)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_profil).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AspNetUser_id = new SelectList(db.AspNetUsers, "Id", "Email", t_profil.AspNetUser_id);
            ViewBag.niveau_experience_id = new SelectList(db.T_niveau_experience, "id", "nom_niveau_experience", t_profil.niveau_experience_id);
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
