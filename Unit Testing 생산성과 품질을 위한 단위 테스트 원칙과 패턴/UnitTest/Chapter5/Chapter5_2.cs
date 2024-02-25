using Moq;
using System;
using UnitTest.Chapter2.London;
using Xunit;

namespace UnitTest.Chapter5_2
{
    public class Chapter5_2
    {
        [Fact]
        public void Successful_purchase()
        {
            var mock = new Mock<IEmailGateway>();
            var sut = new CustomerController(mock.Object);

            bool isSuccess = sut.Purchase(1, 2, 5);

            Assert.True(isSuccess);
            mock.Verify(x => x.SendReceipt("customer@email.com", "Shampoo", 5), Times.Once);
        }

        [Fact]
        public void Wrong_Purchase_succeeds_when_enough_inventory()
        {
            var storeMock = new Mock<IStore>();
            storeMock.Setup(x => x.HasEnoughInventory(Product.Shampoo, 5))
                .Returns(true);

            var customer = new Customer();

            bool success = customer.Purchase(storeMock.Object, Product.Shampoo, 5);

            Assert.True(success);
            storeMock.Verify(x => x.RemoveInventory(Product.Shampoo, 5), Times.Once);
        }
    }

    internal class CustomerController
    {
        private IEmailGateway @object;

        public CustomerController(IEmailGateway @object)
        {
            this.@object = @object;
        }

        internal bool Purchase(int v1, int v2, int v3)
        {
            throw new NotImplementedException();
        }
    }

    internal interface IEmailGateway
    {
        void SendReceipt(string v1, string v2, int v3);
    }
}
