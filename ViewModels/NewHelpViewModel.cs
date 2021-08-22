using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using websitee.Models;

namespace websitee.ViewModels
{
    public class NewHelpViewModel
    {
        public IEnumerable<HelpModels> HelpModelRecord { get; set; }
    }
}