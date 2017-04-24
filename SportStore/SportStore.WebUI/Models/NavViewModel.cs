using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsStore.WebUI.Models
{
    public class NavViewModel
    {
        public string SelectedCategory { get; set; }
        public IEnumerable<string> Categories { get; set; }
    }
}