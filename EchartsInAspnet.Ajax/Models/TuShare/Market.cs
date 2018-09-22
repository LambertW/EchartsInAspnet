using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EchartsInAspnet.Ajax.Models.TuShare
{
    [TableName("tbl_market")]
    public class Market
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public DateTime Date { get; set; }
        
        public decimal Open { get; set; }

        public decimal Volume { get; set; }
    }
}