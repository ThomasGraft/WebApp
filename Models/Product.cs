using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Product
    {
        public long ProductId { get; set; }
        public string Name { get; set; }

        // (No one-to-one mapping C# - SQL numeric type) Attribute to tell EF which SQL datatype to use in database 
        // 8 digits with 2 following decimal point
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }
        public long CategoryId { get; set; }
        public Category Category { get; set; }
        public long SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}
