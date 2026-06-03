using Jew.Applications.InventoryMovements.DTOs;
using Jew.Domain.InventoryMovements.Entities;

namespace Jew.Applications.InventoryMovements.Mappers;

public static class MovementMappers
{
    public static IEnumerable<MovementDto> ConvertMovementsToDto(IEnumerable<InventoryMovement> movements)
    => movements.Select(MovementDto.From);
}