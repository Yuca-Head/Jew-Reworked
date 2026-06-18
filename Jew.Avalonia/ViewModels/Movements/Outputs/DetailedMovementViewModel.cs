using Jew.Applications.ProductInventory.DTOs;

namespace Jew.Avalonia.ViewModels.Movements.Outputs;

public record DetailedMovementViewModel(ProductWithCategoryDto Product, MovementViewModel Movement);