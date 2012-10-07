//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HoldTheAllergen.Data.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public User()
        {
            this.StarredMenuItems = new HashSet<UserStarredMenuItem>();
            this.Allergies = new HashSet<Allergen>();
        }
    
        public System.Guid Id { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime LastActivity { get; set; }
    
        public virtual ICollection<UserStarredMenuItem> StarredMenuItems { get; set; }
        public virtual ICollection<Allergen> Allergies { get; set; }
    }
}
