using Microsoft.AspNetCore.Mvc;

namespace FirstApiWeb.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    // "База данных" в памяти
    private static readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Apple", Price = 1.5m },
        new Product { Id = 2, Name = "Bread", Price = 2.2m },
        new Product { Id = 3, Name = "Peta", Price = 3.3m },
    };

    private static int _nextId = 3;

    [HttpGet]
    public ActionResult<List<Product>> GetAll()
        => Ok(_products);

    [HttpGet("{id:int}")]
    public ActionResult<Product> GetById(int id)
    {
        var p = _products.FirstOrDefault(x => x.Id == id);
        return p is null ? NotFound() : Ok(p);
    }

    public record CreateProductDto(string Name, decimal Price);

    [HttpPost]
    public ActionResult<Product> Create([FromBody] CreateProductDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            return BadRequest("Name is required");

        var p = new Product { Id = _nextId++, Name = dto.Name, Price = dto.Price };
        _products.Add(p);

        return CreatedAtAction(nameof(GetById), new { id = p.Id }, p);
    }
//.........
    public record UpdateProductDto(string Name, decimal Price);

    [HttpPut("{id:int}")]
    public ActionResult<Product> Update(int id, [FromBody] UpdateProductDto dto)
    {
        var p = _products.FirstOrDefault(x => x.Id == id);
        if (p is null) return NotFound();

        p.Name = dto.Name;
        p.Price = dto.Price;

        return Ok(p);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var p = _products.FirstOrDefault(x => x.Id == id);
        if (p is null) return NotFound();

        _products.Remove(p);
        return NoContent();
    }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
}
