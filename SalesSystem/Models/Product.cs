using System;
using System.Collections.Generic;

namespace SalesSystem.Models
{
    public partial class Product
    {
        public Product()
        {
            SaleDetail = new HashSet<SaleDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Cost { get; set; }

        public virtual ICollection<SaleDetail> SaleDetail { get; set; }
    }
}
