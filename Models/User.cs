namespace websitee.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int User_Id { get; set; }

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

        [Required]
        [StringLength(40)]
        public string A1 { get; set; }

        [Required]
        [StringLength(40)]
        public string A2 { get; set; }

        [Required]
        [StringLength(40)]
        public string A3 { get; set; }
    }
}
