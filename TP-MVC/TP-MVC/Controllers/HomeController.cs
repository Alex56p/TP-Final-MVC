using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TP_MVC.Controllers
{
    public class HomeController : Controller
    {
        private MainBDEntities Donnees = new MainBDEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Galerie()
        {
            return View();
        }

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

        

        public ActionResult Pokemons()
        {
            Pokemon[] pokemons = this.Donnees.Pokemons.ToArray();
            ViewBag.Pokemons = pokemons;
            return View();
        }

        public ActionResult Items()
        {
            Item[] items = this.Donnees.Items.ToArray();
            ViewBag.Items = items;
            return View();
        }

        public ActionResult Attaques()
        {
            return View();
        }

        public ActionResult Inscription()
        {
            return View();
        }

        public ActionResult Statistiques()
        {
            return View();
        }
    }
}