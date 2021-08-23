using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace websitee.Models
{
    public class HelpModels
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid HelpId { get; set; }


        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Required]

        [Display(Name = "Issue")]
        public string Issue { get; set; }


        [Required]

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]

        [Display(Name = "Date of Ticket")]
        public DateTime DOT { get; set; }

        [Display(Name = "Admin Resolution")]
        public string AdminResolution { get; set; }

    }

}