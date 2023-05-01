using EduConnect.Entities;
using EduConnect.Identity;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
           .HasOne(a => a.Address)
           .WithOne(b => b.Student)
           .HasForeignKey<Address>(b => b.StudentId);
            
            modelBuilder.Entity<Parent>()
           .HasOne(a => a.Address)
           .WithOne(b => b.Parent)
           .HasForeignKey<Address>(b => b.ParentId);
            
            modelBuilder.Entity<Tutor>()
           .HasOne(a => a.Address)
           .WithOne(b => b.Tutor)
           .HasForeignKey<Address>(b => b.TutorId);
        }
        public DbSet<Tutor> Tutors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }

    }
}
