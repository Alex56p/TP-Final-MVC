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

        public Pokemon Afficher(int num)
        {
            return new Pokemon();
        }

        public ActionResult Liste()
        {
            Joueur[] j = Donnees.Joueurs.ToArray();
            ViewBag.Joueurs = j;
            return PartialView();
        }


        #region Pokemons
        [HttpPost]
        public ActionResult Pokemons(string TB_RechercherPokemon, string Submit)
        {
            switch (Submit)
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

        #endregion
        #region Item
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
        #endregion
        #region Attaques
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
        #endregion
        #region Inscription
        public ActionResult Inscription()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Inscription(String TB_Username, String TB_Email, String TB_Password)
        {
            if (ModelState.IsValid && TB_Username != null && TB_Email != null && TB_Password != null && TB_Username != "" && TB_Email != "" && TB_Password != "")
            {
                if(TB_Email.Contains('@') && TB_Email.Contains('.'))
                {
                    Joueur[] joueurs = this.Donnees.Joueurs.Where(c => c.ALIAS.Equals(TB_Username)).ToArray();
                    if (joueurs.Length == 0)
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

                        Session["Username"] = TB_Username;
                        return RedirectToAction("Index");
                    }
                    else
                        ViewBag.Error = "L'usager existe déjà.";
                }
                else
                {
                    ViewBag.Error = "L'adresse courriel n'est pas valide.";
                }
            }
            else
            {
                ViewBag.Error = "Tous les champs doivent être remplis.";
            }
            return View();
        }
        #endregion
        #region Update
        public ActionResult Update()
        {
            if(Session["Username"] != null)
            {
                String username = Session["Username"].ToString();
                Joueur[] j = Donnees.Joueurs.Where(c => c.ALIAS.Equals(username)).ToArray();
                ViewBag.Joueur = j;
                return View();
            }
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Update( String TB_Password, String TB_Email)
        {
            if (TB_Email != null && TB_Password != null && TB_Email != "" && TB_Password != "" && Session["Username"] != null)
            {
                if (TB_Email.Contains('@') && TB_Email.Contains('.'))
                {
                    Joueur j1 = Donnees.Joueurs.Find(Session["Username"].ToString());
                    j1.EMAIL = TB_Email;
                    j1.MOT_PASSE = TB_Password;
                    Donnees.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "L'email doit être valide.";
                    String username = Session["Username"].ToString();
                    Joueur[] j = Donnees.Joueurs.Where(c => c.ALIAS.Equals(username)).ToArray();
                    ViewBag.Joueur = j;
                    return View();
                }
            }
            else
            {
                ViewBag.Error = "Veuillez remplir tous les champs.";

                String username = Session["Username"].ToString();
                Joueur[] j = Donnees.Joueurs.Where(c => c.ALIAS.Equals(username)).ToArray();
                ViewBag.Joueur = j;
                return View();
            }
        }
        #endregion
        #region Statistiques
        public ActionResult Statistiques()
        {
            String Username = "";
            if(Session["Username"] != null)
            {
                Username = Session["Username"].ToString();
            }

            if(Username != "")
            {
                ACHAT_POKEMON[] pokemons = this.Donnees.ACHAT_POKEMON.Where(c => c.ALIAS_JOUEUR.Equals(Username)).ToArray();
                ViewBag.Pokemons = pokemons;

                Achat_Items[] items = this.Donnees.Achat_Items.Where(c=> c.ALIAS_JOUEUR.Equals(Username)).ToArray();
                ViewBag.Items = items;

                Joueur[] j = Donnees.Joueurs.Where(c => c.ALIAS.Equals(Username)).ToArray();
                ViewBag.Joueur = j;
            }
            else
            {
                ACHAT_POKEMON[] pokemons = this.Donnees.ACHAT_POKEMON.Where(c => c.ALIAS_JOUEUR.Equals("Admin")).ToArray();
                ViewBag.Pokemons = pokemons;

                Achat_Items[] items = this.Donnees.Achat_Items.Where(c => c.ALIAS_JOUEUR.Equals("Admin")).ToArray();
                ViewBag.Items = items;

                Joueur[] j = Donnees.Joueurs.Where(c => c.ALIAS.Equals("Admin")).ToArray();
                ViewBag.Joueur = j;
            }
            
            return View();
        }

        [HttpPost]
        public ActionResult Statistiques(string TB_RechercherJoueur)
        {
            ACHAT_POKEMON[] pokemons = this.Donnees.ACHAT_POKEMON.Where(c => c.ALIAS_JOUEUR.Equals(TB_RechercherJoueur)).ToArray();
            ViewBag.Pokemons = pokemons;

            Achat_Items[] items = this.Donnees.Achat_Items.Where(c => c.ALIAS_JOUEUR.Equals(TB_RechercherJoueur)).ToArray();
            ViewBag.Items = items;

            Joueur[] j = Donnees.Joueurs.Where(c => c.ALIAS.Equals(TB_RechercherJoueur)).ToArray();
            ViewBag.Joueur = j;

            if(j.Length != 0)
            {
                ViewBag.Trouve = true;
                ViewBag.UsernameRecherche = j[0].ALIAS;
            }
            else
            {
                ViewBag.Trouve = false;
            }

            return View();
        }
        #endregion
        #region Connexion
        public ActionResult Connexion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Connexion(String TB_UserName, String TB_Password)
        {
            if(ModelState.IsValid && TB_Password != null && TB_Password != "" && TB_UserName != null && TB_UserName != "")
            { 
                Joueur[] j = this.Donnees.Joueurs.Where(c => c.ALIAS.Equals(TB_UserName)).Where(c => c.MOT_PASSE.Equals(TB_Password)).ToArray();
                if (j.Length > 0)
                {
                    Session["Username"] = TB_UserName;
                    return RedirectToAction("Index");
                }
                else
                    ViewBag.Error = "Mauvais usager/mot de passe";
            }
            else
            {
                ViewBag.Error = "Tous les champs doivent être remplis.";
            }
            return View();
        }

        public ActionResult Deconnexion()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
        #endregion
        #region GodRoom
        public ActionResult GodRoom()
        {
            if(Session["Username"] != null && (Session["Username"].ToString() == "Admin" || Session["Username"].ToString() == "admin"))
            {
                Joueur[] j = Donnees.Joueurs.ToArray();
                ViewBag.Joueurs = j;
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult GodRoom(string TB_Rechercher, string Submit)
        {
            switch (Submit)
            {
                case "Rechercher":
                    Joueur[] j = this.Donnees.Joueurs.Where(c => c.ALIAS.StartsWith(TB_Rechercher)).ToArray();
                    ViewBag.Joueurs = j;
                    break;
                case "Tout":
                    GodRoom();
                    break;
                default:
                    GodRoom();
                    break;
            }
            return View();
        }

        public ActionResult Delete(String id)
        {
            Joueur j = Donnees.Joueurs.Where(c => c.ALIAS.Equals(id)).FirstOrDefault();
            Donnees.Joueurs.Remove(j);
            Donnees.SaveChanges();
            return RedirectToAction("GodRoom");
        }
        #endregion
    }
}