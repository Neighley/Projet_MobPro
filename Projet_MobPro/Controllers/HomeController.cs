using Microsoft.AspNet.Identity;
using Projet_MobPro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projet_MobPro.Controllers
{
    public class HomeController : Controller
    {
        private Mobilite_Pro_BDDEntities db = new Mobilite_Pro_BDDEntities();
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
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}