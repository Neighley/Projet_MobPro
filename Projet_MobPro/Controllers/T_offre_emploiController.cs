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
    public class T_offre_emploiController : Controller
    {
        private Mobilite_Pro_BDDEntities db = new Mobilite_Pro_BDDEntities();

        // GET: T_offre_emploi
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

            var t_offre_emploi = db.T_offre_emploi.Include(t => t.T_entreprise).Include(t => t.T_site).Include(t => t.T_statut).Include(t => t.T_type_contrat);
            return View(t_offre_emploi.ToList());
        }

        public ActionResult Affichage()
        {
            // Récupération de l'ID de l'utilisateur actuel
            var currentUserId = User.Identity.GetUserId();

            // Récupération des entreprises liées à l'utilisateur actuel
            var entreprisesIds = db.T_entreprise
                            .Where(e => e.AspNetUser_id == currentUserId)
                            .Select(e => e.id)
                            .ToList();
            var offresCount = db.T_offre_emploi
                .Count(o => o.entreprise_id.HasValue && entreprisesIds.Contains(o.entreprise_id.Value));

            ViewBag.OffresCount = offresCount;

            // Récupération du rôle de l'utilisateur
            var currentUserRole = db.AspNetUsers
                                    .Where(u => u.Id == currentUserId)
                                    .Select(u => u.role_id)
                                    .FirstOrDefault();
            ViewBag.CurrentUserRoleId = currentUserRole;

            IEnumerable<T_offre_emploi> t_offre_emploi;

            /* Si role_id == 3, afficher les offres des entreprises de l'utilisateur actuel
            * Sinon (role_id == 1 ou 2 soit admin ou rh), afficher toutes les offres) */
            if (currentUserRole == 3)
            {
                // Afficher uniquement les offres des entreprises de l'utilisateur connecté
                t_offre_emploi = db.T_offre_emploi
                    .Include(t => t.T_entreprise)
                    .Include(t => t.T_site)
                    .Include(t => t.T_statut)
                    .Include(t => t.T_type_contrat)
                    .Where(o => o.entreprise_id.HasValue && entreprisesIds.Contains(o.entreprise_id.Value))
                    .ToList();
            }
            else
            {
                // Afficher toutes les offres pour les autres rôles
                t_offre_emploi = db.T_offre_emploi
                    .Include(t => t.T_entreprise)
                    .Include(t => t.T_site)
                    .Include(t => t.T_statut)
                    .Include(t => t.T_type_contrat)
                    .ToList();
            }

            return View(t_offre_emploi);
        }

        // GET: T_offre_emploi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_offre_emploi t_offre_emploi = db.T_offre_emploi.Find(id);
            if (t_offre_emploi == null)
            {
                return HttpNotFound();
            }
            return View(t_offre_emploi);
        }

        // GET: T_offre_emploi/Create
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            var userEntreprises = db.T_entreprise.Where(e => e.AspNetUser_id == userId).ToList();

            // Récupération des IDs des entreprises
            var entrepriseIds = userEntreprises.Select(e => e.id).ToList();

            // Filtrage des sites liés aux entreprises de l'utilisateur
            var userSites = db.T_site.Where(s => entrepriseIds.Contains(s.entreprise_id ?? 0)).ToList();

            ViewBag.EntrepriseId = new SelectList(userEntreprises, "id", "nom");
            ViewBag.entreprise_id = new SelectList(db.T_entreprise, "id", "nom");
            ViewBag.site_id = new SelectList(userSites, "id", "adresse");
            ViewBag.statut_id = new SelectList(db.T_statut, "id", "statut");
            ViewBag.type_contrat_id = new SelectList(db.T_type_contrat, "id", "nom_type_contrat");
            return View();
        }

        // POST: T_offre_emploi/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nom,description,date_publication,date_suppression,ouvert_externe,url_site,teletravail_autorise,full_teletravail,site_id,statut_id,entreprise_id,type_contrat_id,niveau_experience_id")] T_offre_emploi t_offre_emploi)
        {
            if (ModelState.IsValid)
            {
                db.T_offre_emploi.Add(t_offre_emploi);
                db.SaveChanges();
                return RedirectToAction("Affichage");
            }

            ViewBag.entreprise_id = new SelectList(db.T_entreprise, "id", "nom", t_offre_emploi.entreprise_id);
            ViewBag.site_id = new SelectList(db.T_site, "id", "adresse", t_offre_emploi.site_id);
            ViewBag.statut_id = new SelectList(db.T_statut, "id", "statut", t_offre_emploi.statut_id);
            ViewBag.type_contrat_id = new SelectList(db.T_type_contrat, "id", "nom_type_contrat", t_offre_emploi.type_contrat_id);
            return View(t_offre_emploi);
        }

        // GET: T_offre_emploi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_offre_emploi t_offre_emploi = db.T_offre_emploi.Find(id);
            if (t_offre_emploi == null)
            {
                return HttpNotFound();
            }
            ViewBag.entreprise_id = new SelectList(db.T_entreprise, "id", "nom", t_offre_emploi.entreprise_id);
            ViewBag.site_id = new SelectList(db.T_site, "id", "adresse", t_offre_emploi.site_id);
            ViewBag.statut_id = new SelectList(db.T_statut, "id", "statut", t_offre_emploi.statut_id);
            ViewBag.type_contrat_id = new SelectList(db.T_type_contrat, "id", "nom_type_contrat", t_offre_emploi.type_contrat_id);
            return View(t_offre_emploi);
        }

        // POST: T_offre_emploi/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nom,description,date_publication,date_suppression,ouvert_externe,url_site,teletravail_autorise,full_teletravail,site_id,statut_id,entreprise_id,type_contrat_id,niveau_experience_id")] T_offre_emploi t_offre_emploi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_offre_emploi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.entreprise_id = new SelectList(db.T_entreprise, "id", "nom", t_offre_emploi.entreprise_id);
            ViewBag.site_id = new SelectList(db.T_site, "id", "adresse", t_offre_emploi.site_id);
            ViewBag.statut_id = new SelectList(db.T_statut, "id", "statut", t_offre_emploi.statut_id);
            ViewBag.type_contrat_id = new SelectList(db.T_type_contrat, "id", "nom_type_contrat", t_offre_emploi.type_contrat_id);
            return View(t_offre_emploi);
        }

        // GET: T_offre_emploi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_offre_emploi t_offre_emploi = db.T_offre_emploi.Find(id);
            if (t_offre_emploi == null)
            {
                return HttpNotFound();
            }
            return View(t_offre_emploi);
        }

        // POST: T_offre_emploi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_offre_emploi t_offre_emploi = db.T_offre_emploi.Find(id);
            db.T_offre_emploi.Remove(t_offre_emploi);
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
