using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Blazor_CRUD.Shared.DTO
{
    public enum Type { Financial = 1, IT, Ecommerce, Corporate }
    public class CompanyDTO
    {
        [Key]
        public int CompanyId { get; set; }
        [Required, StringLength(50), Display(Name = "Company Name")]
        public string CompanyName { get; set; } = default!;
        [Required, EnumDataType(typeof(Type))]
        public Type Type { get; set; }
        public string Address { get; set; } = default!;
        [Required, StringLength(50)]
        public string Email { get; set; } = default!;
    }
}
