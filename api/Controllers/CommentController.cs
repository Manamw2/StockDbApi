using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("Api/[Controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _CommentRepo;
        private readonly IStockRepository _StockRepo;
        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
        {
            _CommentRepo = commentRepo;
            _StockRepo = stockRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll(){
            if(!ModelState.IsValid){
                return BadRequest();
            }
            var comments = await _CommentRepo.GetAllAsync();
            return Ok(comments.Select(s => s.CommentToCommentDto()));
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id){
            if(!ModelState.IsValid){
                return BadRequest();
            }
            var comment = await _CommentRepo.GetById(id);
            if(comment == null){
                return NotFound();
            }
            return Ok(comment.CommentToCommentDto());
        }

        [HttpPost("{stockId:int}")]
        [Authorize]
        public async Task<IActionResult> Create(int stockId, CreateCommentDto createCommentDto){
            if(!ModelState.IsValid){
                return BadRequest();
            }
            if(!await _StockRepo.StockExist(stockId)){
                return BadRequest("Stock does not found.");
            }
            var comment = createCommentDto.ToCommentFromCreateDto(stockId);
            await _CommentRepo.Create(comment);
            return CreatedAtAction(nameof(GetById), new {Id = comment.Id}, comment.CommentToCommentDto());
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentDto updateCommentDto){
            if(!ModelState.IsValid){
                return BadRequest();
            }
            var comment = await _CommentRepo.Update(updateCommentDto.ToCommentFromUpdateDto(), id);
            if(comment == null){
                return NotFound();
            }
            return Ok(comment.CommentToCommentDto());
        }
        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id){
            if(!ModelState.IsValid){
                return BadRequest();
            }
            var comment = await _CommentRepo.Delete(id);
            if(comment == null){
                return NotFound();
            }
            return NoContent();
        }
    }
}