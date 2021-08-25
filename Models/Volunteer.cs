namespace websitee.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Volunteer")]
    public partial class Volunteer
    {
        [Key]
        public int Volunteer_Id { get; set; }

        public int User_Id { get; set; }

        public int Service_Id { get; set; }

        public virtual Service Service { get; set; }

        public virtual User User { get; set; }
    }
}
