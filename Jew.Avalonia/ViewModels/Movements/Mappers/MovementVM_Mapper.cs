using Jew.Applications.InventoryMovements.DTOs;
using Jew.Avalonia.ViewModels.Movements.Outputs;
using Jew.Infrastructure.Persistence.Mappers.Shared;

namespace Jew.Avalonia.ViewModels.Movements.Mappers;

public sealed class MovementVM_Mapper : IMapper<MovementDto, MovementViewModel>
{
    public MovementDto ToEntity(MovementViewModel viewModel)
    => new(viewModel.ProductCode, viewModel.Quantity, viewModel.Cost, viewModel.MovementType, default, viewModel.Id);

    public MovementViewModel ToModel(MovementDto dto)
    => new(dto.ProductId, dto.Id, dto.Quantity, dto.UnitCost, dto.MovementType);
    
}