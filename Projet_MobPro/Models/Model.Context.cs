﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
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
        public virtual DbSet<T_domaine> T_domaine { get; set; }
        public virtual DbSet<T_entreprise> T_entreprise { get; set; }
        public virtual DbSet<T_langues> T_langues { get; set; }
        public virtual DbSet<T_niveau_experience> T_niveau_experience { get; set; }
        public virtual DbSet<T_num_tel> T_num_tel { get; set; }
        public virtual DbSet<T_offre_emploi> T_offre_emploi { get; set; }
        public virtual DbSet<T_offre_emploi_competences> T_offre_emploi_competences { get; set; }
        public virtual DbSet<T_offre_emploi_langues> T_offre_emploi_langues { get; set; }
        public virtual DbSet<T_profil> T_profil { get; set; }
        public virtual DbSet<T_profil_competences> T_profil_competences { get; set; }
        public virtual DbSet<T_profil_langues> T_profil_langues { get; set; }
        public virtual DbSet<T_role> T_role { get; set; }
        public virtual DbSet<T_site> T_site { get; set; }
        public virtual DbSet<T_statut> T_statut { get; set; }
        public virtual DbSet<T_type_contrat> T_type_contrat { get; set; }
    }
}
