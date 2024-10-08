﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TManagement.Entities;

#nullable disable

namespace TManagement.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240909102212_SeedUsersAndGroups")]
    partial class SeedUsersAndGroups
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TManagement.Entities.AppGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Groups");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "ADMINS"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Secretary"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Reports"
                        });
                });

            modelBuilder.Entity("TManagement.Entities.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("CityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CurrentStatus")
                        .HasColumnType("int");

                    b.Property<Guid>("EducationLevelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("EducationLevelId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CityId = new Guid("cfe43cb8-7b8d-4955-bca1-491971508a77"),
                            CurrentStatus = 1,
                            EducationLevelId = new Guid("cfe43cb8-7b8d-4955-bca1-491971508a79"),
                            Email = "Atallah.esaied@gmail.com",
                            FullName = "System admin",
                            PasswordHash = "8F042D4A735B2AD0EB0474F2253F14D47CF81C37964BC64495EAFD83D4ECC8EB2F0643F987CA8CA1B527F130588DDE42A09B6DC02FCA1F764C299FFBF06B47F4",
                            PasswordSalt = "AC3058FE0771233877CD439096950A7077EB79DCB32010B615AB0BD5EFF4CED02F50908EF7B48EE1B1412955AB1E95C29C2A57666D40F301690F725666DEF9EE"
                        });
                });

            modelBuilder.Entity("TManagement.Entities.AppUserGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("UserGroups");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a7352666-5593-4cff-9443-6db0a1b9dcbc"),
                            GroupId = 1,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("TManagement.Entities.Lookup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("FatherLookupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FatherLookupId");

                    b.ToTable("Lookups");

                    b.HasData(
                        new
                        {
                            Id = new Guid("cfe43cb8-7b8d-4955-bca1-491971508a75"),
                            Name = "Palestine",
                            Type = 1
                        },
                        new
                        {
                            Id = new Guid("cfe43cb8-7b8d-4955-bca1-491971508a76"),
                            Name = "Jordan",
                            Type = 1
                        },
                        new
                        {
                            Id = new Guid("cfe43cb8-7b8d-4955-bca1-491971508a77"),
                            FatherLookupId = new Guid("cfe43cb8-7b8d-4955-bca1-491971508a75"),
                            Name = "Jerusalem",
                            Type = 5
                        },
                        new
                        {
                            Id = new Guid("46baabeb-030b-49f1-b6e5-cd02e313956e"),
                            FatherLookupId = new Guid("cfe43cb8-7b8d-4955-bca1-491971508a76"),
                            Name = "Amman",
                            Type = 5
                        },
                        new
                        {
                            Id = new Guid("8bf52a09-678c-470e-a8e9-ad95fe2c1d7f"),
                            FatherLookupId = new Guid("cfe43cb8-7b8d-4955-bca1-491971508a75"),
                            Name = "Ramallah",
                            Type = 5
                        },
                        new
                        {
                            Id = new Guid("e17c10fd-e4c0-47cc-a3bc-c5ef3e229b4a"),
                            Name = "Elemantary",
                            Type = 21
                        },
                        new
                        {
                            Id = new Guid("29be72b1-3a33-4188-9fc7-0579adac9ffa"),
                            Name = "Tawjihi",
                            Type = 21
                        },
                        new
                        {
                            Id = new Guid("cfe43cb8-7b8d-4955-bca1-491971508a79"),
                            Name = "BA/BS",
                            Type = 21
                        },
                        new
                        {
                            Id = new Guid("f3e1ba09-f161-4f62-824a-8db683276850"),
                            Name = "Master and above",
                            Type = 21
                        });
                });

            modelBuilder.Entity("TManagement.Entities.AppUser", b =>
                {
                    b.HasOne("TManagement.Entities.Lookup", "City")
                        .WithMany("CityUsers")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TManagement.Entities.Lookup", "EducationLevel")
                        .WithMany("EducationLevelUsers")
                        .HasForeignKey("EducationLevelId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("EducationLevel");
                });

            modelBuilder.Entity("TManagement.Entities.AppUserGroup", b =>
                {
                    b.HasOne("TManagement.Entities.AppGroup", "Group")
                        .WithMany("Users")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TManagement.Entities.AppUser", "User")
                        .WithMany("Groups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TManagement.Entities.Lookup", b =>
                {
                    b.HasOne("TManagement.Entities.Lookup", "FatherLookup")
                        .WithMany()
                        .HasForeignKey("FatherLookupId");

                    b.Navigation("FatherLookup");
                });

            modelBuilder.Entity("TManagement.Entities.AppGroup", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("TManagement.Entities.AppUser", b =>
                {
                    b.Navigation("Groups");
                });

            modelBuilder.Entity("TManagement.Entities.Lookup", b =>
                {
                    b.Navigation("CityUsers");

                    b.Navigation("EducationLevelUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
