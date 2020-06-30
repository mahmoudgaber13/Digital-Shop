using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineTechStore.Models
{
    public class Product
    {
        public int ID { get; set; }
        public int Stock { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Weigth { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        //public string Category { get; set; }
        [Display(Name = "Product Category-Name")]
        public Nullable<int> CategoryID { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual ICollection<OrderDetails> OrderDetailes { get; set; }
        public virtual ICollection<Option> Options { get; set; }
        public virtual Category Category { get; set; }

    }
}