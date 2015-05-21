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

        [HttpPost]
        public ActionResult Pokemons(string TB_RechercherPokemon, string Submit)
        {
            switch(Submit)
            {
                case "Rechercher":
                    Pokemon[] pokemons = this.Donnees.Pokemons.Where(c => c.NOM_POKEMON.StartsWith(TB_RechercherPokemon)).ToArray();
                    ViewBag.Pokemons = pokemons;
                    break;
                case "Tout":
                    Pokemons();
                    break;
                default:
                    Pokemons();
                    break;
            }


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

        [HttpPost]
        public ActionResult Items(string TB_RechercherItem, string Submit)
        {
            switch (Submit)
            {
                case "Rechercher":
                    Item[] items = this.Donnees.Items.Where(c => c.NOM_ITEM.StartsWith(TB_RechercherItem)).ToArray();
                    ViewBag.Items = items;
                    break;
                case "Tout":
                    Items();
                    break;
                default:
                    Items();
                    break;
            }
            
            return View();
        }

        public ActionResult Attaques()
        {
            Attaque[] attaques = this.Donnees.Attaques.ToArray();
            ViewBag.Attaques = attaques;
            return View();
        }

        [HttpPost]
        public ActionResult Attaques(string TB_RechercherAttaque, string Submit)
        {
            switch (Submit)
            {
                case "Rechercher":
                    Attaque[] attaques = this.Donnees.Attaques.Where(c => c.NOM_ATTAQUE.StartsWith(TB_RechercherAttaque)).ToArray();
                    ViewBag.Attaques = attaques;
                    break;
                case "Tout":
                    Attaques();
                    break;
                default:
                    Attaques();
                    break;
            }
            
            return View();
        }

        public ActionResult Inscription()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Inscription(String TB_Username, String TB_Email, String TB_Password)
        {
            Joueur j = new Joueur();
            j.ALIAS = TB_Username; 
            j.EMAIL = TB_Email;
            j.MOT_PASSE = TB_Password;
            j.DIVISION = "Bronze";
            j.EXPERIENCE = 0;
            j.NOMBRE_PARTIES_JOUES = 0;
            j.NOMBRE_VICTOIRE = 0;
            Donnees.Joueurs.Add(j);
            Donnees.SaveChanges();
            return View();
        }

        public ActionResult Statistiques()
        {
            Pokemon[] pokemons = this.Donnees.Pokemons.ToArray();
            ViewBag.Pokemons = pokemons;

            Item[] items = this.Donnees.Items.ToArray();
            ViewBag.Items = items;
            return View();
        }

        public ActionResult Connexion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Connexion(String TB_UserName, String TB_Password)
        {
            Joueur[] j = this.Donnees.Joueurs.Where(c => c.ALIAS.Equals(TB_UserName)).Where(c => c.MOT_PASSE.Equals(TB_Password)).ToArray();
            if (j.Length > 0)
            {
                Session["Username"] = TB_UserName;
                return RedirectToAction("Index");
            }
            else
                ViewBag.Error = "Mauvais usager/mot de passe";

            return View();
        }
    }
}