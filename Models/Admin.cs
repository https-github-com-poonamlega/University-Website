namespace websitee.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Admin")]
    public partial class Admin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Admin()
        {
            Services = new HashSet<Service>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Admin_Id { get; set; }

        [Required]
        [StringLength(10)]
        public string First_Name { get; set; }

        [Required]
        [StringLength(10)]
        public string Last_Name { get; set; }

        [Column(TypeName = "date")]
        public DateTime DOB { get; set; }

        [Required]
        [StringLength(10)]
        public string Gender { get; set; }

        [Required]
        [StringLength(10)]
        public string Contact_Number { get; set; }

        [Required]
        [StringLength(40)]
        public string Email { get; set; }

        [Required]
        [StringLength(10)]
        public string Password { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Service> Services { get; set; }
    }
}
