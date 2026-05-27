using Jew.Domain.ProductInventory.Entities;
using Jew.Infrastructure.Repositories.Json;

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
        Assert.True(1 == category.Key);
    }
}