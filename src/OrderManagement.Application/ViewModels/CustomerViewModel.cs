namespace OrderManagement.Application.ViewModels
{
    public record CustomerViewModel
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email {  get; set; }
    }
}
