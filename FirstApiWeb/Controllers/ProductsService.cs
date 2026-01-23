using Microsoft.EntityFrameworkCore;

namespace FirstApiWeb.Controllers;

public class ProductsService : IProductService
{
    private readonly AppDbContext _db;

    public ProductsService(AppDbContext db)
    {
        _db = db;
    }

    public IReadOnlyList<Product> GetAll()
        => _db.Products
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .ToList();

    public Product? GetById(int id)
        => _db.Products
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == id);

    public (bool ok, string? error, Product? product) Create(string name, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name))
            return (false, "Name is required", null);

        var p = new Product ( name, price );

        _db.Products.Add(p);
        _db.SaveChanges(); // после этого p.Id заполнится из БД

        return (true, null, p);
    }

    public bool Update(int id, string name, decimal price, out Product? updated)
    {
        var entity = _db.Products.FirstOrDefault(x => x.Id == id);
        if (entity is null)
        {
            updated = null;
            return false;
        }

        entity.Name = name;
        entity.Price = price;

        _db.SaveChanges();

        updated = entity;
        return true;
    }

    public bool Delete(int id)
    {
        var entity = _db.Products.FirstOrDefault(x => x.Id == id);
        if (entity is null) return false;

        _db.Products.Remove(entity);
        _db.SaveChanges();
        return true;
    }
}

public interface IProductService
{
    public IReadOnlyList<Product> GetAll();

    public Product? GetById(int id);

    public bool Delete(int id);

    public (bool ok, string? error, Product? product) Create(string name, decimal price);
    public bool Update(int id, string name, decimal price, out Product? updated);


}