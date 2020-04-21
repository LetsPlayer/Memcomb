//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Memcomb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.Connections = new HashSet<Connection>();
            this.Convoes = new HashSet<Convo>();
            this.Followings = new HashSet<Following>();
            this.Memories = new HashSet<Memory>();
            this.Blockings = new HashSet<Blocking>();
        }
    
        public int User_ID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email_ID { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> Date_Of_Birth { get; set; }
        public string Phone_Number { get; set; }
        public string Profile_Picture { get; set; }
        public string Biography { get; set; }
        public bool Is_Verified { get; set; }
        public string Activation_Code { get; set; }
        public bool Is_Admin { get; set; }
        public string Background_Pic { get; set; }
        public HttpPostedFileBase Profile_Picture_imgPath { get; set; }
        public HttpPostedFileBase Background_Photo { get; set; }
        public List<Memory> memoryList { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Connection> Connections { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Convo> Convoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Following> Followings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Memory> Memories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Blocking> Blockings { get; set; }
    }
}
