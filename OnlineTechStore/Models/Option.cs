using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTechStore.Models
{
    public class Option
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }
}