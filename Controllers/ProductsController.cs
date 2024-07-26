using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using TodoApi.Models;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ISqlSugarClient _db;
    private readonly IMapper _mapper;

    public ProductsController(ISqlSugarClient db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Query()
    {
        var products = await _db.Queryable<Product>().ToListAsync();
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> Post(ProductCreation productCreation)
    {
        System.Console.Error.WriteLine(
            $"🟥[1]: ProductsController.cs:31: productCreation={productCreation}"
        );
        var product = _mapper.Map<Product>(productCreation);
        System.Console.Error.WriteLine($"🟥[2]: ProductsController.cs:32: product={product}");
        await _db.Insertable(product).ExecuteCommandAsync();
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var product = await _db.Queryable<Product>().Where(p => p.Id == id).FirstAsync();
        if (product == null)
        {
            return new NotFoundResult();
        }
        return new OkObjectResult(product);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(int id, Product product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }

        var result = await _db.Updateable(product).ExecuteCommandAsync();
        return new OkObjectResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _db.Deleteable<Product>(id).ExecuteCommandAsync();
        return NoContent();
    }
}
