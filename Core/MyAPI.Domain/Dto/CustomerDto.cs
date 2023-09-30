using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Domain.Dto
{
    public class CustomerDto
    {
        public string Name { get; set; }
        [EmailAddress]
        public string Mail { get; set; }

        [Phone]
        [RegularExpression(@"^5[0-9]{9}$", ErrorMessage = "Geçerli bir Türkiye telefon numarası giriniz.")]
        public string Phone { get; set; }
    }
}
