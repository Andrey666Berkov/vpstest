using FirstApiWeb.Controllers;

namespace TestProject1;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        //arrange
        var controller = new ProductsService();
        
        //act
        var result=controller.GetById(1);
        
        //assert
        if (result != null) Assert.Equal("Apple", result.Name);
    }
}