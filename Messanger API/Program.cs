using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Common.Mappings;
using SocialNetwork.Core;
using SocialNetwork.Core.Common.Behaviors;
using SocialNetwork.Core.Interfaces;
using SocialNetwork.Domain.Context;
using SocialNetwork.Domain.Middleware;
using System.Reflection;

namespace Messanger_API
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            using (var scope = builder.Services.BuildServiceProvider())
            {
                try
                {
                    var context = scope
                        .GetRequiredService<SocialNetworkContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = scope.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while app initialization");
                }
            }
            builder.Services.AddAutoMapper(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                config.AddProfile(new AssemblyMappingProfile(typeof(INewsDbContext).Assembly));
                config.AddProfile(new AssemblyMappingProfile(typeof(ILikeDbContext).Assembly));
            });

            builder.Services.AddDbContext<SocialNetworkContext>(options => options
                .UseSqlServer(builder.Configuration.GetConnectionString("DebugConnection")));

            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });

            builder.Services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://localhost:60398";
                    options.Audience = "SocialNetworkWebApi";
                    options.RequireHttpsMetadata = false;
                });

            builder.Services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            builder.Services.AddScoped<INewsDbContext>(provider => provider.GetService<SocialNetworkContext>());
            builder.Services.AddScoped<ILikeDbContext>(provider => provider.GetService<SocialNetworkContext>());
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCustomExceptionHandler();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}