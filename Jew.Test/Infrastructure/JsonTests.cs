using Jew.Applications.ProductInventory.DTOs;
using Jew.Applications.Purchases.Commands;
using Jew.Applications.Purchases.DTOs.Suppliers;
using Jew.Domain.ProductInventory.Entities;
using Jew.Domain.Shared.Keys;
using Jew.Infrastructure.Repositories.Json;

/*
namespace Jew.Test.Infrastructure;

public class JsonTests
{

    [Fact]
    public void CanSerializeAndDeserializeJsonCategories()
    {
        //Arrange
        var category = new Category("Tech", "Technology products");

        //Act
        JsonCategoriesRepo jsonRepo = new(Jew.Infrastructure.Enums.Environment.Test);
        jsonRepo.Add(category);
        
        jsonRepo.SaveChanges();
        //Asserts
        //Assert.Equal(jsonRepo.GetById(1), category);
        Assert.Equal("Tech", category.Key);
    }

    [Fact]
    public void SavesServicesWellWorking()
    {
        //Init
        JsonUnitOfWork context = new(Jew.Infrastructure.Enums.Environment.Test);
        
        
        //Inserts
        context.Categories.Add(new("Carne"));
        context.Products.Add(new("Carne-Mol", "Carne molida", context.Categories.GetById("Carne") ?? throw new Exception()));
        context.Clients.Add(new(new(5, "MARIO"), "Marío Gúzman"));
        context.Suppliers.Add(new(0, "Miguelazo.Com")); 

        //Peep-Peep
        context.SaveChanges();
    }

    [Fact]  
    public void LoadServicesWellWorking()
    {
        //Init
        JsonUnitOfWork context = new(Jew.Infrastructure.Enums.Environment.Test);
        context.Load();
        //Asserts
        Assert.NotNull(context.Categories.GetById("Tech"));
    }

    [Fact]
    public void SaveProdutsToSupplier()
    {
        JsonUnitOfWork context = new(Jew.Infrastructure.Enums.Environment.Test);
        context.Load();
        SupplierCommands commands = new(context);
        SupplierDto miguelazo = new(1, "Miguelazo.Com");
        commands.AddProduct(new(miguelazo, ProductDto.From(context.Products.GetById("Carne-Mol")), 20));
        context.SaveChanges();
    }
}*/