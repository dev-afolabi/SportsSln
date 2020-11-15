using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace sportsStore.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void Can_Use_Repository()
        {
            //Arrange
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product {ProductId=1, Name="P1"},
                new Product {ProductId=2, Name="P2"}
            }).AsQueryable<Product>());

            HomeController controller = new HomeController(mock.Object);

            //Act
            ProductListViewModel result = (controller.Index(null) as ViewResult).ViewData.Model
                as ProductListViewModel;

            //Assert
            Product[] prodArray = result.Products.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("P1", prodArray[0].Name);
            Assert.Equal("P2", prodArray[1].Name);
        }

        [Fact]
        public void Can_Paginate()
        {
            //Arrange
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product {ProductId=1, Name="P1"},
                new Product {ProductId=2, Name="P2"},
                new Product {ProductId=3, Name="P3"},
                new Product {ProductId=4, Name="P4"},
                new Product {ProductId=5, Name="P5"}
            }).AsQueryable<Product>());

            HomeController controller = new HomeController(mock.Object);
            controller.PageSize = 3;

            //Act
            ProductListViewModel result = 
                (controller.Index(null,2) as ViewResult).ViewData.Model as ProductListViewModel;

            //Assert
            Product[] prodArray = result.Products.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("P4", prodArray[0].Name);
            Assert.Equal("P5", prodArray[1].Name);
        }

        [Fact]
        public void Can_Send_Pagination_View_Model()
        {
            //Arrange
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product {ProductId=1, Name="P1"},
                new Product {ProductId=2, Name="P2"},
                new Product {ProductId=3, Name="P3"},
                new Product {ProductId=4, Name="P4"},
                new Product {ProductId=5, Name="P5"}
            }).AsQueryable<Product>());

            HomeController controller = new HomeController(mock.Object) { PageSize = 3 };

            //Act
            ProductListViewModel result =
                (controller.Index(null,2) as ViewResult).ViewData.Model as ProductListViewModel;

            //Assert
            PagingInfo pagingInfo = result.PagingInfo;
            Assert.Equal(2, pagingInfo.CurrentPage);
            Assert.Equal(3, pagingInfo.ItemsPerPage);
            Assert.Equal(5, pagingInfo.TotalItems);
            Assert.Equal(2, pagingInfo.TotalPages);
        }
    }
}
