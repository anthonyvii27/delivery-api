using AutoMapper;
using basic_delivery_api.Domain.Models;
using basic_delivery_api.Domain.Services;
using basic_delivery_api.Dto;
using basic_delivery_api.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace basic_delivery_api.Controllers
{
    public class SalesController : BaseApiController
    {
        private readonly ISaleService _saleService;
        private readonly IMapper _mapper;

        public SalesController(ISaleService saleService, IMapper mapper)
        {
            _saleService = saleService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleDto>>> GetAll()
        {
            var sales = await _saleService.ListAsync();
            var response = _mapper.Map<IEnumerable<SaleDto>>(sales);
            return Ok(response);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sale = await _saleService.FindByIdAsync(id);
            if (sale == null)
                return NotFound($"Sale with ID {id} not found.");

            var saleDto = _mapper.Map<SaleDto>(sale);
            return Ok(saleDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSaleDto body)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var sale = _mapper.Map<CreateSaleDto, Sale>(body);
            var result = await _saleService.Create(sale);

            if (!result.Success)
                return BadRequest(result.Message);

            var saleResponse = _mapper.Map<Sale, SaleDto>(result.Sale);
            return Ok(saleResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSaleDto body)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var currentSale = await _saleService.FindByIdAsync(id);
            if (currentSale == null)
                return NotFound($"Sale with ID {id} not found.");

            _mapper.Map(body, currentSale);
            var result = await _saleService.Update(id, currentSale);

            if (!result.Success)
                return BadRequest(result.Message);

            var saleResponse = _mapper.Map<SaleDto>(result.Sale);
            return Ok(saleResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _saleService.Delete(id);

            if (!result.Success)
                return BadRequest(result.Message);

            return NoContent();
        }
    }
}