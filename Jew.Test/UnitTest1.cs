using Jew.Application.ProductInventory;
using Jew.Domain.ProductInventory.Entities;
using Jew.Domain.ProductInventory.Exceptions;
using Jew.Domain.ProductInventory.Repositories;
using Jew.Infrastructure.Repositories;
using Moq;
namespace Jew.Test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        
    }

    [Fact]
    public void Add_Product_By_Inventory()
    {
        // Arrange
        var repoMock = new Mock<IProductsRepo>();
        var catRepoMock = new Mock<ICategoriesRepo>();
        var inMem = new InMemoryProducts(false);

        var fP = new Product("P001", "PC", new("Tech"));

        var service = new InventoryService(inMem,catRepoMock.Object);
        
        Assert.False(inMem.Exist(fP.Code));
        service.Add(fP);
        Assert.Equal(fP, inMem.GetById(fP.Code));
   
        var product = new Product
        {
            Code = "P001"
        };

        // Act & Assert
        Assert.Throws<InventoryException>(() =>
        {
            service.Add(product);
        });
    }

    [Fact]
    public void FastTest()
    {
    }
}
