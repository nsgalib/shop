using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class Product
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

       
        public string ImageURL { get; set; }

        public int? CategoryID { get; set; }
        public virtual Category Category { get; set; }
    }
}