using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DemoCompany.DAL.Models;

public partial class DemoCompanyContext : DbContext
{
    public DemoCompanyContext()
    {
    }

    public DemoCompanyContext(DbContextOptions<DemoCompanyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }


    /*
    + Để mà thiết lập cầu nối giữa Sql Server và .NET thì mình phải qua DbContext
    + OnConfiguring này là nơi cấu hình cầu nối DbContext
    + Có 2 cách để thiết lập cầu nối:
        - Thông qua hàm OnConfiguring
        - Thông qua cơ chế Dependency Injection bên file program
     */

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    // configuration mình sẽ tìm cấu hình của file app settings xong nạp vào trong cái biến configuration
    //    IConfiguration configuration = new ConfigurationBuilder() // new 1 object Configuration Builder
    //                                                              //SetBasePath: tác dụng là để nhắc cho configuration biết là tìm file app settings ở đâu
    //                                    .SetBasePath(Directory.GetCurrentDirectory())
    //                                    // tìm file app settings và add thật sự
    //                                    // optional nếu để true có nghĩa là không tìm thấy file thì sẽ không crash app -> bỏ qua luôn
    //                                    //  reloadOnChange: tự reload nội dung file lại mà không cần restart app nếu có ai sửa bên trong file
    //                                    .AddJsonFile("appsettings.json", true, true)
    //                                    .Build();

    //    // vào bên trong configuration đã có app settings
    //    // OUTPUT: connection string sẽ là "Server=localhost;Database=DemoCompany;User Id=sa;Password=12345;TrustServerCertificate=True"
    //    var connectionString = configuration.GetConnectionString("DefaultConnection");

    //    // mang cái chuỗi gọi sql server
    //    optionsBuilder.UseSqlServer(connectionString);
    //}



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BED1C6DB1BD");

            entity.ToTable("Department");

            entity.Property(e => e.Budget).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DepartmentName).HasMaxLength(100);
            entity.Property(e => e.Location).HasMaxLength(100);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04F1116A6C583");

            entity.ToTable("Employee");

            entity.HasIndex(e => e.Email, "UQ__Employee__A9D1053487EFF19D").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Position).HasMaxLength(100);
            entity.Property(e => e.Salary).HasColumnType("decimal(12, 2)");

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__Employee__Depart__286302EC");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
