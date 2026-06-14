using Jew.Applications.Sales.DTOs.Clients;
using Jew.Domain.Sales.Exceptions;
using Jew.Infrastructure.UnitOfWork;

namespace Jew.Applications.Sales.Commands;

public sealed class ClientCommands(IUnitOfWork context)
{
    public void AddClient(ClientDto client)
    {
        ArgumentNullException.ThrowIfNull(client);

        if(context.Clients.Exist(client.Code))
            throw new ClientException("Este cliente ya existe");

        context.Clients.Add(new(client.Code, client.Name));
        context.Clients.SaveChanges();
    }
}