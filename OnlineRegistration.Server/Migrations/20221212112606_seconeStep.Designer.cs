// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineRegisteration.Server;

#nullable disable

namespace OnlineRegistration.Server.Migrations
{
    [DbContext(typeof(RegistrationDBContext))]
    [Migration("20221212112606_seconeStep")]
    partial class seconeStep
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OnlineRegisteration.Shared.Models.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("OnlineRegisteration.Shared.Models.Classroom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int>("CourseID")
                        .HasColumnType("int");

                    b.Property<int>("LecturerId")
                        .HasColumnType("int");

                    b.Property<int>("SlotName")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseID");

                    b.HasIndex("LecturerId");

                    b.ToTable("Classrooms");
                });

            modelBuilder.Entity("OnlineRegisteration.Shared.Models.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseId"));

                    b.Property<int?>("CourseDependenceId")
                        .HasColumnType("int");

                    b.Property<int>("CreditHours")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TheDepartmentId")
                        .HasColumnType("int");

                    b.HasKey("CourseId");

                    b.HasIndex("CourseDependenceId");

                    b.HasIndex("TheDepartmentId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("OnlineRegisteration.Shared.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("OnlineRegisteration.Shared.Models.Lecturer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Lecturers");
                });

            modelBuilder.Entity("OnlineRegisteration.Shared.Models.RegPeriod", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TheAdminId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("TheAdminId");

                    b.ToTable("RegPeriods");
                });

            modelBuilder.Entity("OnlineRegisteration.Shared.Models.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClassroomId")
                        .HasColumnType("int");

                    b.Property<int>("RegPeriodId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClassroomId");

                    b.HasIndex("RegPeriodId");

                    b.HasIndex("StudentId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("OnlineRegisteration.Shared.Models.Student", b =>
                {
                    b.Property<int>("RegId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RegId"));

                    b.Property<int>("CreditHours")
                        .HasColumnType("int");

                    b.Property<float>("GPA")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TheDepartmentId")
                        .HasColumnType("int");

                    b.HasKey("RegId");

                    b.HasIndex("TheDepartmentId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("OnlineRegisteration.Shared.Models.Classroom", b =>
                {
                    b.HasOne("OnlineRegisteration.Shared.Models.Course", "TheCourse")
                        .WithMany()
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineRegisteration.Shared.Models.Lecturer", "TheLecturer")
                        .WithMany("TheClassrooms")
                        .HasForeignKey("LecturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TheCourse");

                    b.Navigation("TheLecturer");
                });

            modelBuilder.Entity("OnlineRegisteration.Shared.Models.Course", b =>
                {
                    b.HasOne("OnlineRegisteration.Shared.Models.Course", "DependantCourse")
                        .WithMany()
                        .HasForeignKey("CourseDependenceId");

                    b.HasOne("OnlineRegisteration.Shared.Models.Department", "TheDepartment")
                        .WithMany("TheCourses")
                        .HasForeignKey("TheDepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DependantCourse");

                    b.Navigation("TheDepartment");
                });

            modelBuilder.Entity("OnlineRegisteration.Shared.Models.RegPeriod", b =>
                {
                    b.HasOne("OnlineRegisteration.Shared.Models.Admin", "TheAdmin")
                        .WithMany("TheRegPeriods")
                        .HasForeignKey("TheAdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TheAdmin");
                });

            modelBuilder.Entity("OnlineRegisteration.Shared.Models.Schedule", b =>
                {
                    b.HasOne("OnlineRegisteration.Shared.Models.Classroom", "TheClassroom")
                        .WithMany("TheSchedules")
                        .HasForeignKey("ClassroomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineRegisteration.Shared.Models.RegPeriod", "RegPeriod")
                        .WithMany("TheSchedules")
                        .HasForeignKey("RegPeriodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineRegisteration.Shared.Models.Student", "TheStudent")
                        .WithMany("TheSchedules")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RegPeriod");

                    b.Navigation("TheClassroom");

                    b.Navigation("TheStudent");
                });

            modelBuilder.Entity("OnlineRegisteration.Shared.Models.Student", b =>
                {
                    b.HasOne("OnlineRegisteration.Shared.Models.Department", "TheDepartment")
                        .WithMany("TheStudents")
                        .HasForeignKey("TheDepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TheDepartment");
                });

            modelBuilder.Entity("OnlineRegisteration.Shared.Models.Admin", b =>
                {
                    b.Navigation("TheRegPeriods");
                });

            modelBuilder.Entity("OnlineRegisteration.Shared.Models.Classroom", b =>
                {
                    b.Navigation("TheSchedules");
                });

            modelBuilder.Entity("OnlineRegisteration.Shared.Models.Department", b =>
                {
                    b.Navigation("TheCourses");

                    b.Navigation("TheStudents");
                });

            modelBuilder.Entity("OnlineRegisteration.Shared.Models.Lecturer", b =>
                {
                    b.Navigation("TheClassrooms");
                });

            modelBuilder.Entity("OnlineRegisteration.Shared.Models.RegPeriod", b =>
                {
                    b.Navigation("TheSchedules");
                });

            modelBuilder.Entity("OnlineRegisteration.Shared.Models.Student", b =>
                {
                    b.Navigation("TheSchedules");
                });
#pragma warning restore 612, 618
        }
    }
}
