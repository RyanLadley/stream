using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReelStream.data.Models.Context;
using ReelStream.data.Models.Repositories;
using ReelStream.core.Settings;
using ReelStream.core.External.Context;
using Microsoft.AspNetCore.Http.Features;
using System.Linq;
using ReelStream.data.Repositories.IRepositories;
using ReelStream.data.Repositories;
using ReelStream.auth.Logic.Interfaces;
using ReelStream.auth.Logic;
using ReelStream.auth.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ReelStream
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("authsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"authsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<AppSettings>(Configuration);
            services.Configure<AuthSettings>(Configuration);

            services.AddCors();
            services.AddMvc();
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue; // In case of multipart
            });

            services.AddScoped<IExternalMovieDatabase, ExternalMovieDatabase>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IVideoFileRepository, VideoFileRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddTransient<IPasswordManager, PasswordManager>();
            services.AddTransient<IUserRegistrar, UserRegistrar>();

            services.AddTransient<ExternalMovieDatabase>();

            services.AddDbContext<MainContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthorization(options =>
            {
                options.AddPolicy("GeneralUser", policy => policy.RequireClaim("Role", "General"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IOptions<AuthSettings> authsettings)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = GetTokenValidationParams(authsettings.Value.TokenOptions)
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseMvc();
        }


        public TokenValidationParameters GetTokenValidationParams(TokenOptions tokenOptions)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = tokenOptions.SigningKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = tokenOptions.Issuer,

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = tokenOptions.Audience,

                // Validate the token expiry
                ValidateLifetime = true,

                // If you want to allow a certain amount of clock drift, set that here:
            };

            return tokenValidationParameters;
        }
    }
}
