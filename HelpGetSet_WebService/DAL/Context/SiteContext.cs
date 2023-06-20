using DAL.Enteties;
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

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Migrant> Migrants { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<UserChat> UsersChats { get; set; }
        public DbSet<CountryChangesHistory> CountriesHistories { get; set; }
        public DbSet<UserApprove> UsersApproves { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var admin = new Role() { Id = 1, Name = "Admin", NormalizedName = "ADMIN" };
            var registered = new Role() { Id = 2, Name = "Registered", NormalizedName = "REGISTERED" };

            builder.Entity<Role>().HasData(admin, registered);
            var country = new Country() {
                Id = 1,
                Name = "The Netherlands",
                ShortName = "NL",
                CountryVersions = new HashSet<CountryChangesHistory>(),
                UsersFrom = new HashSet<UserProfile>(),
                UsersIn = new HashSet<UserProfile>()
            };
            
            builder.Entity<Country>().HasData(country);

            var adminData = new User()
            {
                Id = 1,
                PhoneNumber = "380671234567",
                UserName = "AdminElya",
                NormalizedUserName = "ADMINELYA"
            };
            var adminProfile = new UserProfile()
            {
                Id = adminData.Id,
                FirstName = "Eleonora",
                LastName = "Mykhalchuk",
                OriginalCountryId = country.Id,
                CurrentCountryId = country.Id
            };

            var passwordHasher = new PasswordHasher<User>();
            adminData.PasswordHash = passwordHasher.HashPassword(adminData, "AdminPassword_1");

            builder.Entity<User>().HasData(adminData);
            builder.Entity<UserProfile>().HasData(adminProfile);

            builder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int> { RoleId = admin.Id, UserId = adminProfile.Id },
                new IdentityUserRole<int> { RoleId = registered.Id, UserId = adminProfile.Id });


            builder.Entity<UserProfile>().HasKey(x => new { x.Id });

            //builder.Entity<UserProfile>().HasMany(x => x.Chats).WithOne(x => x.User).HasForeignKey(x => x.UserId);
            //builder.Entity<Chat>().HasMany(x => x.Users).WithOne(x => x.Chat).HasForeignKey(x => x.ChatId);

            builder.Entity<UserProfile>().HasOne(x => x.AppUser).WithOne(x => x.UserProfile).HasForeignKey<UserProfile>(x => x.Id);

            builder.Entity<UserProfile>().HasMany(x => x.Migrants).WithOne(x => x.User).HasForeignKey(x => x.UserId);
            builder.Entity<UserProfile>().HasMany(x => x.Volunteers).WithOne(x => x.User).HasForeignKey(x => x.UserId);

            builder.Entity<UserProfile>().HasMany(x => x.Posts).WithOne(x => x.Author).HasForeignKey(x => x.AuthorId);
            builder.Entity<Post>().HasOne(x => x.Author).WithMany(x => x.Posts).HasForeignKey(x => x.AuthorId);

            builder.Entity<Migrant>().HasOne(x => x.User).WithMany(x => x.Migrants).HasForeignKey(x => x.UserId);
            builder.Entity<Volunteer>().HasOne(x => x.User).WithMany(x => x.Volunteers).HasForeignKey(x => x.UserId);

            builder.Entity<Country>().HasMany(x => x.Posts).WithOne(x => x.Country).OnDelete(DeleteBehavior.Cascade).HasForeignKey(x => x.CountryId);
            builder.Entity<Post>().HasOne(x => x.Country).WithMany(x => x.Posts).HasForeignKey(x => x.CountryId);

            builder.Entity<CountryChangesHistory>().HasKey(x => new { x.Id });
            builder.Entity<CountryChangesHistory>().HasOne(x => x.Author).WithMany(x => x.MadeCountryChanges).OnDelete(DeleteBehavior.NoAction).HasForeignKey(x => x.AuthorId);
            builder.Entity<CountryChangesHistory>().HasOne(x => x.Country).WithMany(x => x.CountryVersions).OnDelete(DeleteBehavior.Cascade).HasForeignKey(x => x.CountryId);

            builder.Entity<UserProfile>().HasOne(x => x.OriginalCountry).WithMany(x => x.UsersFrom).OnDelete(DeleteBehavior.NoAction).HasForeignKey(x => x.OriginalCountryId);
            builder.Entity<UserProfile>().HasOne(x => x.CurrentCountry).WithMany(x => x.UsersIn).OnDelete(DeleteBehavior.NoAction).HasForeignKey(x => x.CurrentCountryId);
            builder.Entity<Country>().HasMany(x => x.UsersFrom).WithOne(x => x.OriginalCountry).HasForeignKey(x => x.OriginalCountryId);
            builder.Entity<Country>().HasMany(x => x.UsersIn).WithOne(x => x.CurrentCountry).HasForeignKey(x => x.CurrentCountryId);

            builder.Entity<UserChat>().HasKey(x => new { x.UserId, x.ChatId });
            builder.Entity<UserChat>().HasOne(x => x.User).WithMany(x => x.Chats).OnDelete(DeleteBehavior.Cascade).HasForeignKey(x => x.UserId);
            builder.Entity<UserChat>().HasOne(x => x.Chat).WithMany(x => x.Users).OnDelete(DeleteBehavior.Cascade).HasForeignKey(x => x.ChatId);

            builder.Entity<UserApprove>().HasKey(x => new { x.Id });
            builder.Entity<UserApprove>().HasOne(x => x.User).WithMany(x => x.CountryVersionsChecked).OnDelete(DeleteBehavior.NoAction).HasForeignKey(x => x.UserId);
            builder.Entity<UserApprove>().HasOne(x => x.CountryVersion).WithMany(x => x.UsersWhoChecked).OnDelete(DeleteBehavior.NoAction).HasForeignKey(x => x.VersionId);
        }
    }
}
