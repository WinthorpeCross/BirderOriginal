using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Birder2.Models
{
    public class SalesOrder
    {
        public SalesOrder()
        {
            SalesOrderItems = new List<SalesOrderItem>();
        }

        public int SalesOrderId { get; set; }

        public string CustomerName { get; set; }

        public string PONumber { get; set; }

        public virtual List<SalesOrderItem> SalesOrderItems { get; set; }
    }
}
