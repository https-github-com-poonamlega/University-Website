namespace websitee.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Service")]
    public partial class Service
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Service()
        {
            Volunteers = new HashSet<Volunteer>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Service_Id { get; set; }

        public int Admin_Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Service_Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Service_Description { get; set; }

        public int Reqired_Volunteer { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        public DateTime Start_Date { get; set; }


        public int? Participated_Volunteer { get; set; } = 0;

        public virtual Admin Admin { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Volunteer> Volunteers { get; set; }
    }
}
