using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor_CRUD.Shared.DTO
{
    public class PositionItemViewDTO
    {
        public int PositionId { get; set; }
        [Required]
        public string EmployeeName { get; set; } = default!;
        [Required]
        public string Address { get; set; } = default!;
        [Required]
        public decimal Salary { get; set; }
        [Required]
        public bool IsContinue { get; set; }
    }
}
