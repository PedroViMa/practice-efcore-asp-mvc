using Microsoft.EntityFrameworkCore;
using StoreWebApp_DAL.Data;

namespace StoreWebApp.Tests.DataAccessTests.EFCore
{
    public class EFCProductRepTest
    {
        private DbContextOptions<StoreDbContext> _options;

        public void SetUp()
        {
        }

        [Fact]
        public void GetProducts_ReturnsListOfProducts()
        {
            Assert.True(true);
        }

        [Fact]
        public void GetProductById_GivenIdOne_ReturnsProductWithIdOne()
        {

        }
    }
}
