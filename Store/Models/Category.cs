using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class Category
    {
        public int ID { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3)]
        [Display(Name = "Category")]
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}