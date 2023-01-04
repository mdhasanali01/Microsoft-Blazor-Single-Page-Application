using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor_CRUD.Shared.DTO
{
    public class PositionEditDTO
    {
        [Key]
        public int PositionId { get; set; }

        public string PositionName { get; set; } = default!;

        public virtual ICollection<PositionItemEditDTO> PositionItemDTOs { get; set; } = new List<PositionItemEditDTO>();
    }
}
