//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class T_profil_langues
    {
        public int id { get; set; }
        public int profil_id { get; set; }
        public int langues_id { get; set; }
        public Nullable<int> level_langues { get; set; }
        public string commentaire { get; set; }
    
        public virtual T_langues T_langues { get; set; }
        public virtual T_profil T_profil { get; set; }
    }
}
