using System.Threading.Tasks;
using Jew.Applications.Sales.DTOs.Clients;
using Jew.Applications.Shared.UnitsOfWork;
using Jew.Domain.Sales.Entities;
using Jew.Domain.Sales.Exceptions;

namespace Jew.Applications.Sales.Commands;

public sealed class ClientCommands(IUnitOfWork context)
{
    public async Task AddClient(ClientDto client)
    {
        ArgumentNullException.ThrowIfNull(client);

        if(await context.Clients.ExistsAsync(client.Code))
            throw new ClientException("Este cliente ya existe");

        await context.Clients.AddAsync(new Client(client.Code, client.Name));
        await context.Clients.SaveChangesAsync();
    }
}