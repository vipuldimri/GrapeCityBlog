using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DTO.CreateDTO
{
    public class BlogForCreationDTO {
       
        [Required]
        [MaxLength(15)]
        [MinLength(3)]
        public string Title { get; set; }



        [Required]
        [MaxLength(1000)]
        [MinLength(15)]
        public string Text { get; set; }
    }
}
