﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    public class Purchase
    {
        [Key]
        [BindNever] //this info can't be passed through the URL
        public int PurchaseId { get; set; }

        [BindNever]
        public ICollection<BasketLineItem> Lines { get; set; }

        [Required(ErrorMessage = "Please enter a name:")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Please enter the first address line")]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }

        [Required(ErrorMessage ="Please Enter a City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please Enter a State")]
        public string State { get; set; }
        public string Zip { get; set; }

        [Required(ErrorMessage = "Please Enter a Country")]
        public string Country { get; set; }

        //public bool Anonymous { get; set; } //probably not need this. Only in WaterProject
    }
}