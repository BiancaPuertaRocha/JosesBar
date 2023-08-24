using Azure;
using JosesBarAPI.Controllers;
using JosesBarAPI.Dtos;
using JosesBarAPI.Entities;
using JosesBarAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace JosesBarAPI.tests
{
    public class ProductTests
    {
        private List<Product> products = new List<Product>()
            {
                new Product() { Id = 1, Description = "skol lata 350ml", Price = 2.3m, Quantity = 10},
                new Product() { Id = 2, Description = "coca-Cola lata 350ml", Price = 3.50m, Quantity = 15},
                new Product() { Id = 3, Description = "suco de Uva 500ml", Price = 5.50m, Quantity = 50}
            };
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly ProductController _productController;

        private UpdateProduct updateProduct = new UpdateProduct() { Description = "sal" };
        private Product updatedProduct = new Product() { Id = 3, Description = "sal", Price = 3.50m, Quantity = 15 };

        private CreateProduct createProduct = new CreateProduct() { Description = "salgadinho 200g", Price = 1.50m, Quantity = 15 };
        private Product createdProduct = new Product() { Id = 4, Description = "salgadinho 200g", Price = 1.50m, Quantity = 15 };


        public ProductTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _productController = new ProductController(_productRepositoryMock.Object);

            _productRepositoryMock.Setup(r => r.GetProducts().Result).Returns(products);
            _productRepositoryMock.Setup(r => r.GetProductByID(2).Result).Returns(products.Find(x => x.Id == 2));
            _productRepositoryMock.Setup(r => r.GetProductByDescription("s").Result).Returns(products.FindAll(x => x.Description.Contains("s")));
            _productRepositoryMock.Setup(r => r.DeleteProduct(2).Result).Returns(true);
            _productRepositoryMock.Setup(r => r.InsertProduct(this.createProduct).Result).Returns(this.createdProduct);
            _productRepositoryMock.Setup(r => r.UpdateProduct(this.updateProduct, 3).Result).Returns(updatedProduct); 


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

        [Fact]
        public void List_GetProductByDescription_FilteredProducts()
        {
            var result = _productController.GetAsync(description:"s").Result;
            var okResult = result as OkObjectResult;
            var actualResult = okResult.Value as List<Product>;

            Assert.Equal(2, actualResult.Count);
            Assert.All(actualResult, item => Assert.Contains("s", item.Description));
           
        }

        [Fact]
        public void Delete_DeleteProductById_Deleted()
        {
            var result = _productController.DeleteAsync(2).Result;
            var okResult = result as OkObjectResult;
            var actualResult = okResult.Value as bool?;

            Assert.True(actualResult);

        }

        [Fact]
        public async void Insert_InsertProduct_ProductInserted()
        {
            var response = await _productController.PostAsync(this.createProduct);

            CreatedResult okResult = Assert.IsType<CreatedResult>(response);

            dynamic value = okResult.Value as Product;
            Assert.NotNull(value);

            Assert.Equal(this.createdProduct, value);

        }

        [Fact]
        public async void Update_UpdateProduct_ProductUpdated()
        {

            var response = await _productController.PutAsync(3, this.updateProduct);

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(response);

            dynamic value = okResult.Value;
            Assert.NotNull(value);

            string desc = (string) value.Description;
            Assert.Equal(this.updatedProduct.Description, desc);


        }
    }
}