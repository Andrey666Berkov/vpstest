using Microsoft.AspNetCore.Mvc;

namespace FirstApiWeb.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<List<Product>> GetAll()
        => Ok(_service.GetAll());

    [HttpGet("{id:int}")]
    public ActionResult<Product> GetById(int id)
    {
        var p = _service.GetById(id);
        return p is null ? NotFound() : Ok(p);
    }

    public record CreateProductDto(string Name, decimal Price);

    [HttpPost]
    public ActionResult<Product> Create([FromBody] CreateProductDto dto)
    {
        var (ok, error, product) = _service.Create(dto.Name, dto.Price);
        if (!ok) return BadRequest(error);

        return CreatedAtAction(nameof(GetById), new { id = product!.Id }, product);
    }

    public record UpdateProductDto(string Name, decimal Price);

    [HttpPut("{id:int}")]
    public ActionResult<Product> Update(int id, [FromBody] UpdateProductDto dto)
    {
        var ok = _service.Update(id, dto.Name, dto.Price, out var updated);
        if (!ok) return NotFound();

        return Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var ok = _service.Delete(id);
        return ok ? NoContent() : NotFound();
    }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }

    public Product(string name, decimal price)
    {
        Price = price;
        Name = name;
    }
}
