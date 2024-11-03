﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScheduleBusinessObject.DataContext;

#nullable disable

namespace ScheduleBusinessObject.Migrations
{
    [DbContext(typeof(ScheduleContext))]
    [Migration("20241103161532__updateObjectInScheduleObject")]
    partial class _updateObjectInScheduleObject
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ScheduleBusinessObject.Models.Class", b =>
                {
                    b.Property<int>("ClassId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Class Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClassId"));

                    b.Property<string>("ClassName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Class Name");

                    b.HasKey("ClassId");

                    b.ToTable("Classes", "dbo");
                });

            modelBuilder.Entity("ScheduleBusinessObject.Models.Room", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Room ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoomId"));

                    b.Property<string>("RoomName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Room Name");

                    b.Property<bool>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("Room Status");

                    b.HasKey("RoomId");

                    b.ToTable("Rooms", "dbo");
                });

            modelBuilder.Entity("ScheduleBusinessObject.Models.Schedule", b =>
                {
                    b.Property<int>("ScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Schedule Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ScheduleId"));

                    b.Property<int>("ClassId")
                        .HasColumnType("int");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<string>("LectureId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("LecturerId");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<int>("TimeSlotId")
                        .HasColumnType("int");

                    b.HasKey("ScheduleId");

                    b.HasIndex("ClassId");

                    b.HasIndex("RoomId");

                    b.HasIndex("SubjectId");

                    b.HasIndex("TimeSlotId");

                    b.ToTable("Schedules", "dbo");
                });

            modelBuilder.Entity("ScheduleBusinessObject.Models.StudentInClass", b =>
                {
                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ClassId")
                        .HasColumnType("int");

                    b.HasKey("StudentId", "ClassId");

                    b.HasIndex("ClassId");

                    b.ToTable("StudentInClass");
                });

            modelBuilder.Entity("ScheduleBusinessObject.Models.Subject", b =>
                {
                    b.Property<int>("SubjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Subject ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubjectId"));

                    b.Property<int>("NumberOfSlot")
                        .HasColumnType("int");

                    b.Property<string>("SubjectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Subject Name");

                    b.HasKey("SubjectId");

                    b.ToTable("subjects", "dbo");
                });

            modelBuilder.Entity("ScheduleBusinessObject.Models.TimeSlot", b =>
                {
                    b.Property<int>("SlotId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("timeSlot ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SlotId"));

                    b.Property<bool>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("Status");

                    b.Property<TimeOnly>("TimeEnd")
                        .HasColumnType("time")
                        .HasColumnName("Time End");

                    b.Property<TimeOnly>("TimeStart")
                        .HasColumnType("time")
                        .HasColumnName("Time Start");

                    b.HasKey("SlotId");

                    b.ToTable("TimeSlots", "dbo");
                });

            modelBuilder.Entity("ScheduleBusinessObject.Models.Schedule", b =>
                {
                    b.HasOne("ScheduleBusinessObject.Models.Class", "Class")
                        .WithMany("Schedules")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ScheduleBusinessObject.Models.Room", "Room")
                        .WithMany("Schedules")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ScheduleBusinessObject.Models.Subject", "Subject")
                        .WithMany("Schedules")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ScheduleBusinessObject.Models.TimeSlot", "TimeSlot")
                        .WithMany("Schedules")
                        .HasForeignKey("TimeSlotId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Class");

                    b.Navigation("Room");

                    b.Navigation("Subject");

                    b.Navigation("TimeSlot");
                });

            modelBuilder.Entity("ScheduleBusinessObject.Models.StudentInClass", b =>
                {
                    b.HasOne("ScheduleBusinessObject.Models.Class", "Class")
                        .WithMany("StudentsInClass")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");
                });

            modelBuilder.Entity("ScheduleBusinessObject.Models.Class", b =>
                {
                    b.Navigation("Schedules");

                    b.Navigation("StudentsInClass");
                });

            modelBuilder.Entity("ScheduleBusinessObject.Models.Room", b =>
                {
                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("ScheduleBusinessObject.Models.Subject", b =>
                {
                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("ScheduleBusinessObject.Models.TimeSlot", b =>
                {
                    b.Navigation("Schedules");
                });
#pragma warning restore 612, 618
        }
    }
}