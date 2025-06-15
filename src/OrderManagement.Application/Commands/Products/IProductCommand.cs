namespace OrderManagement.Application.Commands.Products
{
    public interface IProductCommand
    {
        public string Name { get; }
        public int Stock { get; }
        public decimal Price { get; }
        public long Code { get; }
    }
}
