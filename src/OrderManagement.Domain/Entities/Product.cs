using OrderManagement.Domain.Exceptions;

namespace OrderManagement.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; private set; }
        public int Stock { get; private set; }
        public decimal Price { get; private set; }
        public long Code { get; private set; }

        private Product(string name, int stock, decimal price, long code)
        {
            Name = name.Trim();
            Stock = stock;
            Price = price;
            Code = code;
        }

        public static Product Create(string name, int stock, decimal price, long code)
        {
            ValidateName(name);
            ValidateStock(stock);
            ValidatePrice(price);
            ValidateCode(code);

            return new Product(name, stock, price, code);
        }

        public void Update(string name, int stock, decimal price, long code)
        {
            if (IsDeleted)
                throw new DomainException("Cannot update a deleted customer.");

            ValidateName(name);
            ValidateStock(stock);
            ValidatePrice(price);
            ValidateCode(code);

            Name = name.Trim();
            Stock = stock;
            Price = price;
            Code = code;

            SetUpdatedAt();
        }

        public void AddStock(int value)
        {
            ValidateStock(value);

            Stock += value;

            if (IsDeleted) //Pensei nessa logica para que em caso de um produto deletado voltar a ser ativo, a acao de adicionar estoque para ele seja suficiente para restaurar seu registro. 
            {
                IsDeleted = false;
                SetUpdatedAt();
            }
        }

        public void RemoveStock(int value)
        {
            ValidateStock(value);

            if (Stock - value < 0)
                throw new DomainException("The stock cannot be less than zero.");

            Stock -= value;

            SetUpdatedAt();
        }

        private static void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Product name cannot be null or empty.");

            if (name.Trim().Length < 2)
                throw new DomainException("Product name must have at least 2 characters.");

            if (name.Trim().Length > 100)
                throw new DomainException("Product name cannot exceed 100 characters.");
        }

        private static void ValidateStock(int stock)
        {
            if (stock < 0)
                throw new DomainException("Cannot set a negativa stock for a product.");
        }

        private static void ValidatePrice(decimal price)
        {
            if (price <= 0)
                throw new DomainException("Cannot set a null price for a product.");
        }

        private static void ValidateCode(long code)
        {
            if (code <= 0)
                throw new DomainException("Cannot set an invalid code for a product.");
        }
    }
}
