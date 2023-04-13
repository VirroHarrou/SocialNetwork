using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace SocialNetwork
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => 
            _provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach(var description in _provider.ApiVersionDescriptions)
            {
                var apiVersion = description.ApiVersion.ToString();
                options.SwaggerDoc(description.GroupName,
                    new OpenApiInfo
                    {
                        Version = apiVersion,
                        Title = $"Social Network API {apiVersion}",
                        Description = "Simple ASP NET Core Web API",
                        Contact = new OpenApiContact
                        {
                            Name = "Contact with me",
                            Email = string.Empty,
                            Url = new Uri("https://t.me/EBAHXELb")
                        },
                        License = new OpenApiLicense
                        {
                            Name = "Ivan Fot",
                            Url = new Uri("https://vk.com/virroharrou")
                        }
                    });
                options.AddSecurityDefinition($"AuthToken {apiVersion}",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "bearer",
                        Name = "Authirization",
                        Description = "Authirization token"
                    });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = $"AuthToken {apiVersion}"
                            }
                        },
                        new string[] {} 
                    }
                });
                options.CustomOperationIds(apiDescription =>
                apiDescription.TryGetMethodInfo(out MethodInfo methodInfo)
                ? methodInfo.Name : null);
            }
        }
    }
}
