using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EchartsInAspnet.Ajax.Models
{
    public class PortalSearchDataContext : IDisposable
    {
        public static List<DOC_Order_Header> DOC_Order_Header { get; set; }

        static PortalSearchDataContext()
        {
            DOC_Order_Header = new List<DOC_Order_Header>();
            Random random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                var temp = new DOC_Order_Header
                {
                    OrderTime = DateTime.Now.AddHours(-random.Next(100)),
                    TotalPrice = 2000 + (decimal)random.NextDouble() * 600
                };
                DOC_Order_Header.Add(temp);
            }
        }

        public static void AddData()
        {
            Random random = new Random();
            var latest = DOC_Order_Header.OrderByDescending(t => t.OrderTime).First();
            var temp = new DOC_Order_Header
            {
                OrderTime = DateTime.Now,
                TotalPrice = (random.Next() % 2) == 0 ? latest.TotalPrice + random.Next(20) : latest.TotalPrice - random.Next(20)
            };
            DOC_Order_Header.Add(temp);
        }

        public void Dispose()
        {
        }
    }
}