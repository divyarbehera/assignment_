using ApiTest.Contracts.Database;
using ApiTest.Contracts.DTO;
using ApiTest.Contracts.Repository;
using ApiTest.Entity.Repository;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ApiTest.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ProductsController(IProductRepository _productRepo, IMapper _mapper) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(CreateProductRequsestDTO productRequestDto)
    {
        var existingProduct = await _productRepo.GetByNameAsync(productRequestDto.Name);
        if (existingProduct != null)
        {
            return BadRequest($"Product with name '{productRequestDto.Name}' already exists!");
        }
        var product = _mapper.Map<Product>(productRequestDto);
        await _productRepo.AddAsync(product);

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }
    [HttpGet]
    public async Task<IEnumerable<Product>> GetProducts() => await _productRepo.GetAllAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _productRepo.GetByIdAsync(id);
        if (product != null)
        {
            return Ok(product);
        }
        return NotFound($"The product you are looking for is not available!");
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchProduct(int id, [FromBody] JsonPatchDocument<Product> patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest();
        }

        var product = await _productRepo.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        patchDoc.ApplyTo(product, ModelState);

        if (!TryValidateModel(product))
        {
            return BadRequest(ModelState);
        }

        var original = product with { };
        
        if (product != original)
        {
            var existingProduct = await _productRepo.GetByNameAsync(product.Name);
            if (existingProduct != null)
            {
                return BadRequest($"Product with name '{product.Name}' already exists!");
            }
            await _productRepo.UpdateAsync(product);
        }

        return Ok(product);
    }
}
