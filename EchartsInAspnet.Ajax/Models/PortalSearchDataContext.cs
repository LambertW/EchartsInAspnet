using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EchartsInAspnet.Ajax.Models
{
    public class PortalSearchDataContext : IDisposable
    {
        public List<DOC_Order_Header> DOC_Order_Header { get; set; }

        public PortalSearchDataContext()
        {
            DOC_Order_Header = new List<DOC_Order_Header>();
            Random random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                var temp = new DOC_Order_Header
                {
                    OrderTime = DateTime.Now.AddDays(-random.Next(100))
                };
                DOC_Order_Header.Add(temp);
            }
        }

        public void Dispose()
        {
        }
    }
}