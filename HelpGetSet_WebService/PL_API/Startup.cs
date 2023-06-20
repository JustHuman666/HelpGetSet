using PL_API.AutoMapper;
using BLL.AutoMapper;
using BLL.Interfaces;
using BLL.Services;
using BLL.JwtAuthOptions;
using DAL.Enteties;
using DAL.Interfaces.BaseInterfaces;
using DAL.Interfaces;
using DAL.Repositories;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using DAL.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace PL_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            services.AddDbContext<SiteContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("railway"), builder =>
                {
                    builder.MigrationsAssembly("DAL");
                    builder.EnableRetryOnFailure();
                }));

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;

            }).AddEntityFrameworkStores<SiteContext>();


            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<ICountryHistoryRepository, CountryChangesRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IMigrantRepository, MigrantRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVolunteerRepository, VolunteerRepository>();


            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<ICountryChangesHistoryService, CountryChangesHistoryService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IMigrantService, MigrantService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IVolunteerService, VolunteerService>();
            

            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddAutoMapper(typeof(AutoMapperPl));

            var authConfiguration = Configuration.GetSection("AuthOptions");
            AuthOptions.Issuer = authConfiguration["Issuer"];
            AuthOptions.Audience = authConfiguration["Audience"];
            AuthOptions.Key = authConfiguration["Key"];
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = AuthOptions.Issuer,

                    ValidateAudience = true,
                    ValidAudience = AuthOptions.Audience,

                    ValidateLifetime = true,

                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "HelpGetSetApi",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer",
                    Description = "Enter JWT token into field"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseExceptionHandler("/error");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HelpGetSetAPI"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
