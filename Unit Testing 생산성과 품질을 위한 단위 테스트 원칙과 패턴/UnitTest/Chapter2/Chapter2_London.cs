using Moq;
using System.Collections.Generic;
using Xunit;

namespace UnitTest.Chapter2.London
{
    public class Chapter2_London
    {
        [Fact]
        public void Purchase_succeeds_when_enough_inventory()
        {
            // Arrange
            var storeMock = new Mock<IStore>();
            storeMock.Setup(x => x.HasEnoughInventory(Product.Shampoo, 5))
                .Returns(true);

            Customer customer = new Customer();

            // Act
            bool success = customer.Purchase(storeMock.Object, Product.Shampoo, 5);

            // Assert
            Assert.True(success);
            storeMock.Verify(x => x.RemoveInventory(Product.Shampoo, 5), Times.Once);
        }

        [Fact]
        public void Purchase_fails_when_not_enough_inventory()
        {
            // Arrange
            var storeMock = new Mock<IStore>();
            storeMock.Setup(x => x.HasEnoughInventory(Product.Shampoo, 5))
                .Returns(false);

            Customer customer = new Customer();

            // Act
            bool success = customer.Purchase(storeMock.Object, Product.Shampoo, 5);

            // Assert
            Assert.False(success);
            storeMock.Verify(x => x.RemoveInventory(Product.Shampoo, 5), Times.Never);
        }
    }

    public interface IStore
    {
        void AddInventory(Product shampoo, int v);
        int GetInventory(Product shampoo);
        bool RemoveInventory(Product product, int v);
        bool HasEnoughInventory(Product product, int v);
    }

    public class Store : IStore
    {
        private readonly Dictionary<Product, int> _inventory;

        public Store()
        {
            _inventory = new Dictionary<Product, int>();
        }

        public void AddInventory(Product shampoo, int v)
        {
            if (!_inventory.ContainsKey(shampoo))
            {
                _inventory.Add(shampoo, v);
                return;
            }

            _inventory[shampoo] += v;
        }

        public int GetInventory(Product shampoo)
            => _inventory.ContainsKey(shampoo) ? _inventory[shampoo] : -1;

        public bool HasEnoughInventory(Product product, int v)
            => _inventory[product] >= v;

        public bool RemoveInventory(Product product, int v)
        {
            if (!_inventory.ContainsKey(product))
                return false;

            if (!HasEnoughInventory(product, v))
                return false;

            _inventory[product] -= v;
            return true;
        }
    }

    public class Customer
    {
        internal bool Purchase(IStore store, Product shampoo, int v)
        {
            if (!store.HasEnoughInventory(shampoo, v))
                return false;

            store.RemoveInventory(shampoo, v);
            return true;
        }
    }

    public enum Product
    {
        Shampoo,
        Book
    }
}