namespace OrderManagement.Application.ViewModels
{
    public record ProductViewModel
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public long Code { get; set; }
    }
}
