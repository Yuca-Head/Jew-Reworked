using System.Threading.Tasks;
using Jew.Applications.Sales.DTOs.Clients;
using Jew.Applications.Shared.UnitsOfWork;
using Jew.Domain.Sales.Exceptions;
using Jew.Domain.Shared.Keys;


namespace Jew.Applications.Sales.Queries;

public sealed class ClientQueryService(IUnitOfWork context)
{
    public Task<bool> ClientExistsAsync(CodeKey codeKey)
    => context.Clients.ExistsAsync(codeKey);

    public Task<bool> ClientExists(string code)
    => string.IsNullOrWhiteSpace(code) || code.Length != 5 ? 
        throw new ClientException("Tipo de código no válido (debe contener 5 caracteres)") : 
        ClientExistsAsync(new CodeKey(5, code.Take(5).ToString()));

    public async Task<ClientDto> GetClient(CodeKey key)
    => ClientDto.From(await context.Clients.GetByIdAsync(key) ?? 
    throw new ClientException($"Cliente con código {key.Key} no encontrado "));
    public async Task<IEnumerable<ClientDto>> GetClients()
    => (await context.Clients.GetAllAsync()).Select(ClientDto.From);
}