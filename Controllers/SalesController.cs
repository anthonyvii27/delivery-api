using AutoMapper;
using basic_delivery_api.Domain.Models;
using basic_delivery_api.Domain.Services;
using basic_delivery_api.Requests;
using basic_delivery_api.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace basic_delivery_api.Controllers
{
    public class SalesController : BaseApiController
    {
        private readonly ISaleService _saleService;
        private readonly IShippingService _shippingService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public SalesController(ISaleService saleService, IShippingService shippingService, IProductService productService, IMapper mapper)
        {
            _saleService = saleService;
            _shippingService = shippingService;
            _productService = productService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all sales.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleRequest>>> GetAll()
        {
            var sales = await _saleService.ListAsync();
            var saleDtos = _mapper.Map<IEnumerable<SaleRequest>>(sales);
            return Ok(saleDtos);
        }

        /// <summary>
        /// Retrieves a sale by its ID.
        /// </summary>
        /// <param name="id">The ID of the sale to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<SaleRequest>> GetById(int id)
        {
            var sale = await _saleService.FindByIdAsync(id);
            if (sale == null)
                return NotFound(new { Message = $"Sale with ID {id} not found." });

            var saleDto = _mapper.Map<SaleRequest>(sale);
            return Ok(saleDto);
        }

        /// <summary>
        /// Creates a new sale.
        /// </summary>
        /// <param name="body">The sale data to create.</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSaleRequest body)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            
            if (body.SaleItems == null || !body.SaleItems.Any())
                return BadRequest(new { Message = "At least one SaleItem is required." });

            var productIds = body.SaleItems.Select(si => si.ProductId).Distinct();
    
            var products = await _productService.GetProductsByIdsAsync(productIds);
            var productIdsInDb = products.Select(p => p.Id);

            if (productIds.Except(productIdsInDb).Any())
                return BadRequest(new { Message = "One or more products in the SaleItems do not exist." });

            var shippingCost = await _shippingService.CalculateShippingCostAsync(body.ZipCode);

            var sale = _mapper.Map<Sale>(body);
            sale.ShippingCost = shippingCost;

            var result = await _saleService.Create(sale);

            if (!result.Success)
                return BadRequest(new { Message = result.Message });

            var saleResponse = _mapper.Map<SaleRequest>(result.Sale);
            return CreatedAtAction(nameof(GetById), new { id = result.Sale.Id }, saleResponse);
        }

        /// <summary>
        /// Deletes a sale by its ID.
        /// </summary>
        /// <param name="id">The ID of the sale to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _saleService.Delete(id);

            if (!result.Success)
                return BadRequest(new { Message = result.Message });

            return NoContent();
        }
    }
}
