namespace websitee.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Event
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Event()
        {
            Events_User_Relation = new HashSet<Events_User_Relation>();
        }

        [Key]
        public int Event_Id { get; set; }

        [Required]
        public string Event_Name { get; set; }

        public DateTime Start_Date { get; set; }

        public DateTime End_Date { get; set; }

        [Required]
        public string Category { get; set; }

        public int Participated_User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Events_User_Relation> Events_User_Relation { get; set; }
    }
}
