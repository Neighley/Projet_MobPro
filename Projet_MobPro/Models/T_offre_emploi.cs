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
    
    public partial class T_offre_emploi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_offre_emploi()
        {
            this.T_competences = new HashSet<T_competences>();
            this.T_langues = new HashSet<T_langues>();
            this.T_niveau_experience = new HashSet<T_niveau_experience>();
        }
    
        public int id { get; set; }
        public string nom { get; set; }
        public string description { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> date_publication { get; set; }
        public Nullable<System.DateTime> date_suppression { get; set; }
        public Nullable<bool> ouvert_externe { get; set; }
        public string url_site { get; set; }
        public Nullable<bool> teletravail_autorise { get; set; }
        public Nullable<bool> full_teletravail { get; set; }
        public Nullable<int> site_id { get; set; }
        public Nullable<int> statut_id { get; set; }
        public Nullable<int> entreprise_id { get; set; }
        public Nullable<int> type_contrat_id { get; set; }
        public Nullable<int> niveau_experience_id { get; set; }
    
        public virtual T_entreprise T_entreprise { get; set; }
        public virtual T_site T_site { get; set; }
        public virtual T_statut T_statut { get; set; }
        public virtual T_type_contrat T_type_contrat { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_competences> T_competences { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_langues> T_langues { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_niveau_experience> T_niveau_experience { get; set; }
    }
}
