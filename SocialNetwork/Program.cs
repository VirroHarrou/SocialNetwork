using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SocialNetwork.Core;
using SocialNetwork.Core.Common.Behaviors;
using SocialNetwork.Core.Common.Mappings;
using SocialNetwork.Core.Interfaces;
using SocialNetwork.Domain.Context;
using SocialNetwork.Domain.Middleware;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace SocialNetwork
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
            builder.Services.AddApiVersioning();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddVersionedApiExplorer(options =>
                options.GroupNameFormat = "'v'VVV");
            builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            builder.Services.AddSwaggerGen(config =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
            app.UseSwaggerUI(config =>
            {              
                foreach(var desciption in provider.ApiVersionDescriptions)
                {
                    config.SwaggerEndpoint($"{desciption.GroupName}/swagger.json",
                        $"Social Network API - {desciption.GroupName.ToUpperInvariant()}");
                    config.RoutePrefix = "swagger";
                }               
            });
            app.UseCustomExceptionHandler();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseApiVersioning();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            app.Run();
        }
    }
}