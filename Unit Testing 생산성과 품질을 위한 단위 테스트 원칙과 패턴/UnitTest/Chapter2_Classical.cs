namespace UnitTest.Chapter2_Classical
{
    public class Chapter2_Classical
    {
        [Fact]
        public void Purchase_succeeds_when_enough_inventory()
        {
            // Arrange
            Store store = new ();
            store.AddInventory(Product.Shampoo, 10);
            Customer customer = new();

            // Act
            bool success = customer.Purchase(store, Product.Shampoo, 5);

            // Assert
            Assert.True(success);
            Assert.Equal(5, store.GetInventory(Product.Shampoo));
        }

        [Fact]
        public void Purchase_fails_when_not_enough_inventory()
        {
            // Arrange
            Store store = new ();
            store.AddInventory(Product.Shampoo, 10);
            Customer customer = new();

            // Act
            bool success = customer.Purchase(store, Product.Shampoo, 15);

            // Assert
            Assert.False(success);
            Assert.Equal(10, store.GetInventory(Product.Shampoo));
        }
    }

    class Store
    {
        private readonly Dictionary<Product, int> _inventory;

        public Store()
        {
            _inventory = new();
        }

        internal void AddInventory(Product shampoo, int v)
        {
            if (!_inventory.ContainsKey(shampoo))
            {
                _inventory.Add(shampoo, v);
                return;
            }

            _inventory[shampoo] += v;
        }

        internal int GetInventory(Product shampoo)
            => _inventory.ContainsKey(shampoo) ? _inventory[shampoo] : -1;

        internal bool RemoveInventory(Product product, int v)
        {
            if (!_inventory.ContainsKey(product))
                return false;

            if (_inventory[product] < v)
                return false;

            _inventory[product] -= v;
            return true;
        }
    }

    class Customer
    {
        internal bool Purchase(Store store, Product shampoo, int v)
            => store.RemoveInventory(shampoo, v);
    }

    enum Product
    {
        Shampoo,
        Book
    }
}