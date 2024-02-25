using Moq;
using System;
using UnitTest.Chapter2.London;
using Xunit;

namespace UnitTest.Chapter5_1
{
    public class Chapter5_1
    {
        [Fact]
        public void Sending_a_greethings_email()
        {
            var mock = new Mock<IEmailGateway>();
            var sut = new EmailGateWayController(mock.Object);

            sut.GreetUser("user@email.com");

            mock.Verify(x => x.SendGreetingEmail("user@email.com"), Times.Once);
        }

        [Fact]
        public void Creating_a_report()
        {
            var stub = new Mock<IDataBase>();
            stub.Setup(x => x.GetNumberOfUsers()).Returns(10);

            var sut = new ReportController(stub.Object);

            Report report = sut.CreateReport();

            Assert.Equal(10, report.NumberOfUsers);
        }

        [Fact]
        public void Wrong_Creating_a_report()
        {
            var stub = new Mock<IDataBase>();
            stub.Setup(x => x.GetNumberOfUsers()).Returns(10);

            var sut = new ReportController(stub.Object);

            Report report = sut.CreateReport();

            Assert.Equal(10, report.NumberOfUsers);
            stub.Verify(x => x.GetNumberOfUsers(), Times.Once);
        }

        [Fact]
        public void Purchase_fails_when_not_enough_inventory()
        {
            var storeMock = new Mock<IStore>();
            storeMock.Setup(x => x.HasEnoughInventory(Product.Shampoo, 5))
                .Returns(false);

            var sut = new Customer();

            bool success = sut.Purchase(storeMock.Object, Product.Shampoo, 5);

            Assert.False(success);
            storeMock.Verify(x => x.RemoveInventory(Product.Shampoo, 5), Times.Never);
        }
    }

    internal class ReportController
    {
        private IDataBase @object;

        public ReportController(IDataBase @object)
        {
            this.@object = @object;
        }

        internal Report CreateReport()
        {
            throw new NotImplementedException();
        }
    }

    internal class Report
    {
        public int NumberOfUsers { get; internal set; }
    }

    internal interface IDataBase
    {
        int GetNumberOfUsers();
    }

    internal class EmailGateWayController
    {
        private IEmailGateway @object;

        public EmailGateWayController(IEmailGateway @object)
        {
            this.@object = @object;
        } 

        internal void GreetUser(string v)
        {
            throw new NotImplementedException();
        }
    }

    internal interface IEmailGateway
    {
        void SendGreetingEmail(string v);
    }
}
