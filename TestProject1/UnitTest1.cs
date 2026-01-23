using FirstApiWeb.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestProject1;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        // arrange
        var mock = new Mock<IProductService>();
        mock.Setup(s => s.GetById(1))
            .Returns(new Product("Test", 10));

        var controller = new ProductsController(mock.Object);

        // act
        var action = controller.GetById(1);

        // assert
        var ok = Assert.IsType<OkObjectResult>(action.Result);
        Assert.NotNull(ok.Value);

        var product = Assert.IsType<Product>(ok.Value);
        Assert.Equal("Test", product.Name);
        //.....
    }
}