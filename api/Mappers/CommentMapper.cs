using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Controllers;
using api.Dtos.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto CommentToCommentDto(this Comment comment){
            return new CommentDto{
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId
            };
        }
        public static Comment ToCommentFromCreateDto(this CreateCommentDto createCommentDto, int stockId){
            return new Comment{
                Title = createCommentDto.Title,
                Content = createCommentDto.Content,
                StockId = stockId
            };
        }

        public static Comment ToCommentFromUpdateDto(this UpdateCommentDto updateCommentDto){
            return new Comment{
                Title = updateCommentDto.Title,
                Content = updateCommentDto.Content,
            };
        }
    }

}