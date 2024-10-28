﻿// <auto-generated />
using System;
using BusinessObject.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BusinessObject.Migrations
{
    [DbContext(typeof(AuthenticationContext))]
    partial class AuthenticationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BusinessObject.Models.Account", b =>
                {
                    b.Property<int>("AcountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Account ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AcountId"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Email");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Password");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("Role ID");

                    b.HasKey("AcountId");

                    b.HasIndex("RoleId");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("BusinessObject.Models.Major", b =>
                {
                    b.Property<int>("MajorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Major ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MajorId"));

                    b.Property<string>("MajorName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Major Name");

                    b.HasKey("MajorId");

                    b.ToTable("Major");
                });

            modelBuilder.Entity("BusinessObject.Models.Profile", b =>
                {
                    b.Property<Guid>("ProfileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Profile ID");

                    b.Property<int?>("AccountId")
                        .HasColumnType("int")
                        .HasColumnName("Account ID");

                    b.Property<DateOnly>("Birthday")
                        .HasColumnType("date")
                        .HasColumnName("Birthday");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Code");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("Created At");

                    b.Property<string>("FirtName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Firt Name");

                    b.Property<int>("GenderId")
                        .HasColumnType("int")
                        .HasColumnName("Gender ID");

                    b.Property<DateTime>("LastModifiedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("Last Modified At");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Last Name");

                    b.Property<int>("MajorId")
                        .HasColumnType("int")
                        .HasColumnName("Major ID");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Middel Name");

                    b.HasKey("ProfileId");

                    b.HasIndex("AccountId")
                        .IsUnique()
                        .HasFilter("[Account ID] IS NOT NULL");

                    b.HasIndex("MajorId")
                        .IsUnique();

                    b.ToTable("Profile");
                });

            modelBuilder.Entity("BusinessObject.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Role ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Role Name");

                    b.HasKey("RoleId");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("BusinessObject.Models.Account", b =>
                {
                    b.HasOne("BusinessObject.Models.Role", "Role")
                        .WithMany("Accounts")
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("BusinessObject.Models.Profile", b =>
                {
                    b.HasOne("BusinessObject.Models.Account", "Account")
                        .WithOne("Profile")
                        .HasForeignKey("BusinessObject.Models.Profile", "AccountId");

                    b.HasOne("BusinessObject.Models.Major", "Major")
                        .WithOne("Profile")
                        .HasForeignKey("BusinessObject.Models.Profile", "MajorId");

                    b.Navigation("Account");

                    b.Navigation("Major");
                });

            modelBuilder.Entity("BusinessObject.Models.Account", b =>
                {
                    b.Navigation("Profile");
                });

            modelBuilder.Entity("BusinessObject.Models.Major", b =>
                {
                    b.Navigation("Profile");
                });

            modelBuilder.Entity("BusinessObject.Models.Role", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
