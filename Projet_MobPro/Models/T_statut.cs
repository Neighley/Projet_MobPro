//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------
using System.ComponentModel.DataAnnotations;

namespace Projet_MobPro.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_statut
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_statut()
        {
            this.T_offre_emploi = new HashSet<T_offre_emploi>();
        }
    
        public int id { get; set; }
        [Display(Name = "Statut")]
        public string statut { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_offre_emploi> T_offre_emploi { get; set; }
    }
}
