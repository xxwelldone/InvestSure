
using InvestSure.Domain.Interfaces;
using InvestSure.Infra.Data;
using InvestSure.Infra.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace InvestSure.IoC.Injection
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(
                            swagger =>
                            {
                                swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Invest Sure", Description = "Off shore Investment API", Version = "v1" });
                                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                                {
                                    Name = "Authorization",
                                    Type = SecuritySchemeType.ApiKey,
                                    Scheme = "Bearer",
                                    In = ParameterLocation.Header,
                                    Description = "This is a JWT Bearer Token Authentication, Insert your Token with 'Bearer' at the begininng \\\n" +
                                    "Example: Bearer ywowknqwqytt12816gstar",
                                });
                                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                     });
                            }
                 );
            services.AddScoped<IInvestorRepository, InvestorRepository>();
            services.AddScoped<IInvestimentRepository, InvestimentRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<DBSession>();

            return services;


        }
    }
}
