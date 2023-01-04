using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor_CRUD.Shared.DTO
{
    public class PositionItemDTO
    {
        [ForeignKey("Position")]
        public int PositionId { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
    }
}
