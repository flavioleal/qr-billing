using Moq.AutoMock;
using QR.Billings.Business.Entities;
using QR.Billings.Business.Interfaces.Notifier;
using QR.Billings.Business.Interfaces.Repositories;
using QR.Billings.Business.Interfaces.Services;
using QR.Billings.Business.IO.Billing;
using QR.Billings.Business.Services;
using QR.Billings.UnitTests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QR.Billings.UnitTests.Services
{
    public class BillingServiceTests : BaseTests
    {
        private readonly IBillingService _billingService;
        private readonly AutoMocker _mocker;

        public BillingServiceTests()
        {
            _mocker = new AutoMocker();
            _billingService = _mocker.CreateInstance<BillingService>();
            MockCurrentUser(_mocker);
        }

        [Fact(DisplayName = "Add - When inserting new billing with valid input should return true")]
        [Trait("Category", "Billing - Services")]
        public async Task AddAsync_ValidInput_ReturnsTrue()
        {
            // Arrange
            var input = new AddBillingInput
            {
                Value = 33.10m,
                CustomerName = "Flavio",
                CustomerEmail = "teste@teste.com"
            };

            // Act
            var result = await _billingService.AddAsync(input);

            // Assert
            Assert.True(result);
        }
    }
}
