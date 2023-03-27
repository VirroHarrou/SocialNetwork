using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Common.Mappings;
using SocialNetwork.Core;
using SocialNetwork.Core.Common.Behaviors;
using SocialNetwork.Core.Interfaces;
using SocialNetwork.Domain.Context;
using System.Reflection;

namespace Messanger_API
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            try
            {
                var context = builder.Services.BuildServiceProvider()
                    .GetRequiredService<SocialNetworkContext>();
                DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {

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

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowAll");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}