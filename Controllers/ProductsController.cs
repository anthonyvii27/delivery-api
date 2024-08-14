using AutoMapper;
using basic_delivery_api.Domain.Models;
using basic_delivery_api.Domain.Services;
using basic_delivery_api.Dto;
using basic_delivery_api.Extensions;
using basic_delivery_api.Resources;
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
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            var products = await _productService.ListAsync();
            var response = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.FindByIdAsync(id);
            if (product == null)
                return NotFound($"Product with ID {id} not found.");

            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="resource">The product data to create.</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var product = _mapper.Map<Product>(resource);
            var result = await _productService.Create(product);

            if (!result.Success)
                return BadRequest(result.Message);

            var productResponse = _mapper.Map<ProductDto>(result.Product);
            return CreatedAtAction(nameof(GetById), new { id = result.Product.Id }, productResponse);
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="body">The updated product data.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDto body)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var currentProduct = await _productService.FindByIdAsync(id);
            if (currentProduct == null)
                return NotFound($"Product with ID {id} not found.");

            _mapper.Map(body, currentProduct);
            var result = await _productService.Update(id, currentProduct);

            if (!result.Success)
                return BadRequest(result.Message);

            var productResponse = _mapper.Map<ProductDto>(result.Product);
            return Ok(productResponse);
        }

        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.Delete(id);

            if (!result.Success)
                return BadRequest(result.Message);

            return NoContent();
        }
    }
}