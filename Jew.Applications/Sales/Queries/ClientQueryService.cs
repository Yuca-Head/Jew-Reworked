using Jew.Applications.Sales.DTOs.Clients;
using Jew.Domain.Sales.Exceptions;
using Jew.Domain.Shared.Keys;
using Jew.Infrastructure.UnitOfWork;

namespace Jew.Applications.Sales.Queries;

public sealed class ClientQueryService(IUnitOfWork context)
{
    public bool ClientExists(CodeKey codeKey)
    => context.Clients.Exist(codeKey);

    public bool ClientExists(string code)
    => string.IsNullOrWhiteSpace(code) || code.Length != 5 ? 
        throw new ClientException("Tipo de código no válido (debe contener 5 caracteres)") : 
        ClientExists(new CodeKey(5, code.Take(5).ToString()));

    public ClientDto GetClient(CodeKey key)
    => ClientDto.From(context.Clients.GetById(key) ?? 
    throw new ClientException($"Cliente con código {key.Key} no encontrado "));
    public IEnumerable<ClientDto> GetClients()
    => context.Clients.GetAll().Select(ClientDto.From);
}