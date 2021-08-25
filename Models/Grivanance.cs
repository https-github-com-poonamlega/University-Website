namespace websitee.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Grivanance")]
    public partial class Grivanance
    {
        [Key]
        public int Grivanance_Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        public int User_Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Topic { get; set; }

        [Required]
        [StringLength(150)]
        public string Sub_Topic { get; set; }

        [Required]
        [StringLength(300)]
        public string Details { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        public DateTime Expected_Resolution_Date { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(150)]
        public string Admin_Comment { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        public DateTime Created_On { get; set; }
    }
}
