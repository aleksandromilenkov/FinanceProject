using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO.Comment
{
    public class UpdateCommentRequestDTO
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be 5 or more characters long")]
        [MaxLength(280, ErrorMessage = "Title cannot be over 280 characters long")]
        public string Title { get; set; } = String.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Content must be 5 or more characters long")]
        [MaxLength(280, ErrorMessage = "Content cannot be over 280 characters long")]
        public string Content { get; set; } = String.Empty;
    }
}