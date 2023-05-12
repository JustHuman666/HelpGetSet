﻿using DAL.Enteties;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace DAL.Context
{

    /// <summary>
    /// Class that represents a database with all information of network
    /// </summary>
    public class SiteContext : IdentityDbContext<User, Role, int>
    {
        /// <summary>
        /// Constructor for creating of DB context with some options
        /// </summary>
        /// <param name="options">Instance of DbContextOptions for creating DB context</param>
        public SiteContext(DbContextOptions<SiteContext> options) : base(options)
        {
        }

        //public SiteContext(DbContextOptions options) : base(options)
        //{
        //}

        //public SiteContext() : base()
        //{
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server = localhost, 1433; Database = HelpGetSet; User ID = sa; Password = <password12345>; TrustServerCertificate = True")
        //        .EnableSensitiveDataLogging();
        //}

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Migrant> Migrants { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<UserCountry> UserCountries { get; set; }
        public DbSet<UserChat> UsersChats { get; set; }
        public DbSet<CountryChangesHistory> CountriesHistories { get; set; }
        public DbSet<UserApprove> UsersApproves { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var admin = new Role() { Id = 1, Name = "Admin", NormalizedName = "ADMIN" };
            var volunteer = new Role() { Id = 2, Name = "Volunteer", NormalizedName = "VOLUNTEER" };
            var migrant = new Role() { Id = 3, Name = "Migrant", NormalizedName = "MIGRANT" };

            builder.Entity<Role>().HasData(admin, volunteer, migrant);

            var defaultMigrant = new Migrant()
            {
                Id = 1
            };

            var defaultVolunteer = new Volunteer()
            {
                Id = 1
            };

            var adminData = new User()
            {
                Id = 1,
                PhoneNumber = "+380671234567",
                UserName = "AdminElya",
                NormalizedUserName = "ADMINELYA"
            };
            var adminProfile = new UserProfile()
            {
                Id = adminData.Id,
                FirstName = "Eleonora",
                LastName = "Mykhalchuk",
                MigrantId = 1,
                VolunteerId = 1
            };

            var passwordHasher = new PasswordHasher<User>();
            adminData.PasswordHash = passwordHasher.HashPassword(adminData, "AdminPassword_1");

            builder.Entity<User>().HasData(adminData);
            builder.Entity<Migrant>().HasData(defaultMigrant);
            builder.Entity<Volunteer>().HasData(defaultVolunteer);
            builder.Entity<UserProfile>().HasData(adminProfile);

            builder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int> { RoleId = admin.Id, UserId = adminProfile.Id },
                new IdentityUserRole<int> { RoleId = volunteer.Id, UserId = adminProfile.Id },
                new IdentityUserRole<int> { RoleId = migrant.Id, UserId = adminProfile.Id });


            builder.Entity<UserProfile>().HasKey(x => new { x.Id });

            builder.Entity<UserProfile>().HasMany(x => x.Chats).WithOne(x => x.User).HasForeignKey(x => x.UserId);
            builder.Entity<Chat>().HasMany(x => x.Users).WithOne(x => x.Chat).HasForeignKey(x => x.ChatId);

            builder.Entity<UserProfile>().HasOne(x => x.AppUser).WithOne(x => x.UserProfile).HasForeignKey<UserProfile>(x => x.Id);
            builder.Entity<UserProfile>().HasOne(x => x.Volunteer).WithMany(x => x.Users).OnDelete(DeleteBehavior.Cascade).HasForeignKey(x => x.Id);
            builder.Entity<UserProfile>().HasOne(x => x.Migrant).WithMany(x => x.Users).OnDelete(DeleteBehavior.Cascade).HasForeignKey(x => x.Id);

            builder.Entity<UserProfile>().HasMany(x => x.Countries).WithOne(x => x.User).HasForeignKey(x => x.UserId);
            builder.Entity<Country>().HasMany(x => x.Users).WithOne(x => x.Country).HasForeignKey(x => x.CountryId);

            builder.Entity<Country>().HasMany(x => x.Posts).WithOne(x => x.Country).OnDelete(DeleteBehavior.Cascade).HasForeignKey(x => x.CountryId);
            builder.Entity<Post>().HasOne(x => x.Country).WithMany(x => x.Posts).HasForeignKey(x => x.Id);

            builder.Entity<CountryChangesHistory>().HasKey(x => new { x.Id });
            builder.Entity<CountryChangesHistory>().HasOne(x => x.Author).WithMany(x => x.MadeCountryChanges).OnDelete(DeleteBehavior.NoAction).HasForeignKey(x => x.AuthorId);
            builder.Entity<CountryChangesHistory>().HasOne(x => x.Country).WithMany(x => x.CountryVersions).OnDelete(DeleteBehavior.Cascade).HasForeignKey(x => x.CountryId);

            builder.Entity<UserCountry>().HasKey(x => new { x.UserId, x.CountryId });
            builder.Entity<UserCountry>().HasOne(x => x.User).WithMany(x => x.Countries).OnDelete(DeleteBehavior.Cascade).HasForeignKey(x => x.UserId);
            builder.Entity<UserCountry>().HasOne(x => x.Country).WithMany(x => x.Users).OnDelete(DeleteBehavior.NoAction).HasForeignKey(x => x.CountryId);
            
            builder.Entity<UserChat>().HasKey(x => new { x.UserId, x.ChatId });
            builder.Entity<UserChat>().HasOne(x => x.User).WithMany(x => x.Chats).OnDelete(DeleteBehavior.Cascade).HasForeignKey(x => x.UserId);
            builder.Entity<UserChat>().HasOne(x => x.Chat).WithMany(x => x.Users).OnDelete(DeleteBehavior.Cascade).HasForeignKey(x => x.ChatId);

            builder.Entity<UserApprove>().HasKey(x => new { x.Id });
            builder.Entity<UserApprove>().HasOne(x => x.User).WithMany(x => x.CountryVersionsChecked).OnDelete(DeleteBehavior.NoAction).HasForeignKey(x => x.UserId);
            builder.Entity<UserApprove>().HasOne(x => x.CountryVersion).WithMany(x => x.UsersWhoChecked).OnDelete(DeleteBehavior.NoAction).HasForeignKey(x => x.VersionId);
        }
    }
}
