using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EchartsInAspnet.Ajax.Models
{
    public class Series
    {
        public string name { get; set; }
        public string type { get; set; }
        public List<decimal> data { get; set; }
    }
}