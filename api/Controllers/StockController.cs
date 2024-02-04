using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route ("api/[Controller]")]
    [ApiController]
    public class StockController: ControllerBase
    {
        private readonly IStockRepository _StockRepo;
        public StockController(IStockRepository stockRepo)
        {
            _StockRepo = stockRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] GetStocksObject obj){
            if(!ModelState.IsValid){
                return BadRequest();
            }
            var stocks = await _StockRepo.GetAllAsync(obj);
            var stocksDto = stocks.Select(s => s.ToStockDto());
            return Ok(stocksDto);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id){
            if(!ModelState.IsValid){
                return BadRequest();
            }
            var stock = await _StockRepo.GetByIdAsync(id);

            if(stock == null){
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateStockDto createStockDto){
            if(!ModelState.IsValid){
                return BadRequest();
            }
            var stock = createStockDto.ToCreateStockDto();
            await _StockRepo.CreateAsync(stock);
            return CreatedAtAction(nameof(GetById), new {Id = stock.Id}, stock);
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockDto updateStockDto){
            if(updateStockDto == null || !ModelState.IsValid){
                return BadRequest();
            }

            var stock = await _StockRepo.UpdateAsync(id, updateStockDto);

            if(stock == null){
                return NotFound();
            }
            
            return Ok(stock.ToStockDto());
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> delete([FromRoute] int id){
            if(!ModelState.IsValid){
                return BadRequest();
            }
            var stock = await _StockRepo.DeleteAsync(id);

            if(stock == null){
                return NotFound();
            }

            return NoContent();
        }
    }
}