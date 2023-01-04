using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Blazor_CRUD.Shared.DTO
{
    public class PositionViewDTO
    {
        [Key]
        public int PositionId { get; set; }
        [Required, StringLength(50)]
        public string PositionName { get; set; } = default!;      
              
    }
}
