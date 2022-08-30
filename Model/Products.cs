using API_Aplication.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_Aplication.Model
{
    public class Products
    {
        [Key]
        public string id { get; set; } = RandomKeyGenerator.GenerateGuId();
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

    }
}
