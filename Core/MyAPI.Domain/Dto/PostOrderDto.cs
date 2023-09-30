using MyAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Domain.Dto
{
    public class PostOrderDto
    {
        public string Description { get; set; }
        public string Adress { get; set; }
        public Customer? Customers { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
