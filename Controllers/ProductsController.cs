using AutoMapper;
using basic_delivery_api.Domain.Models;
using basic_delivery_api.Domain.Services;
using basic_delivery_api.Requests;
using basic_delivery_api.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace basic_delivery_api.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductRequest>>> GetAll()
        {
            var products = await _productService.ListAsync();
            var productDtos = _mapper.Map<IEnumerable<ProductRequest>>(products);
            return Ok(productDtos);
        }

        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductRequest>> GetById(int id)
        {
            var product = await _productService.FindByIdAsync(id);
            if (product == null)
                return NotFound(new { Message = $"Product with ID {id} not found." });

            var productDto = _mapper.Map<ProductRequest>(product);
            return Ok(productDto);
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="body">The product data to create.</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest body)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var product = _mapper.Map<Product>(body);
            var result = await _productService.Create(product);

            if (!result.Success)
                return BadRequest(new { Message = result.Message });

            var productResponse = _mapper.Map<ProductRequest>(result.Product);
            return CreatedAtAction(nameof(GetById), new { id = result.Product.Id }, productResponse);
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="body">The updated product data.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductRequest body)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var currentProduct = await _productService.FindByIdAsync(id);
            if (currentProduct == null)
                return NotFound(new { Message = $"Product with ID {id} not found." });

            _mapper.Map(body, currentProduct);
            var result = await _productService.Update(id, currentProduct);

            if (!result.Success)
                return BadRequest(new { Message = result.Message });

            var productResponse = _mapper.Map<ProductRequest>(result.Product);
            return Ok(productResponse);
        }

        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _productService.HasAssociatedSalesAsync(id))
            {
                return Conflict(new { Message = "Cannot delete the product as it has associated sale items." });
            }

            var result = await _productService.Delete(id);

            if (!result.Success)
                return BadRequest(new { Message = result.Message });

            return NoContent();
        }
    }
}