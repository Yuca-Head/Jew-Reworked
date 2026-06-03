using Jew.Applications.ProductInventory;
using Jew.Applications.ProductInventory.Commands;
using Jew.Applications.ProductInventory.DTOs;
using Jew.Domain.ProductInventory.Entities;
using Jew.Domain.ProductInventory.Exceptions;
using Jew.Domain.ProductInventory.Repositories;
using Jew.Infrastructure.Repositories.InMemory;
using Jew.Infrastructure.UnitOfWork;
using Moq;
namespace Jew.Test.Application;

public class InventoryTests
{
    [Fact]
    public void Test1()
    {
        
    }

    [Fact]
    public void Add_Product_By_Inventory()
    {
        // Arrange
        var context = new Mock<IUnitOfWork>();
        var inMem = new InMemoryProducts([]);

        var fP = new CreateProductDto("P001", "PC", "Tech");

        var service = new InventoryCommands(context.Object);
        
        Assert.False(inMem.Exist(fP.Code));
        service.AddProduct(fP);
   
        var product = new CreateProductDto("P001", "Mame", "Tech");

        // Act & Assert
        Assert.Throws<InventoryException>(() =>
        {
            service.AddProduct(product);
        });
    }

    [Fact]
    public void FastTest()
    {
    }
}
