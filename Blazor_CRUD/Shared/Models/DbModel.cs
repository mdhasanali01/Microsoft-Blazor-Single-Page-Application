using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Blazor_CRUD.Shared.Models
{
    public enum Type { Financial = 1, IT, Ecommerce, Corporate }
    public class Company
    {
        public int CompanyId { get; set; }
        [Required, StringLength(50), Display(Name = "Company Name")]
        public string CompanyName { get; set; } = default!;
        [Required, EnumDataType(typeof(Type))]
        public Type Type { get; set; }
        public string Address { get; set; } = default!;
        [Required, StringLength(50)]
        public string Email { get; set; } = default!;
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
    public class Employee
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
        public Company? Company { get; set; } = default!;
        public virtual ICollection<EmployeePosition> EmployeePositions { get; set; } = new List<EmployeePosition>();
    }
    public class Position
    {
        public int PositionId { get; set; }
        [Required, StringLength(50)]
        public string PositionName { get; set; } = default!;
        public virtual ICollection<EmployeePosition> EmployeePositions { get; set; } = new List<EmployeePosition>();
    }
    public class EmployeePosition
    {
        [ForeignKey("Position")]
        public int PositionId { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public virtual Position Position { get; set; } = default!;
        public virtual Employee Employee { get; set; } = default!;
    }
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options) { }
        public DbSet<Company> Companys { get; set; } = default!;
        public DbSet<Position> Positions { get; set; } = default!;
        public DbSet<EmployeePosition> EmployeePositions { get; set; } = default!;
        public DbSet<Employee> Employees { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeePosition>().HasKey(o => new { o.PositionId, o.EmployeeId });
        }
    }
}
