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
    public class T_niveau_experienceController : Controller
    {
        private Mobilite_Pro_BDDEntities db = new Mobilite_Pro_BDDEntities();

        // GET: T_niveau_experience
        public ActionResult Index()
        {
            var currentUserId = User.Identity.GetUserId();

            var t_niveau_experience = db.T_niveau_experience.Include(t => t.T_domaine).Include(t => t.T_profil).Include(t => t.T_offre_emploi);
            return View(t_niveau_experience.ToList());
        }

        // GET: T_niveau_experience/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_niveau_experience t_niveau_experience = db.T_niveau_experience.Find(id);
            if (t_niveau_experience == null)
            {
                return HttpNotFound();
            }
            return View(t_niveau_experience);
        }

        // GET: T_niveau_experience/Create
        public ActionResult Create(int? profilId)
        {
            var profil = db.T_profil.Find(profilId);

            ViewBag.ProfilId = profilId;
            ViewBag.domaine_id = new SelectList(db.T_domaine, "id", "domaine");
            ViewBag.profil_id = new SelectList(db.T_profil, "id", "nom");
            ViewBag.offre_emploi_id = new SelectList(db.T_offre_emploi, "id", "nom");
            return View();
        }

        // POST: T_niveau_experience/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int profilId, [Bind(Include = "id,nom_niveau_experience,domaine_id,profil_id,offre_emploi_id")] T_niveau_experience niveauExperience)
        {
            if (ModelState.IsValid)
            {
                niveauExperience.profil_id = profilId;
                db.T_niveau_experience.Add(niveauExperience);
                db.SaveChanges();
                return RedirectToAction("Details", "T_Profil", new { id = profilId });
            }

            ViewBag.domaine_id = new SelectList(db.T_domaine, "id", "domaine", niveauExperience.domaine_id);
            ViewBag.profil_id = new SelectList(db.T_profil, "id", "nom", niveauExperience.profil_id);
            ViewBag.offre_emploi_id = new SelectList(db.T_offre_emploi, "id", "nom", niveauExperience.offre_emploi_id);
            return View(niveauExperience);
        }

        // GET: T_niveau_experience/Create_Offre pour offres d'emploi
        public ActionResult Create_Offre(int? offreEmploiId)
        {
            var offre = db.T_offre_emploi.Find(offreEmploiId);

            ViewBag.OffreId = offreEmploiId;
            ViewBag.domaine_id = new SelectList(db.T_domaine, "id", "domaine");
            ViewBag.profil_id = new SelectList(db.T_profil, "id", "nom");
            ViewBag.offre_emploi_id = new SelectList(db.T_offre_emploi, "id", "nom");
            return View();
        }

        // POST: T_niveau_experience/Create_Offre pour offres d'emploi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_Offre(int offreEmploiId, [Bind(Include = "id,nom_niveau_experience,domaine_id,profil_id,offre_emploi_id")] T_niveau_experience niveauExperience)
        {
            if (ModelState.IsValid)
            {
                niveauExperience.offre_emploi_id = offreEmploiId;
                db.T_niveau_experience.Add(niveauExperience);
                db.SaveChanges();
                return RedirectToAction("Details", "T_offre_emploi", new { id = offreEmploiId });
            }

            ViewBag.domaine_id = new SelectList(db.T_domaine, "id", "domaine", niveauExperience.domaine_id);
            ViewBag.profil_id = new SelectList(db.T_profil, "id", "nom", niveauExperience.profil_id);
            ViewBag.offre_emploi_id = new SelectList(db.T_offre_emploi, "id", "nom", niveauExperience.offre_emploi_id);
            return View(niveauExperience);
        }

        // GET: T_niveau_experience/Edit/5
        public ActionResult Edit(int? profilId, int? id)
        {
            var profil = db.T_entreprise.Find(profilId);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_niveau_experience t_niveau_experience = db.T_niveau_experience.Find(id);
            if (t_niveau_experience == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProfilId = profilId;
            ViewBag.domaine_id = new SelectList(db.T_domaine, "id", "domaine", t_niveau_experience.domaine_id);
            ViewBag.profil_id = new SelectList(db.T_profil, "id", "nom", t_niveau_experience.profil_id);
            ViewBag.offre_emploi_id = new SelectList(db.T_offre_emploi, "id", "nom", t_niveau_experience.offre_emploi_id);
            return View(t_niveau_experience);
        }

        // POST: T_niveau_experience/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int profilId, [Bind(Include = "id,nom_niveau_experience,domaine_id,profil_id,offre_emploi_id")] T_niveau_experience t_niveau_experience)
        {
            if (ModelState.IsValid)
            {
                t_niveau_experience.profil_id = profilId;
                db.Entry(t_niveau_experience).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "T_profil", new { id = profilId });
            }
            ViewBag.domaine_id = new SelectList(db.T_domaine, "id", "domaine", t_niveau_experience.domaine_id);
            ViewBag.profil_id = new SelectList(db.T_profil, "id", "nom", t_niveau_experience.profil_id);
            ViewBag.offre_emploi_id = new SelectList(db.T_offre_emploi, "id", "nom", t_niveau_experience.offre_emploi_id);
            return View(t_niveau_experience);
        }

        // GET: T_niveau_experience/Edit_Offre/5 pour offres d'emploi
        public ActionResult Edit_Offre(int? offreEmploiId, int? id)
        {
            var offre = db.T_entreprise.Find(offreEmploiId);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_niveau_experience t_niveau_experience = db.T_niveau_experience.Find(id);
            if (t_niveau_experience == null)
            {
                return HttpNotFound();
            }

            ViewBag.OffreId = offreEmploiId;
            ViewBag.domaine_id = new SelectList(db.T_domaine, "id", "domaine", t_niveau_experience.domaine_id);
            ViewBag.profil_id = new SelectList(db.T_profil, "id", "nom", t_niveau_experience.profil_id);
            ViewBag.offre_emploi_id = new SelectList(db.T_offre_emploi, "id", "nom", t_niveau_experience.offre_emploi_id);
            return View(t_niveau_experience);
        }

        // POST: T_niveau_experience/Edit_Offre/5 pour offres d'emploi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Offre(int offreEmploiId, [Bind(Include = "id,nom_niveau_experience,domaine_id,profil_id,offre_emploi_id")] T_niveau_experience t_niveau_experience)
        {
            if (ModelState.IsValid)
            {
                t_niveau_experience.offre_emploi_id = offreEmploiId;
                db.Entry(t_niveau_experience).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "T_offre_emploi", new { id = offreEmploiId });
            }
            ViewBag.domaine_id = new SelectList(db.T_domaine, "id", "domaine", t_niveau_experience.domaine_id);
            ViewBag.profil_id = new SelectList(db.T_profil, "id", "nom", t_niveau_experience.profil_id);
            ViewBag.offre_emploi_id = new SelectList(db.T_offre_emploi, "id", "nom", t_niveau_experience.offre_emploi_id);
            return View(t_niveau_experience);
        }

        // GET: T_niveau_experience/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_niveau_experience t_niveau_experience = db.T_niveau_experience.Include(t => t.T_profil).FirstOrDefault(t => t.id == id);
            if (t_niveau_experience == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProfilId = t_niveau_experience.profil_id;
            return View(t_niveau_experience);
        }

        // POST: T_niveau_experience/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int profilId)
        {
            T_niveau_experience t_niveau_experience = db.T_niveau_experience.Find(id);
            db.T_niveau_experience.Remove(t_niveau_experience);
            db.SaveChanges();
            return RedirectToAction("Details", "T_profil", new { id = profilId });
        }

        // GET: T_niveau_experience/Delete_Offre/5 pour offres d'emploi
        public ActionResult Delete_Offre(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_niveau_experience t_niveau_experience = db.T_niveau_experience.Include(t => t.T_offre_emploi).FirstOrDefault(t => t.id == id);
            if (t_niveau_experience == null)
            {
                return HttpNotFound();
            }

            ViewBag.OffreId = t_niveau_experience.offre_emploi_id;
            return View(t_niveau_experience);
        }

        // POST: T_niveau_experience/Delete/5
        [HttpPost, ActionName("Delete_Offre")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed_Offre(int id, int offreEmploiId)
        {
            T_niveau_experience t_niveau_experience = db.T_niveau_experience.Find(id);
            db.T_niveau_experience.Remove(t_niveau_experience);
            db.SaveChanges();
            return RedirectToAction("Details", "T_offre_emploi", new { id = offreEmploiId });
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
