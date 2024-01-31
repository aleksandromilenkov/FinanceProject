using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMappers
    {
        public static CommentDTO ToCommentDTO(this Comment commentModel)
        {
            return new CommentDTO
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                CreatedBy = commentModel.AppUser.UserName,
                StockId = commentModel.StockId
            };
        }

        public static Comment ToCommentFromCreateCommentDTO(this CreateCommentRequestDTO model, int stockId)
        {
            return new Comment
            {
                Title = model.Title,
                Content = model.Content,
                StockId = stockId,
            };
        }
        public static Comment ToCommentFromUpdateCommentDTO(this UpdateCommentRequestDTO model, int commentId, int stockId)
        {
            return new Comment
            {
                Id = commentId,
                Title = model.Title,
                Content = model.Content,
                StockId = stockId
            };
        }

    }
}