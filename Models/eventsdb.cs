namespace websitee.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class eventsdb : DbContext
    {
        public eventsdb()
            : base("name=eventsdb")
        {
        }

        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Events_User_Relation> Events_User_Relation { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public System.Data.Entity.DbSet<websitee.ViewModels.CreateGrivenceViewModel> CreateGrivenceViewModels { get; set; }
    }
}
