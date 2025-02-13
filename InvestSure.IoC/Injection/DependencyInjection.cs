
using System.Text;
using InvestSure.App.Interfaces;
using InvestSure.App.Mappings;
using InvestSure.App.Services;
using InvestSure.Domain.Interfaces;
using InvestSure.Infra.Data;
using InvestSure.Infra.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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
            services.AddScoped<IloginService, LoginService>();
            services.AddScoped<IAuthenticationService, AuthenticateService>();
            services.AddScoped<DBSession>();
            services.AddAutoMapper(typeof(MappingDTO));

            services.AddAuthentication(opt =>
            {

                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["jwt:issuer"],
                    ValidAudience = configuration["jwt:audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:secretkey"])

                    )

                };
            });

            return services;


        }
    }
}
