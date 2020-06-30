using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTechStore.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public Cart (Product product,int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }
}