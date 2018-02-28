namespace PruebaCodeFirstNetcore.Models
{
    using Microsoft.EntityFrameworkCore;

    public class Context : DbContext
    {
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Task> Task { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder )
        {
            const string conn = @"Data Source=PC-STARKILLER\MSSQLSERVER2012;Initial Catalog=ExamenMvc;User ID=sa;Password=A@141516182235";
            optionBuilder.UseSqlServer(conn);
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
                entity.ToTable("Tasks");
                entity.Property(e => e.TaskId).HasColumnName("TaskId").HasColumnType("numeric").ValueGeneratedOnAdd();
                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeId").HasColumnType("numeric");
                entity.Property(e => e.TaskName).HasColumnName("TaskName").HasColumnType("varchar(200)");
            });
        }
    }
}
