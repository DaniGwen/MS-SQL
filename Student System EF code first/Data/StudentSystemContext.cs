
namespace P01_StudentSystem.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using P01_StudentSystem.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;

    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        {

        }

        public StudentSystemContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Homework> Homeworks { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Config.connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasData(new Student { StudentId = 1, PhoneNumber = "088433243", Name = "Pesho" },
                         new Student { StudentId = 2, PhoneNumber = "0883434323", Name = "Gosho" },
                         new Student { StudentId = 3, PhoneNumber = "088433243", Name = "Sasho" },
                         new Student { StudentId = 4, PhoneNumber = "087654323", Name = "Nedko" },
                         new Student { StudentId = 5, PhoneNumber = "088412344", Name = "Sashka" },
                         new Student { StudentId = 6, PhoneNumber = "088340023", Name = "Penka" },
                         new Student { StudentId = 7, PhoneNumber = "088433200", Name = "Ivanka" },
                         new Student { StudentId = 8, PhoneNumber = "088114323", Name = "Munio" },
                         new Student { StudentId = 9, PhoneNumber = "08811003", Name = "Angel" },
                         new Student { StudentId = 10, PhoneNumber = "080033432", Name = "Mitko" }
                         );

            modelBuilder.Entity<Course>().
                HasData(new Course { CourseId = 1, Name = "Biology", Price = 560 },
                        new Course { CourseId = 2, Name = "Astronomy", Price = 400 },
                        new Course { CourseId = 3, Name = "History", Price = 360 },
                        new Course { CourseId = 4, Name = "Geography", Price = 453, },
                        new Course { CourseId = 5, Name = "Archeology", Price = 250 },
                        new Course { CourseId = 6, Name = "Chemistry", Price = 650 });


            OnModelCreatingStudent(modelBuilder);

            OnModelCreatingCourse(modelBuilder);

            OnModelCreatingHomework(modelBuilder);

            OnModelCreatingStudent(modelBuilder);

            OnModelCreatingStudentCourse(modelBuilder);

            OnModelCreatingResources(modelBuilder);
        }

        private void OnModelCreatingResources(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Resource>()
                .HasKey(r => r.ResourceId);

            modelBuilder.Entity<Resource>()
           .Property(r => r.Name)
           .HasMaxLength(50)
           .IsUnicode();

            modelBuilder.Entity<Resource>()
           .Property(r => r.Url)
           .IsUnicode(false);

            modelBuilder.Entity<Resource>()
           .HasOne(r => r.Course)
           .WithMany(c => c.Resources);
        }

        private void OnModelCreatingStudentCourse(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.HasKey(s => new { s.StudentId, s.CourseId });

                entity.HasOne(studentCourse => studentCourse.Student)
                .WithMany(student => student.CourseEnrollments)
                .HasForeignKey(sc => sc.StudentId);

                entity.HasOne(sc => sc.Course)
                .WithMany(c => c.StudentsEnrolled)
                .HasForeignKey(sc => sc.CourseId);
            });
        }

        private void OnModelCreatingHomework(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Homework>()
                .HasKey(h => h.HomeworkId);

            modelBuilder.Entity<Homework>()
                .Property(h => h.Content)
                .IsUnicode(false);

            modelBuilder.Entity<Homework>()
                .HasOne(h => h.Student)
                .WithMany(s => s.HomeworkSubmissions);

            modelBuilder.Entity<Homework>()
                .HasOne(h => h.Course)
                .WithMany(c => c.HomeworkSubmissions);

        }

        private void OnModelCreatingCourse(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasKey(c => c.CourseId);

            modelBuilder.Entity<Course>()
                .Property(c => c.Name)
                .HasMaxLength(80)
                .IsUnicode()
                .IsRequired();

            modelBuilder.Entity<Course>()
                .Property(c => c.StartDate)
                .HasDefaultValueSql("GETDATE()");
        }

        private void OnModelCreatingStudent(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                 .HasKey(s => s.StudentId);

            modelBuilder.Entity<Student>()
                .Property(s => s.Name)
                .HasMaxLength(100)
                .IsUnicode()
                .IsRequired();

            modelBuilder.Entity<Student>()
                .Property(s => s.PhoneNumber)
                .HasColumnType("CHAR(10)");

            modelBuilder.Entity<Student>()
                .Property(s => s.RegisteredOn)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Student>()
                .HasMany(s => s.HomeworkSubmissions)
                .WithOne(h => h.Student);

            modelBuilder.Entity<Student>()
               .HasMany(s => s.CourseEnrollments)
               .WithOne(c => c.Student);
        }
    }
}
