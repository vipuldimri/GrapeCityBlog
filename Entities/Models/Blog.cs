using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class Blog
    {
        [Column("BlogId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int BlogId { get; set; }


        [Required]
        public DateTime CreationDate { get; set; }


        [Required]
        [MaxLength(15)]
        [MinLength(3)]
        public string Title { get; set; }



        [Required]
        [MaxLength(1000)]
        [MinLength(15)]
        public string Text { get; set; }



        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }

    }
}
