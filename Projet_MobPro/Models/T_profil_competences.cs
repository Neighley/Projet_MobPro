//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Projet_MobPro.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_profil_competences
    {
        public int profil_id { get; set; }
        public int competences_id { get; set; }
        public Nullable<int> level_competences { get; set; }
        public string commentaire { get; set; }
        public Nullable<bool> marge_erreur { get; set; }
        public Nullable<bool> restriction_level_competences { get; set; }
    
        public virtual T_competences T_competences { get; set; }
        public virtual T_profil T_profil { get; set; }
    }
}
