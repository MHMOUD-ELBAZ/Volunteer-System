using Demo.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;


namespace Demo.API.Services
{
    public class Registration
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("Default"))
            );

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;  
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<AppDbContext>();


            // Register the repositories as scoped services
            services.AddScoped<VolunteerRepository, VolunteerRepository>();
            services.AddScoped<IApplicationRepository, ApplicationRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IOpportunityRepository, OpportunityRepository>();
            services.AddScoped<IOpportunitySkillRepository, OpportunitySkillRepository>();
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<ISkillRepository, SkillRepository>();
            services.AddScoped<IVolunteerRepository, VolunteerRepository>();
            services.AddScoped<IVolunteerSkillRepository, VolunteerSkillRepository>();



            services.AddScoped<IVolunteerService, VolunteerService>();
            services.AddScoped<IOrganizationService, OrganizationService>();
            services.AddScoped<IOpportunityService, OpportunityService>();
            services.AddScoped<ISkillService, SkillService>();
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<IReviewService, ReviewService>();
            

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWT:issuer"],
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:secretKey"]))
                };
            });


            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", policy =>
                {
                    //policy.WithOrigins("")
                    //.AllowAnyMethod().AllowAnyHeader();
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();

                });
            });

            // Add Swagger services
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hands of hope-API", Version = "v1" });

                // Add security definitions and requirements
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
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
            });

        }
    }
}
