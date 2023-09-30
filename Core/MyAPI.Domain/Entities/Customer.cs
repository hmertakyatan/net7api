using MyAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Mail { get; set; }

        [Phone]
        [RegularExpression(@"^5[0-9]{9}$", ErrorMessage = "Geçerli bir Türkiye telefon numarası giriniz.")]
        public string Phone { get; set; }
        public ICollection<Order>? Orders;

    }
}
