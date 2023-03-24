namespace SalesSystem.Models.Request
{
    public class SaleReq
    {
        public int IdClient { get; set; }
        public decimal Total { get; set; }
        public List<Detail> DetailList { get; set; } = new List<Detail>();
    }

    public class Detail
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
        public int IdProduct { get; set; }

    }
}
