﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Mobilite_Pro_BDDEntities : DbContext
    {
        public Mobilite_Pro_BDDEntities()
            : base("name=Mobilite_Pro_BDDEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<T_competences> T_competences { get; set; }
        public virtual DbSet<T_entreprise> T_entreprise { get; set; }
        public virtual DbSet<T_langues> T_langues { get; set; }
        public virtual DbSet<T_num_tel> T_num_tel { get; set; }
        public virtual DbSet<T_offre_emploi> T_offre_emploi { get; set; }
        public virtual DbSet<T_profil> T_profil { get; set; }
        public virtual DbSet<T_profil_competences> T_profil_competences { get; set; }
        public virtual DbSet<T_profil_langues> T_profil_langues { get; set; }
        public virtual DbSet<T_role> T_role { get; set; }
        public virtual DbSet<T_site> T_site { get; set; }
        public virtual DbSet<T_statut> T_statut { get; set; }
        public virtual DbSet<T_type_contrat> T_type_contrat { get; set; }
        public virtual DbSet<T_niveau_experience> T_niveau_experience { get; set; }
    }
}
