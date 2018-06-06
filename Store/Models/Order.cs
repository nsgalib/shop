﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Store.Models
{
    public class Order
    {
        [Display(Name = "Order ID")]
        public int OrderID { get; set; }

        //[Display(Name = "User")]
        //public string UserID { get; set; }

        //[Display(Name = "Deliver to")]
        //public string DeliveryName { get; set; }

        //[Display(Name = "Delivery Address")]
        //public string Address { get; set; }

        [Display(Name = "Total Price")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalPrice { get; set; }

        public List<OrderLine> OrderLines { get; set; }
        public string UserID { get; set; }
    }
}