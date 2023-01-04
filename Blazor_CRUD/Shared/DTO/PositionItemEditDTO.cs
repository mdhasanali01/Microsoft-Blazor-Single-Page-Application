using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor_CRUD.Shared.DTO
{
    public class PositionItemEditDTO
    {
        [Key]
        
        public int PositionId { get; set; }
        [Required]
        public int EmployeeId { get; set; }
    }
}
