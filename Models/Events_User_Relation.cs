namespace websitee.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Events_User_Relation
    {
        public int Id { get; set; }

        public int Event_Id { get; set; }

        public int User_Id { get; set; }

        public int? Like_Dislike { get; set; }

        public int? Interest { get; set; }

        public string Comment { get; set; }

        public int? Attendence { get; set; }

        public virtual Event Event { get; set; }
    }
}
