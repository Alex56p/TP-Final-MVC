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
    
    public partial class Attaque
    {
        public int Id { get; set; }
        public string NOM_ATTAQUE { get; set; }
        public string TYPE { get; set; }
        public Nullable<int> VIE_ENNEMI { get; set; }
        public Nullable<int> VIE_ALIER { get; set; }
        public int MANA { get; set; }
        public int PRIX { get; set; }
    }
}
