using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor_CRUD.Shared.DTO
{
   
    public class EmployeeEdit
    {
        public int EmployeeId { get; set; }
        [Required, StringLength(50)]

        public string EmployeeName { get; set; } = default!;
        public string Address { get; set; } = default!;
        [Required, Column(TypeName = "date"),
        Display(Name = "Join Date"),
            DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
            ApplyFormatInEditMode = true)]
        public DateTime JoinDate { get; set; }
        [Required, Column(TypeName = "money"), DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal Salary { get; set; }
        public bool IsContinue { get; set; }
        [Required, StringLength(150)]
        public string Picture { get; set; } = default!;
        [Required, ForeignKey("Company")]
        public int CompanyId { get; set; }
        public string? CompanyName { get; set; } = default!;

    }
}
