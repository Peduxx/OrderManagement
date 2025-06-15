namespace OrderManagement.Application.DTOs
{
    public record OrderItemDTO
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
