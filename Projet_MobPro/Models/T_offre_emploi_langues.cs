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
    
    public partial class T_offre_emploi_langues
    {
        public int id { get; set; }
        public int offre_emploi_id { get; set; }
        public int langues_id { get; set; }
        public string commentaire { get; set; }
    
        public virtual T_langues T_langues { get; set; }
        public virtual T_offre_emploi T_offre_emploi { get; set; }
    }
}
