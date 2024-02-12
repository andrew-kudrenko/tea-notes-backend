﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TeaNotes.Database;

#nullable disable

namespace TeaNotes.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240206124742_Added Email field but removed AvatarUrl")]
    partial class AddedEmailfieldbutremovedAvatarUrl
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.9");

            modelBuilder.Entity("TeaNotes.Auth.Models.RefreshSession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("RefreshSessions");
                });

            modelBuilder.Entity("TeaNotes.Notes.Models.TeaNote", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AftertasteComment")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("TEXT");

                    b.Property<int?>("AftertasteDuration")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AftertasteIntensity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("BrewingDishware")
                        .HasColumnType("TEXT");

                    b.Property<string>("BrewingMethod")
                        .HasColumnType("TEXT");

                    b.Property<int?>("BrewingQuantity")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BrewingTemperature")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BrewingVolume")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DryLeafAppearance")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("TEXT");

                    b.Property<string>("DryLeafAroma")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("TEXT");

                    b.Property<string>("ImpressionComment")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("TEXT");

                    b.Property<int?>("ImpressionRate")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImpressionWellCombinedWith")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("TEXT");

                    b.Property<string>("InfusionAppearance")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("TEXT");

                    b.Property<string>("InfusionAroma")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("TEXT");

                    b.Property<int?>("InfusionBalance")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("InfusionBouquet")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("InfusionDensity")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("InfusionExtractivity")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("InfusionTartness")
                        .HasColumnType("INTEGER");

                    b.Property<string>("InfusionTaste")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("TEXT");

                    b.Property<int?>("InfusionViscosity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Kind")
                        .HasColumnType("TEXT");

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int?>("ManufacturingYear")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PricePerGram")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("TastingDate")
                        .HasColumnType("Date");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("TeaNotes");
                });

            modelBuilder.Entity("TeaNotes.Notes.Models.TeaTaste", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Kind")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TeaNoteId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TeaNoteId");

                    b.ToTable("TeaTastes");
                });

            modelBuilder.Entity("TeaNotes.Users.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NickName")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TeaNotes.Notes.Models.TeaNote", b =>
                {
                    b.HasOne("TeaNotes.Users.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TeaNotes.Notes.Models.TeaTaste", b =>
                {
                    b.HasOne("TeaNotes.Notes.Models.TeaNote", "TeaNote")
                        .WithMany("Tastes")
                        .HasForeignKey("TeaNoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TeaNote");
                });

            modelBuilder.Entity("TeaNotes.Notes.Models.TeaNote", b =>
                {
                    b.Navigation("Tastes");
                });
#pragma warning restore 612, 618
        }
    }
}
