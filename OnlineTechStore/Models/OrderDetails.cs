using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineTechStore.Models
{
    public class OrderDetails
    {
        public int ID { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public Nullable<int> OrderId { get; set; }
        public virtual Order Order { get; set; } 
        public virtual Product Product { get; set; }
    }
}