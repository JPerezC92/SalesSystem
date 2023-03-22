using System;
using System.Collections.Generic;

namespace SalesSystem.Models
{
    public partial class Sale
    {
        public Sale()
        {
            SaleDetail = new HashSet<SaleDetail>();
        }

        public long Id { get; set; }
        public DateTime Date { get; set; }
        public int? IdClient { get; set; }
        public decimal? Total { get; set; }

        public virtual Client IdClientNavigation { get; set; }
        public virtual ICollection<SaleDetail> SaleDetail { get; set; }
    }
}
