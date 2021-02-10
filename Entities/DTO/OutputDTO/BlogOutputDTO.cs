using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTO.OutputDTO
{
    public class BlogOutputDTO
    {
        public int BlogId { get; set; }
        public DateTime CreationDate { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
