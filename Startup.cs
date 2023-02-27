// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.HttpsPolicy;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
// using Microsoft.Extensions.Logging;
// using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
using WepApiCRUD.Data;
using WepApiCRUD.Data.Interfaces;
using WepApiCRUD.Mapper;
using WepApiCRUD.Services;
using WepApiCRUD.Services.interfaces;

namespace WepApiCRUD
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add Services to the container.
            var conf = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataContext>(x =>
            {
                x.UseLazyLoadingProxies();
                x.UseSqlite(conf);
            });
            // Add repositories
            services.AddScoped<IApiRepository, ApiRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            
            // Add services
            services.AddScoped<ITokenService, TokenService>();

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            // secret jwt token
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => 
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });




            // services.AddCors(options =>
            // {
            //     options.AddPolicy(name: "_myAllowSpecificOrigins",
            //                       policy =>
            //                       {
            //                           policy.WithOrigins("http://localhost:3000/",
            //                                               "http://www.contoso.com");
            //                       });
            // });
            // services.AddSignalR();
            // services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            // services.AddHttpContextAccessor();
            // services.AddHttpContextAccessor();
            services.AddControllers();
            // services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivosLocal>();

            // services.AddScoped<CFuncionesComprobantesElectronicos>();
            // services.AddScoped<CSriws>();
            // services.AddScoped<CGenerarRide>();
            // services.AddScoped<CCorreo>();
            //services.AddScoped<CFuncionCedulas>();
            //services.AddScoped<CConsultaSri>();
            //services.AddScoped<CRespuestaAutorizacionSRI>();


            //         services.AddDbContext<DbContextFacturasTesis>(options =>
            //   options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //         services.AddIdentity<NetUserAditional, IdentityRole>()

            //         .AddEntityFrameworkStores<DbContextFacturasTesis>()
            //         .AddDefaultTokenProviders();

            //         services.Configure<IdentityOptions>(options =>
            //         {
            //             // Default Password settings.
            //             options.Password.RequireDigit = false;
            //             options.Password.RequireLowercase = false;
            //             options.Password.RequireNonAlphanumeric = false;
            //             options.Password.RequireUppercase = false;
            //             options.Password.RequiredLength = 8;

            //         });
            //         services.AddAuthorization(opciones =>
            //         {

            //             opciones.AddPolicy("AdminEmpresa", policy => policy.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
            //                   "AdminEmpresa"));

            //        //     opciones.AddPolicy("Usuario", policy => policy.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
            //        //"UsuarioEmpresa", "AdminEmpresa"));

            //         });
            //         services.AddAuthentication(options =>
            //         {
            //             options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //             options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //             options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //         })
            //  .AddJwtBearer(options =>
            //  {
            //      options.SaveToken = true;
            //      options.RequireHttpsMetadata = false;
            //      options.TokenValidationParameters = new TokenValidationParameters
            //      {
            //          ValidateIssuer = false,
            //          ValidateAudience = false,
            //          ValidateLifetime = true,
            //          ValidateIssuerSigningKey = true,
            //          IssuerSigningKey = new SymmetricSecurityKey(
            //       Encoding.UTF8.GetBytes(Configuration["jwt:key"])),
            //          ClockSkew = TimeSpan.Zero
            //      };
            //  });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            services.AddSwaggerGen();
            //     services.AddScoped<IInicializadorDB, InicializadorDB>();

            // services.AddSingleton<IConfiguration>(Configuration);
            //services.AddSwaggerGen(options =>
            //{
            //    options.SwaggerDoc("v1", new OpenApiInfo
            //    {
            //        Title = "NesSoftFactApi",
            //        Version = "v1",
            //        Description = "Version de Api NesFAct Consumo de servicios realizados en .net core 6",
            //        TermsOfService = new Uri("https://nes-soft.te/terms"),
            //        Contact = new OpenApiContact
            //        {
            //            Name = "NesSoftFactApiDesarrollo",
            //            Email = "edwinwla13@hotmail.com",
            //            Url = new Uri("https://www.nes-soft.com/"),

            //        },

            //        License = new OpenApiLicense
            //        {
            //            Name = "Uso bajo supervicion",
            //            Url = new Uri("https://choosealicense.com/licenses/mit/"),
            //        }
            //    });

            //    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //    options.IncludeXmlComments(xmlPath);
            //}
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApiCRUD", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            // services.AddControllersWithViews();
            // services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiRest v1"));
            }
            // app.UseCors(x => x
            //     .AllowAnyOrigin()
            //     .AllowAnyMethod()
            //     .AllowAnyHeader());

            app.UseHttpsRedirection();

            // Add authentication
            app.UseAuthentication();
            app.UseAuthorization();
            // app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyWebApiRest v1"));

            app.UseRouting();

            try
            {
                app.UseAuthorization();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });


            }
            catch (System.Exception exp)
            {
                Console.WriteLine(exp.Message);
                throw;
            }

        }
    }
}


// namespace WepApiCRUD{
//     public class Startup
// {
//     public void ConfigureServices(this WebApplicationBuilder builder)
//     {
//         // ...

//         // builder.Services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration, "AzureAd");
//         builder.Services.AddControllers();
//     }

//     public void Configure(this WebApplication app)
//     {
//         // ...

//         app.UseCors();

//         // Configure the HTTP request pipeline.
//         if (app.Environment.IsDevelopment())
//         {
//             app.UseSwagger();
//             app.UseSwaggerUI();
//         }
//         app.UseHttpsRedirection();
//         app.UseAuthorization();
//         app.MapControllers();
//     }
// }
// }




