//------------------------------------------------------------------------------
// <auto-generated>
//    Ce code a été généré à partir d'un modèle.
//
//    Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//    Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TP_MVC
{
    using System;
    using System.Collections.Generic;
    
    public partial class Item
    {
        public int Id { get; set; }
        public string IMAGE { get; set; }
        public string NOM_ITEM { get; set; }
        public string TYPE { get; set; }
        public string DESCRIPTION { get; set; }
        public int PRIX { get; set; }

        public static Item Afficher(int num)
        {
            MainBDEntities Donnees = new MainBDEntities();
            Item result = Donnees.Items.Find(num);

            return result;
        }
    }
}
