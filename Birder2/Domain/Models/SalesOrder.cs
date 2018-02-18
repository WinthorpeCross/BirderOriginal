using System.Collections.Generic;

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
