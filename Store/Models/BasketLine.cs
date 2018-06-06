using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Store.Models
{
    public class BasketLine
    {
        public int ID { get; set; }
        public string BasketID { get; set; }
        public int ProductID { get; set; }
        [Range(0, 10, ErrorMessage = "Please enter a quantity between 0 and 10")]
        public int Quantity { get; set; }
        public virtual Product Product { get; set; }
    }
}