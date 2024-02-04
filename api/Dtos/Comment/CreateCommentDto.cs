using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(2, ErrorMessage = "Title must be atleast 2 characters")]
        [MaxLength(80, ErrorMessage = "Title can't exceed 80 characters")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(3, ErrorMessage = "Content must be atleast 3 characters")]
        [MaxLength(280, ErrorMessage = "Content can't exceed 280 characters")]
        public string Content { get; set; } = string.Empty;
    }
}