using Microsoft.EntityFrameworkCore;
using webAPI1.Models;

namespace webAPI1.Data
{
    public class AppDbContext : DbContext
    {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {
            }

            public DbSet<Students> Students { get; set; }
            public DbSet<Courses> Courses { get; set; }
            public DbSet<StudentCourses> StudentCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
            {
            builder.Entity<StudentCourses>().HasKey(h => new { h.StudentId, h.CoursesId });
            builder.Entity<StudentCourses>().HasOne(h => h.Courses).WithMany(h => h.StudentCourses);
            builder.Entity<StudentCourses>().HasOne(h => h.Student).WithMany(h => h.StudentCourses);

            new DbInitializer(builder).Seed();
        }
        public class DbInitializer
        {
            private readonly ModelBuilder _builder;
            public DbInitializer(ModelBuilder builder)
            {
                this._builder = builder;
            }

            public void Seed()
            {
                _builder.Entity<Students>(s =>
                {
                    s.HasData(new Students
                    {
                        StudentId = new Guid("150c81c6-2458-466e-907a-2df11325e2b8"),
                        Name = "Trần Kim Long"
                    });
                    s.HasData(new Students
                    {
                        StudentId = new Guid("90d10994-3bdd-4ca2-a178-6a35fd653c59"),
                        Name = "Đức Trọng"
                    });
                });
                _builder.Entity<Courses>(c =>
                { c.HasData(new Courses
                    {
                        CourseId = new Guid("9250d994-2558-4573-8465-417248667051"),
                        CourseName = "Toán Học",
                        Description = "Hỗ Trợ toán học nâng cao"
                    });
                    c.HasData(new Courses
                    {
                        CourseId = new Guid("88738493-3a85-4443-8f6a-313453432192"),
                        CourseName = "Tiếng Anh",
                        Description = "Thông thạo viết nói nghe tiếng nước ngoài",
                    });
                });
                _builder.Entity<Models.StudentCourses>(sc =>
                {
                    sc.HasData(new Models.StudentCourses
                    {
                        StudentId = new Guid("150c81c6-2458-466e-907a-2df11325e2b8"),
                        CoursesId = new Guid("88738493-3a85-4443-8f6a-313453432192")
                    });
                    sc.HasData(new Models.StudentCourses
                    {
                        StudentId = new Guid("90d10994-3bdd-4ca2-a178-6a35fd653c59"),
                        CoursesId = new Guid("9250d994-2558-4573-8465-417248667051")
                    });
                });
            }

        }
    }
}

