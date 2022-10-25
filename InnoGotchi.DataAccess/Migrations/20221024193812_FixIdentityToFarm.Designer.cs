﻿// <auto-generated />
using System;
using InnoGotchi.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InnoGotchi.DataAccess.Migrations
{
    [DbContext(typeof(InnoGotchiDbContext))]
    [Migration("20221024193812_FixIdentityToFarm")]
    partial class FixIdentityToFarm
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("InnoGotchi.DataAccess.Models.BodyPart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("BodyPartTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BodyPartTypeId");

                    b.ToTable("BodyParts");
                });

            modelBuilder.Entity("InnoGotchi.DataAccess.Models.BodyPartType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BodyPartTypes");
                });

            modelBuilder.Entity("InnoGotchi.DataAccess.Models.Collaborator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("FarmId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FarmId");

                    b.HasIndex("UserId");

                    b.ToTable("Collaborators");
                });

            modelBuilder.Entity("InnoGotchi.DataAccess.Models.Farm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Farms");
                });

            modelBuilder.Entity("InnoGotchi.DataAccess.Models.IdentityRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("IdentityRoles");
                });

            modelBuilder.Entity("InnoGotchi.DataAccess.Models.IdentityUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("IdentityUsers");
                });

            modelBuilder.Entity("InnoGotchi.DataAccess.Models.Pet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("BodyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EyeId")
                        .HasColumnType("int");

                    b.Property<int>("FarmId")
                        .HasColumnType("int");

                    b.Property<int?>("MouthId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NoseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BodyId");

                    b.HasIndex("EyeId");

                    b.HasIndex("FarmId");

                    b.HasIndex("MouthId");

                    b.HasIndex("NoseId");

                    b.ToTable("Pets");
                });

            modelBuilder.Entity("InnoGotchi.DataAccess.Models.VitalSign", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("HappinessDaysCount")
                        .HasColumnType("int");

                    b.Property<int>("HungerLevel")
                        .HasColumnType("int");

                    b.Property<bool>("IsAlive")
                        .HasColumnType("bit");

                    b.Property<int>("PetId")
                        .HasColumnType("int");

                    b.Property<int>("ThirsyLevel")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PetId");

                    b.ToTable("VitalSigns");
                });

            modelBuilder.Entity("InnoGotchi.DataAccess.Models.BodyPart", b =>
                {
                    b.HasOne("InnoGotchi.DataAccess.Models.BodyPartType", "BodyPartType")
                        .WithMany()
                        .HasForeignKey("BodyPartTypeId");

                    b.Navigation("BodyPartType");
                });

            modelBuilder.Entity("InnoGotchi.DataAccess.Models.Collaborator", b =>
                {
                    b.HasOne("InnoGotchi.DataAccess.Models.Farm", "Farm")
                        .WithMany()
                        .HasForeignKey("FarmId");

                    b.HasOne("InnoGotchi.DataAccess.Models.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Farm");

                    b.Navigation("User");
                });

            modelBuilder.Entity("InnoGotchi.DataAccess.Models.Farm", b =>
                {
                    b.HasOne("InnoGotchi.DataAccess.Models.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("InnoGotchi.DataAccess.Models.IdentityUser", b =>
                {
                    b.HasOne("InnoGotchi.DataAccess.Models.IdentityRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("InnoGotchi.DataAccess.Models.Pet", b =>
                {
                    b.HasOne("InnoGotchi.DataAccess.Models.BodyPart", "Body")
                        .WithMany()
                        .HasForeignKey("BodyId");

                    b.HasOne("InnoGotchi.DataAccess.Models.BodyPart", "Eye")
                        .WithMany()
                        .HasForeignKey("EyeId");

                    b.HasOne("InnoGotchi.DataAccess.Models.Farm", "Farm")
                        .WithMany("Pets")
                        .HasForeignKey("FarmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InnoGotchi.DataAccess.Models.BodyPart", "Mouth")
                        .WithMany()
                        .HasForeignKey("MouthId");

                    b.HasOne("InnoGotchi.DataAccess.Models.BodyPart", "Nose")
                        .WithMany()
                        .HasForeignKey("NoseId");

                    b.Navigation("Body");

                    b.Navigation("Eye");

                    b.Navigation("Farm");

                    b.Navigation("Mouth");

                    b.Navigation("Nose");
                });

            modelBuilder.Entity("InnoGotchi.DataAccess.Models.VitalSign", b =>
                {
                    b.HasOne("InnoGotchi.DataAccess.Models.Pet", "Pet")
                        .WithMany()
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pet");
                });

            modelBuilder.Entity("InnoGotchi.DataAccess.Models.Farm", b =>
                {
                    b.Navigation("Pets");
                });
#pragma warning restore 612, 618
        }
    }
}
