
using Jew.Domain.Purchases.Entities;
using Jew.Domain.Purchases.Repositories;
using Jew.Infrastructure.Repositories.Test;


namespace Jew.Infrastructure.Repositories;

public class InMemorySuppliers : ISuppliersRepo
{
    private readonly Dictionary<int, Supplier> _suppliers = [];

    public InMemorySuppliers()
    => Collections.SuppliersList.ForEach(x => Add(x));

    private int idCount = 1;
    public void Add(Supplier entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        var id = idCount++;

        _suppliers.Add(id, new(entity){Key = id});
    }

    public IEnumerable<Supplier> GetAll() 
    => _suppliers.Values.ToList();
    public Supplier? GetById(int id)
    => _suppliers.GetValueOrDefault(id);
    public Supplier? GetByName(string name)
    => _suppliers.Values.FirstOrDefault(s => string.Equals(s.Name, name, StringComparison.OrdinalIgnoreCase));
    public bool Exist(int key)
    => _suppliers.ContainsKey(key);
}