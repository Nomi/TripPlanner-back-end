﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TripPlannerAPI.Data;

#nullable disable

namespace TripPlannerAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "522a963a-74a5-4b90-a087-df0c0056c119",
                            ConcurrencyStamp = "f00b2976-7722-428d-95f3-f36bf477f966",
                            Name = "User",
                            NormalizedName = "USER"
                        },
                        new
                        {
                            Id = "3d19bc9a-8f90-49a8-b704-e87a9ee61b75",
                            ConcurrencyStamp = "799778ef-dcd4-431f-9997-df1558a3045a",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("TripPlannerAPI.Models.Location", b =>
                {
                    b.Property<int>("locationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("locationId"));

                    b.Property<float>("lat")
                        .HasColumnType("real");

                    b.Property<float>("lng")
                        .HasColumnType("real");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("tripId")
                        .HasColumnType("int");

                    b.HasKey("locationId");

                    b.HasIndex("tripId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("TripPlannerAPI.Models.Pin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float>("Lat")
                        .HasColumnType("real");

                    b.Property<float>("Lng")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("tripId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("tripId");

                    b.ToTable("Pin");
                });

            modelBuilder.Entity("TripPlannerAPI.Models.Post", b =>
                {
                    b.Property<int>("postId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("postId"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("RelatedTriptripId")
                        .HasColumnType("int");

                    b.HasKey("postId");

                    b.HasIndex("CreatorId");

                    b.HasIndex("RelatedTriptripId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("TripPlannerAPI.Models.Preference", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("preferenceStr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("tripId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("tripId");

                    b.ToTable("Preference");
                });

            modelBuilder.Entity("TripPlannerAPI.Models.PreferenceType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("PreferenceTypeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PreferenceType");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PreferenceTypeName = "Entertainment"
                        },
                        new
                        {
                            Id = 2,
                            PreferenceTypeName = "Sightseeing"
                        },
                        new
                        {
                            Id = 3,
                            PreferenceTypeName = "Exploring"
                        },
                        new
                        {
                            Id = 4,
                            PreferenceTypeName = "Culture"
                        },
                        new
                        {
                            Id = 5,
                            PreferenceTypeName = "History"
                        },
                        new
                        {
                            Id = 6,
                            PreferenceTypeName = "Free ride"
                        },
                        new
                        {
                            Id = 7,
                            PreferenceTypeName = "Training"
                        },
                        new
                        {
                            Id = 8,
                            PreferenceTypeName = "Nature"
                        });
                });

            modelBuilder.Entity("TripPlannerAPI.Models.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsOrganizer")
                        .HasColumnType("bit");

                    b.Property<float>("RatingPoints")
                        .HasColumnType("real");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("TripPlannerAPI.Models.Trip", b =>
                {
                    b.Property<int>("tripId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("tripId"));

                    b.Property<DateTime>("creationDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("creatorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("distance")
                        .HasColumnType("real");

                    b.Property<bool>("isRecommended")
                        .HasColumnType("bit");

                    b.Property<float>("totalTime")
                        .HasColumnType("real");

                    b.Property<string>("type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("tripId");

                    b.HasIndex("creatorId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("TripPlannerAPI.Models.TripType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TripType");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            TypeName = "car"
                        },
                        new
                        {
                            Id = 2,
                            TypeName = "bike"
                        },
                        new
                        {
                            Id = 3,
                            TypeName = "foot"
                        });
                });

            modelBuilder.Entity("TripPlannerAPI.Models.TripTypePreference", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<int>("PreferenceTypeId")
                        .HasColumnType("int");

                    b.Property<int>("TripTypeId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("PreferenceTypeId");

                    b.HasIndex("TripTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("TripTypesPreferences");
                });

            modelBuilder.Entity("TripPlannerAPI.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<float>("OrganizerRating")
                        .HasColumnType("real");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<float>("UserRating")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("TripUser", b =>
                {
                    b.Property<int>("FavoriteTripstripId")
                        .HasColumnType("int");

                    b.Property<string>("FavoritedById")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("FavoriteTripstripId", "FavoritedById");

                    b.HasIndex("FavoritedById");

                    b.ToTable("TripUser");
                });

            modelBuilder.Entity("TripUser1", b =>
                {
                    b.Property<int>("TripsJoinedtripId")
                        .HasColumnType("int");

                    b.Property<string>("membersId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("TripsJoinedtripId", "membersId");

                    b.HasIndex("membersId");

                    b.ToTable("TripUser1");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("TripPlannerAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("TripPlannerAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TripPlannerAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("TripPlannerAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TripPlannerAPI.Models.Location", b =>
                {
                    b.HasOne("TripPlannerAPI.Models.Trip", null)
                        .WithMany("waypoints")
                        .HasForeignKey("tripId");
                });

            modelBuilder.Entity("TripPlannerAPI.Models.Pin", b =>
                {
                    b.HasOne("TripPlannerAPI.Models.Trip", null)
                        .WithMany("Pins")
                        .HasForeignKey("tripId");
                });

            modelBuilder.Entity("TripPlannerAPI.Models.Post", b =>
                {
                    b.HasOne("TripPlannerAPI.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");

                    b.HasOne("TripPlannerAPI.Models.Trip", "RelatedTrip")
                        .WithMany()
                        .HasForeignKey("RelatedTriptripId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("RelatedTrip");
                });

            modelBuilder.Entity("TripPlannerAPI.Models.Preference", b =>
                {
                    b.HasOne("TripPlannerAPI.Models.Trip", null)
                        .WithMany("preferences")
                        .HasForeignKey("tripId");
                });

            modelBuilder.Entity("TripPlannerAPI.Models.Rating", b =>
                {
                    b.HasOne("TripPlannerAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TripPlannerAPI.Models.Trip", b =>
                {
                    b.HasOne("TripPlannerAPI.Models.User", "creator")
                        .WithMany("CreatedTrips")
                        .HasForeignKey("creatorId");

                    b.Navigation("creator");
                });

            modelBuilder.Entity("TripPlannerAPI.Models.TripTypePreference", b =>
                {
                    b.HasOne("TripPlannerAPI.Models.PreferenceType", "PreferenceType")
                        .WithMany()
                        .HasForeignKey("PreferenceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TripPlannerAPI.Models.TripType", "TripType")
                        .WithMany()
                        .HasForeignKey("TripTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TripPlannerAPI.Models.User", "User")
                        .WithMany("TripTypesPreferences")
                        .HasForeignKey("UserId");

                    b.Navigation("PreferenceType");

                    b.Navigation("TripType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TripUser", b =>
                {
                    b.HasOne("TripPlannerAPI.Models.Trip", null)
                        .WithMany()
                        .HasForeignKey("FavoriteTripstripId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TripPlannerAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("FavoritedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TripUser1", b =>
                {
                    b.HasOne("TripPlannerAPI.Models.Trip", null)
                        .WithMany()
                        .HasForeignKey("TripsJoinedtripId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TripPlannerAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("membersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TripPlannerAPI.Models.Trip", b =>
                {
                    b.Navigation("Pins");

                    b.Navigation("preferences");

                    b.Navigation("waypoints");
                });

            modelBuilder.Entity("TripPlannerAPI.Models.User", b =>
                {
                    b.Navigation("CreatedTrips");

                    b.Navigation("TripTypesPreferences");
                });
#pragma warning restore 612, 618
        }
    }
}
