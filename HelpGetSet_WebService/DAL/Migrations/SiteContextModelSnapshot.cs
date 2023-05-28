﻿// <auto-generated />
using System;
using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(SiteContext))]
    partial class SiteContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DAL.Enteties.Chat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ChatName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("DAL.Enteties.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Countries");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "The Netherlands",
                            ShortName = "NL"
                        });
                });

            modelBuilder.Entity("DAL.Enteties.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ChatId")
                        .HasColumnType("int");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SendingTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("DAL.Enteties.Migrant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AmountOfChildren")
                        .HasColumnType("int");

                    b.Property<int>("FamilyStatus")
                        .HasColumnType("int");

                    b.Property<int>("Housing")
                        .HasColumnType("int");

                    b.Property<bool>("IsCommonMigrant")
                        .HasColumnType("bit");

                    b.Property<bool>("IsEmployed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsForcedMigrant")
                        .HasColumnType("bit");

                    b.Property<bool>("IsOfficialRefugee")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Migrants");
                });

            modelBuilder.Entity("DAL.Enteties.Post", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("DAL.Enteties.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ConcurrencyStamp = "e612ad1f-5f86-4e53-80ac-271329267130",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = 2,
                            ConcurrencyStamp = "d981f57c-6cea-4253-a9f8-548d0aae2f12",
                            Name = "Registered",
                            NormalizedName = "REGISTERED"
                        });
                });

            modelBuilder.Entity("DAL.Enteties.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

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
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "10540b5d-4484-4565-9c03-b8158ad8055c",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "ADMINELYA",
                            PasswordHash = "AQAAAAEAACcQAAAAEFAhtQ+DwRuD4JcrD7Vi9hw0l5tlb+sVprNB71EMAKaMWOWLLkQOmIlrU6HsUqZS4w==",
                            PhoneNumber = "+380671234567",
                            PhoneNumberConfirmed = false,
                            TwoFactorEnabled = false,
                            UserName = "AdminElya"
                        });
                });

            modelBuilder.Entity("DAL.Enteties.UserChat", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("ChatId")
                        .HasColumnType("int");

                    b.Property<bool>("isAdmin")
                        .HasColumnType("bit");

                    b.HasKey("UserId", "ChatId");

                    b.HasIndex("ChatId");

                    b.ToTable("UsersChats");
                });

            modelBuilder.Entity("DAL.Enteties.UserProfile", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<int>("CurrentCountryId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<int>("OriginalCountryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CurrentCountryId");

                    b.HasIndex("OriginalCountryId");

                    b.ToTable("UserProfiles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Birthday = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentCountryId = 1,
                            FirstName = "Eleonora",
                            Gender = 0,
                            LastName = "Mykhalchuk",
                            OriginalCountryId = 1
                        });
                });

            modelBuilder.Entity("DAL.Enteties.Volunteer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("HasAPlace")
                        .HasColumnType("bit");

                    b.Property<bool>("IsATranslator")
                        .HasColumnType("bit");

                    b.Property<bool>("IsOrganisation")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Volunteers");
                });

            modelBuilder.Entity("DAL.Entities.CountryChangesHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ApprovesAmount")
                        .HasColumnType("int");

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("AuthorUsername")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ChangeTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<int>("DisApprovesAmount")
                        .HasColumnType("int");

                    b.Property<string>("EmploymentInfo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InsuranceInfo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegistrationInfo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupportInfo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaxInfo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("CountryId");

                    b.ToTable("CountriesHistories");
                });

            modelBuilder.Entity("DAL.Entities.UserApprove", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Approved")
                        .HasColumnType("bit");

                    b.Property<bool>("DisApproved")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("VersionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("VersionId");

                    b.ToTable("UsersApproves");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            RoleId = 1
                        },
                        new
                        {
                            UserId = 1,
                            RoleId = 2
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("DAL.Enteties.Message", b =>
                {
                    b.HasOne("DAL.Enteties.Chat", "UsersChat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatId");

                    b.HasOne("DAL.Enteties.UserProfile", "Sender")
                        .WithMany("Messages")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.Enteties.Migrant", b =>
                {
                    b.HasOne("DAL.Enteties.UserProfile", "User")
                        .WithMany("Migrants")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.Enteties.Post", b =>
                {
                    b.HasOne("DAL.Enteties.UserProfile", "Author")
                        .WithMany("Posts")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Enteties.Country", "Country")
                        .WithMany("Posts")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.Enteties.UserChat", b =>
                {
                    b.HasOne("DAL.Enteties.Chat", "Chat")
                        .WithMany("Users")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Enteties.UserProfile", "User")
                        .WithMany("Chats")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.Enteties.UserProfile", b =>
                {
                    b.HasOne("DAL.Enteties.Country", "CurrentCountry")
                        .WithMany("UsersIn")
                        .HasForeignKey("CurrentCountryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DAL.Enteties.User", "AppUser")
                        .WithOne("UserProfile")
                        .HasForeignKey("DAL.Enteties.UserProfile", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Enteties.Country", "OriginalCountry")
                        .WithMany("UsersFrom")
                        .HasForeignKey("OriginalCountryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.Enteties.Volunteer", b =>
                {
                    b.HasOne("DAL.Enteties.UserProfile", "User")
                        .WithMany("Volunteers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.Entities.CountryChangesHistory", b =>
                {
                    b.HasOne("DAL.Enteties.UserProfile", "Author")
                        .WithMany("MadeCountryChanges")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DAL.Enteties.Country", "Country")
                        .WithMany("CountryVersions")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.Entities.UserApprove", b =>
                {
                    b.HasOne("DAL.Enteties.UserProfile", "User")
                        .WithMany("CountryVersionsChecked")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DAL.Entities.CountryChangesHistory", "CountryVersion")
                        .WithMany("UsersWhoChecked")
                        .HasForeignKey("VersionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("DAL.Enteties.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("DAL.Enteties.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("DAL.Enteties.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("DAL.Enteties.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Enteties.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("DAL.Enteties.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
