﻿// <auto-generated />
using System;
using AttendanceCapture.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AttendanceCapture.Migrations
{
    [DbContext(typeof(UniversityContext))]
    partial class UniversityContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AttendanceCapture.Models.ApplicationRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<DateTimeOffset>("TimeCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("TimeUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("AttendanceCapture.Models.Attendance", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long?>("Course")
                        .HasColumnType("bigint");

                    b.Property<long>("LecturerID")
                        .HasColumnType("bigint");

                    b.Property<string>("Remarks")
                        .HasColumnType("text");

                    b.Property<long?>("Student")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("TimeCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("TimeUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Attendances");
                });

            modelBuilder.Entity("AttendanceCapture.Models.Class", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("ClassNumber")
                        .HasColumnType("text");

                    b.Property<long>("LecturerID")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("StudentIds")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("TimeCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("TimeUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("AttendanceCapture.Models.Course", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long?>("DepartmentID")
                        .HasColumnType("bigint");

                    b.Property<long?>("LectureriD")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("TimeCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("TimeUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("AttendanceCapture.Models.DBSessions", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTimeOffset>("ExpiresAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("SessionKey")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("TimeCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("TimeUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("UserID")
                        .HasColumnType("bigint");

                    b.Property<int>("UserType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("UserSessions");
                });

            modelBuilder.Entity("AttendanceCapture.Models.Department", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long?>("FacultyID")
                        .HasColumnType("bigint");

                    b.Property<int?>("FacultyId")
                        .HasColumnType("integer");

                    b.Property<string>("Lecturers")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("TimeCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("TimeUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("AttendanceCapture.Models.Faculty", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Departments")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("TimeCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("TimeUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("AttendanceCapture.Models.LecturTutor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Courses")
                        .HasColumnType("text");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("TimeCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("TimeUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Lecturers");
                });

            modelBuilder.Entity("AttendanceCapture.Models.LogActivity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("TimeCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("TimeUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("LogActivities");
                });

            modelBuilder.Entity("AttendanceCapture.Models.Student", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Attendances")
                        .HasColumnType("text");

                    b.Property<int?>("ClassId")
                        .HasColumnType("integer");

                    b.Property<string>("MatricNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Photo")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("TimeCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("TimeUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("AttendanceCapture.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<int>("AccountStatus")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("TimeCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("TimeUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<int>("UserType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            AccessFailedCount = 0,
                            AccountStatus = 1,
                            ConcurrencyStamp = "6eaebebe-4432-4615-93af-3d2c67958f8e",
                            Email = "technology@facifier.com",
                            EmailConfirmed = true,
                            FirstName = "FInalYear",
                            LastName = "Administrator",
                            LockoutEnabled = false,
                            NormalizedEmail = "TECHNOLOGY@FACIFIER.COM",
                            NormalizedUserName = "TECHNOLOGY@FACIFIER.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEB+h1loB2rnS5q6htME46g/yBEced4NFe1ILh0q17CqT8gug4sZuD9xsQnLT1KOoNg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "E5612E4D361F914EABB4056F90624177",
                            TimeCreated = new DateTimeOffset(new DateTime(2024, 3, 10, 18, 55, 16, 332, DateTimeKind.Unspecified).AddTicks(9161), new TimeSpan(0, 0, 0, 0, 0)),
                            TimeUpdated = new DateTimeOffset(new DateTime(2024, 3, 10, 18, 55, 16, 332, DateTimeKind.Unspecified).AddTicks(9167), new TimeSpan(0, 0, 0, 0, 0)),
                            TwoFactorEnabled = false,
                            UserName = "FACIFIER",
                            UserType = 1
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<long>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<long>", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<long>", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<long>", b =>
                {
                    b.HasOne("AttendanceCapture.Models.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<long>", b =>
                {
                    b.HasOne("AttendanceCapture.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<long>", b =>
                {
                    b.HasOne("AttendanceCapture.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<long>", b =>
                {
                    b.HasOne("AttendanceCapture.Models.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AttendanceCapture.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<long>", b =>
                {
                    b.HasOne("AttendanceCapture.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
