﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Store.Models
{
    public class Admin
    {
        public int ID { get; set; }

        [Required(ErrorMessage ="Please enter a username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}