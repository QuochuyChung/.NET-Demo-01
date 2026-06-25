
using DemoCompany.BLL;
using DemoCompany.DAL;
using DemoCompany.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoCompany.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // DI Container: là nơi chứa các object như là dbcontext, swagger, services, repository, controller

            // Map những controller vào bên trong DI Container
            builder.Services.AddControllers();  

            // Endpoint Controller add để cho swagger có thể nhận diện được 
            builder.Services.AddEndpointsApiExplorer();

            // Add Swagger
            builder.Services.AddSwaggerGen();



            /*
              3 cách add vào DI Container
                + AddSingleton();
                Singleton: là object xài được cho toàn bộ app, và nó sẽ không bao giờ chết, nếu nó là Db thì sẽ không bao giờ đóng connection  
                + AddTransistent();
                Transistent: là object cứ cần là tạo cái mới, cũng là xài xong rồi bỏ
                + AddScoped():
                Scoped: là object tồn tại trong 1 vòng đời của request, request hết tương đương với bỏ luôn, rồi Garbage Collector sẽ tự dọn dẹp
              
             
             */
            builder.Services.AddDbContext<DemoCompanyContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<EmployeeRepository>();
            builder.Services.AddScoped<DepartmentRepository>();

            builder.Services.AddScoped<UnitOfWork>();

            builder.Services.AddScoped<EmployeeService>();
            builder.Services.AddScoped<DepartmentService>();

            builder.Services.AddScoped<DepartmentRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //AddScoped, AddDbContext();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
