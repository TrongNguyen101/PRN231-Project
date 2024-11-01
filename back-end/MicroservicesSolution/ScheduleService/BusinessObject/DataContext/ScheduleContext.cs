using ScheduleBusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ScheduleBusinessObject.DataContext
{
    public class ScheduleContext : DbContext
    {
        public ScheduleContext() { }
        public ScheduleContext(DbContextOptions<ScheduleContext> options) : base(options) { }

        public DbSet<Class> Classes { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("BangConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Create data table
            modelBuilder.Entity<Class>(entity => {
                entity.ToTable("Classes", "dbo");
                entity.HasKey(clas => clas.ClassId);

                entity.Property(clas => clas.ClassId)
                .IsRequired(true)
                .HasColumnName("Class Id")
                .ValueGeneratedOnAdd();

                entity.Property(clas => clas.ClassName)
                .IsRequired(true)
                .HasColumnName("Class Name");

                entity.HasMany(clas => clas.Schedules) // A class has many schedule
                .WithOne(schedule => schedule.Class)   //Each schedule has one class which depend on the number of students, with one teacher, within a specific period of time and held in one room
                .HasForeignKey(schedule => schedule.ClassId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.StudentsInClass)
                  .WithOne(e => e.Class)
                  .HasForeignKey(e => e.ClassId)
                  .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Rooms", "dbo");
                entity.HasKey(room => room.RoomId);

                entity.Property(room => room.RoomId)
                .IsRequired(true)
                .HasColumnName("Room ID")
                .ValueGeneratedOnAdd();

                entity.Property(room => room.RoomName)
                .IsRequired(true)
                .HasColumnName("Room Name");

                entity.Property(room => room.Status)
                .IsRequired(true)
                .HasColumnName("Room Status");
            });

            modelBuilder.Entity<StudentInClass>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.ClassId });

                // Composite key configuration
                entity.HasOne(e => e.Class)
                      .WithMany(c => c.StudentsInClass)
                      .HasForeignKey(e => e.ClassId);

                // Since StudentId is from another service, configure it as a regular property
                entity.Property(e => e.StudentId)
                      .IsRequired();
            });

            modelBuilder.Entity<Schedule>(entity => {
                entity.ToTable("Schedules", "dbo");
                entity.HasKey(schedule => schedule.ScheduleId);

                entity.Property(s => s.Date);

                entity.Property(schedule => schedule.ScheduleId)
                .IsRequired(true)
                .HasColumnName("Schedule Id")
                .ValueGeneratedOnAdd();

                entity.Property(schedule => schedule.LectureId)
                .IsRequired(true)
                .HasColumnName("LecturerId");

                entity.HasOne(schedule => schedule.Room)     //A schedule is held in one room which depend on number of students studying with one lecturer within a specific period of time, "
                .WithMany(room => room.Schedules)            //A room has many schedules
                .HasForeignKey(schedule => schedule.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(schedule => schedule.Subject) //A schedule teaches one subject
                .WithMany(subject => subject.Schedules)     //A subject is taught at many schedules
                .HasForeignKey(schedule => schedule.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);

               entity.HasOne(schedule => schedule.TimeSlot)   // A schedule is held within a specific period of time
                .WithMany(timeSlot => timeSlot.Schedules)     // A specific of time has many schedules. it depends on the number of room, number of leacturer...
                .HasForeignKey(schedule => schedule.TimeSlotId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("subjects", "dbo");
                entity.HasKey(subject => subject.SubjectId);

                entity.Property(subject => subject.SubjectId)
                .IsRequired(true)
                .HasColumnName("Subject ID")
                .ValueGeneratedOnAdd();

                entity.Property(subject => subject.SubjectName)
                .IsRequired(true)
                .HasColumnName("Subject Name");
            });

            modelBuilder.Entity<TimeSlot>(entity =>
            {
                entity.ToTable("TimeSlots", "dbo");
                entity.HasKey(timeSlot => timeSlot.SlotId);

                entity.Property(timeSlot => timeSlot.SlotId)
                .IsRequired(true)
                .HasColumnName("timeSlot ID")
                .ValueGeneratedOnAdd();

                entity.Property(timeSlot => timeSlot.TimeStart)
                .IsRequired(true)
                .HasColumnName("Time Start");

                entity.Property(timeSlot => timeSlot.TimeEnd)
                .IsRequired(true)
                .HasColumnName("Time End");

                entity.Property(timeSlot => timeSlot.Status)
                .IsRequired(true)
                .HasColumnName("Status");
            });
        }
    }
}
