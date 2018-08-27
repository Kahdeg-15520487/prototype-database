﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using prototype_database.Dal;

namespace prototype_database.Dal.Migrations
{
    [DbContext(typeof(UserDbContext))]
    partial class UserDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("prototype_database.Dal.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<Guid>("OrganizationId");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Groups");

                    b.HasData(
                        new { Id = new Guid("f75ff792-e7af-4653-b400-06598caccb64"), Name = "Technical", OrganizationId = new Guid("9c393d2a-0f35-40bf-8a9c-59733c581f23") },
                        new { Id = new Guid("f81948f0-6341-4837-b272-b7cabf245ddd"), Name = "HR", OrganizationId = new Guid("9c393d2a-0f35-40bf-8a9c-59733c581f23") },
                        new { Id = new Guid("16209a73-9842-42f6-abce-50097e7dfbdc"), Name = "SoftwareEngineer", OrganizationId = new Guid("35c125a5-c693-4eee-a6c9-155dd0b8f716") },
                        new { Id = new Guid("50010527-2c8a-4f9a-9f7d-11c5559f82f1"), Name = "ComputerEngineer", OrganizationId = new Guid("35c125a5-c693-4eee-a6c9-155dd0b8f716") }
                    );
                });

            modelBuilder.Entity("prototype_database.Dal.Organization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Organizations");

                    b.HasData(
                        new { Id = new Guid("9c393d2a-0f35-40bf-8a9c-59733c581f23"), Name = "Rosen" },
                        new { Id = new Guid("35c125a5-c693-4eee-a6c9-155dd0b8f716"), Name = "UIT" }
                    );
                });

            modelBuilder.Entity("prototype_database.Dal.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new { Id = new Guid("d734e870-882d-44a2-9578-f8b08c5a37ce"), Name = "Technical Lead" },
                        new { Id = new Guid("2dbf08ad-b4b9-4e88-b253-6b969d879b68"), Name = "HR Lead" },
                        new { Id = new Guid("4b388280-17f5-43c6-aea7-2de535bb3003"), Name = "Engineer" }
                    );
                });

            modelBuilder.Entity("prototype_database.Dal.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Mobile");

                    b.Property<Guid>("OrganizationId");

                    b.Property<string>("Phone");

                    b.Property<string>("ProfileImage");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("prototype_database.Dal.UserGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("GroupId");

                    b.Property<bool>("IsMain");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("UserGroups");
                });

            modelBuilder.Entity("prototype_database.Dal.UserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsMain");

                    b.Property<Guid>("RoleId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("prototype_database.Dal.Group", b =>
                {
                    b.HasOne("prototype_database.Dal.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("prototype_database.Dal.User", b =>
                {
                    b.HasOne("prototype_database.Dal.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}