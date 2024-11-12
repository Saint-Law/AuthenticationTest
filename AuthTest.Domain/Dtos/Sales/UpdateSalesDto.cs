namespace AuthTest.Domain.Dtos.Sales
{
    public class UpdateSalesDto
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string OrderType { get; set; }
        public DateTime OrderDateTime { get; set; }
        public string Status { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public string PaymentMethod { get; set; }
        public string CustomerNumber { get; set; }
        public int AmountDiscount { get; set; }
    }
}
