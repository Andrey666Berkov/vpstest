namespace FirstApiWeb.Controllers;

public class ProductsService
{
    // пока что in-memory "хранилище"
    private static readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Apple", Price = 1.5m },
        new Product { Id = 2, Name = "Bread", Price = 2.2m },
        new Product { Id = 3, Name = "Peta", Price = 3.3m },
        new Product { Id = 4, Name = "GOGa", Price = 7.7m },
    };

    private static int _nextId = _products.Max(p => p.Id) + 1;

    public IReadOnlyList<Product> GetAll() => _products;

    public Product? GetById(int id) =>
        _products.FirstOrDefault(x => x.Id == id);

    public (bool ok, string? error, Product? product) Create(string name, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name))
            return (false, "Name is required", null);

        var p = new Product { Id = _nextId++, Name = name, Price = price };
        _products.Add(p);

        return (true, null, p);
    }

    public bool Update(int id, string name, decimal price, out Product? updated)
    {
        updated = _products.FirstOrDefault(x => x.Id == id);
        if (updated is null) return false;

        updated.Name = name;
        updated.Price = price;
        return true;
    }

    public bool Delete(int id)
    {
        var p = _products.FirstOrDefault(x => x.Id == id);
        if (p is null) return false;

        _products.Remove(p);
        return true;
    }
}