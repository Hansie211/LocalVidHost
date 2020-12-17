﻿// <auto-generated />
using System;
using Database.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BlazorApp.Migrations
{
    [DbContext(typeof(MovieDatabaseContext))]
    partial class MovieDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Database.Entities.Genre", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("MovieID")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("MovieID");

                    b.ToTable("_Genres");
                });

            modelBuilder.Entity("Database.Entities.Language", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("_Languages");
                });

            modelBuilder.Entity("Database.Entities.Movie", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Filename")
                        .HasColumnType("TEXT");

                    b.Property<string>("IMDBIndex")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("LanguageID")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("LanguageID");

                    b.ToTable("_Movies");
                });

            modelBuilder.Entity("Database.Entities.MovieMetadata", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsFavorite")
                        .HasColumnType("INTEGER");

                    b.Property<double>("LastPosition")
                        .HasColumnType("REAL");

                    b.Property<Guid?>("MovieID")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("UserID")
                        .HasColumnType("TEXT");

                    b.Property<int>("ViewCount")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("MovieID");

                    b.HasIndex("UserID");

                    b.ToTable("_MovieMetadatas");
                });

            modelBuilder.Entity("Database.Entities.Subtitle", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Filename")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("LanguageID")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("MovieID")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("LanguageID");

                    b.HasIndex("MovieID");

                    b.ToTable("_Subtitles");
                });

            modelBuilder.Entity("Database.Entities.User", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("_Users");
                });

            modelBuilder.Entity("Database.Entities.Genre", b =>
                {
                    b.HasOne("Database.Entities.Movie", null)
                        .WithMany("Genres")
                        .HasForeignKey("MovieID");
                });

            modelBuilder.Entity("Database.Entities.Movie", b =>
                {
                    b.HasOne("Database.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageID");

                    b.Navigation("Language");
                });

            modelBuilder.Entity("Database.Entities.MovieMetadata", b =>
                {
                    b.HasOne("Database.Entities.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieID");

                    b.HasOne("Database.Entities.User", "User")
                        .WithMany("MovieMetadatas")
                        .HasForeignKey("UserID");

                    b.Navigation("Movie");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Database.Entities.Subtitle", b =>
                {
                    b.HasOne("Database.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageID");

                    b.HasOne("Database.Entities.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieID");

                    b.Navigation("Language");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("Database.Entities.Movie", b =>
                {
                    b.Navigation("Genres");
                });

            modelBuilder.Entity("Database.Entities.User", b =>
                {
                    b.Navigation("MovieMetadatas");
                });
#pragma warning restore 612, 618
        }
    }
}
