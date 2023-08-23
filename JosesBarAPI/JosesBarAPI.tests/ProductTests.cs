using JosesBarAPI.Controllers;
using JosesBarAPI.Entities;
using JosesBarAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace JosesBarAPI.tests
{
    public class ProductTests
    {
        private List<Product> products = new List<Product>()
            {
                new Product() { Id = 1, Description = "Skol lata 350ml", Price = 2.3m, Quantity = 10},
                new Product() { Id = 2, Description = "Coca-Cola lata 350ml", Price = 3.50m, Quantity = 15},
                new Product() { Id = 3, Description = "Suco de Uva 500ml", Price = 5.50m, Quantity = 50}
            };
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly ProductController _productController;


        public ProductTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _productController = new ProductController(_productRepositoryMock.Object);

            _productRepositoryMock.Setup(r => r.GetProducts().Result).Returns(products);
            _productRepositoryMock.Setup(r => r.GetProductByID(2).Result).Returns(products.Find(x => x.Id == 2));
        }


        [Fact]
        public void List_GetProducts_AllProducts()
        {
            var result = _productController.GetAsync().Result;
            var okResult = result as OkObjectResult;
            var actualResult = okResult.Value as List<Product>;

            Assert.Equal(products, actualResult);
        }

        [Fact]
        public void List_GetProductByID_OneProduct()
        {
            var result = _productController.GetAsync(2).Result;
            var okResult = result as OkObjectResult;
            var actualResult = okResult.Value as Product;

            Assert.Equal(2, actualResult.Id);
        }
    }
}