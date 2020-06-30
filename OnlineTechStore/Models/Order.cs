using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace OnlineTechStore.Models
{
    public class Order
    {
        public int ID { get; set; }
        public int Amount { get; set; }
        public string ShippingAddress { get; set; }
        public string OrderAddress { get; set; }
        public string OrderEmail { get; set; }

        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public string CustomerId{ get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }

        public virtual ApplicationUser Customer { get; set; }
    }
}