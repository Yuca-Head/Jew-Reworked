namespace Jew.Applications.ProductInventory.DTOs;

//Quizás Categoría también...
public sealed record ModifyProductDto(string Code, string? NewName, bool? NewState);