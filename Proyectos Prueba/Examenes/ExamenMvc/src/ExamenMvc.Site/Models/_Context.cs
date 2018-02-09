namespace ExamenMvc.Site.Models
{
    using Microsoft.EntityFrameworkCore;
    
    namespace ExamenMVC.Models
    {
        public class _Context : DbContext
        {
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                var conn = @"Server=.\SQLExpress; uid=sa; pwd=password$$1; database=ExamenMVC;";
                optionsBuilder.UseSqlServer(conn);
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Employee>(entity => {
                    entity.ToTable("Employees");
                    entity.Property(e => e.EmployeeId).HasColumnName("EmployeeId").HasColumnType("numeric").ValueGeneratedOnAdd();
                    entity.Property(e => e.EmployeeNumber).HasColumnName("EmployeeNumber").HasColumnType("varchar(8)");
                    entity.Property(e => e.EmployeeName).HasColumnName("EmployeeName").HasColumnType("varchar(200)");
                });
                modelBuilder.Entity<Task>(entity => {
                    entity.ToTable("Taskss");
                    entity.Property(e => e.TaskId).HasColumnName("TaskId").HasColumnType("numeric").ValueGeneratedOnAdd();
                    entity.Property(e => e.EmployeeId).HasColumnName("EmployeeId").HasColumnType("numeric");
                    entity.Property(e => e.TaskName).HasColumnName("TaskName").HasColumnType("varchar(200)");
                });
            }

            public virtual DbSet<Employee> Employees { get; set; }
            public virtual DbSet<Task> Tasks { get; set; }
        }
    }

}
