using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using datamakerslib.Startup;
//using Electrocore.DataContext;
//using Electrocore.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Electrocore.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Electrocore.CustomTokenProvider;
using Blog.TokenAuthGettingStarted.CustomTokenProvider;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using AspNet.Security.OAuth.Validation;
using AutoMapper;
using DotVVM.BusinessPack;
using Electro.model.DataContext;
using Electro.model.Repository;
using Electrocore.DataContext;
using Microsoft.Extensions.FileProviders;
using Amazon.S3;
using Amazon;

namespace Electrocore
{
    public partial class Startup
    {
      
      // RsaSecurityKey  key;
        public TokenOptions _tokenAuth;
        public Startup(IConfiguration configuration)
        {
          
            Configuration = configuration;   
        }

        

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
            
           // services.AddSingleton<TokenOptions>(_tokenAuth);
                 services
              .AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });


               
                    //CONFIG S3
                /* 
                Environment.SetEnvironmentVariable("AWS_ACCESS_KEY_ID", Configuration["AWS:AccessKey"]);
                Environment.SetEnvironmentVariable("AWS_SECRET_ACCESS_KEY", Configuration["AWS:SecretKey"]);
                Environment.SetEnvironmentVariable("AWS_REGION", Configuration["AWS:Region"]);
                */

               // Environment.SetEnvironmentVariable("AWS_ACCESS_KEY_ID", "AKIAIBP6JF2ZA2KOKPZA");
                //Environment.SetEnvironmentVariable("AWS_SECRET_ACCESS_KEY", "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");
                //Environment.SetEnvironmentVariable("AWS_REGION", "us-east-1");
//CONFIG S3
               // services.AddDefaultAWSOptions(Configuration.GetAWSOptions());

                //services.AddAWSService<IAmazonS3>();




               services.AddSwaggerGen(c =>
            {
                
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme() { In = "header", Description = "Please insert JWT with Bearer into field", Name = "Authorization", Type = "apiKey" });
                 
               // c.AddSecurityDefinition("Bearer", new ApiKeyScheme() { In = "header", Description = "Please insert JWT with Bearer into field", Name = "Authorization", Type = "apiKey" });
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Version = "v 2.0",
                        Title = "ELECTROHUILA WEB SERVICE - API",
                        Description = "A  API for app ELECTROHUILA",
                        TermsOfService = "Some terms ..."
                    }
                     

                );
             
            });

           
           services.AddLogging();
       //services.AddSwaggerGen();
        services.AddAutoMapper(); 
          //.AddApiExplorer();
             // Add service and create Policy with options 
           services.AddCors(options => { options.AddPolicy("CorsPolicy", 
                                      builder => builder.AllowAnyOrigin() 
                                                        .AllowAnyMethod() 
                                                        .AllowAnyHeader() 
                                                        .AllowCredentials()); 
                                  }); 

          //  services.AddScoped<IDataAccessProvider, DataAccessPostgreSqlProvider.DataAccessPostgreSqlProvider>();
             //configuracion de objetos
             services.AddOptions();
             services.AddDataAccess<MyAppContext>();
              var configurationSection = Configuration.GetSection("ConnectionStrings:DataAccessPostgreSqlProvider").Value;
           // var configureOptions = services.BuildServiceProvider().GetRequiredService<IConfigureOptions<ConnectionStrings>>();
             services.AddDbContext<MyAppContext>(options =>
             {
                options.UseOpenIddict();
                options.UseNpgsql(
                configurationSection,
                    b => b.MigrationsAssembly("Electrocore")
                );
            });
          
            
          services.AddOpenIddict(options =>
        {
            // Register the Entity Framework stores.
            options.AddEntityFrameworkCoreStores<MyAppContext>();
            // Register the ASP.NET Core MVC binder used by OpenIddict.
            // Note: if you don't call this method, you won't be able to
            // bind OpenIdConnectRequest or OpenIdConnectResponse parameters.
            options.AddMvcBinders();
            // Enable the token endpoint.
            options.EnableTokenEndpoint("/connect/token");
            // Enable the password flow.
            options.AllowPasswordFlow();
            // During development, you can disable the HTTPS requirement.
            options.DisableHttpsRequirement();
        });
        // Register the validation handler, that is used to decrypt the tokens.
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = OAuthValidationDefaults.AuthenticationScheme;
        })
        .AddOAuthValidation();


         services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

         //aqui agregar las implmentaciones de las interfaces
            // services.AddTransient<IUserRepository, UserRepository>(); 
           // services.AddTransient<IUserTypeRepository, UserTypeRepository>(); 
             //services.AddTransient<IlecturasRepository, LecturasRepository>(); 
             //services.AddTransient<IMedidorRepository, MedidorRepository>(); 
            
            //Empresa
             services.AddTransient<IEmpresaRepository, EmpresaRepository>(); 
             services.AddTransient<IDispositivoRepository, DispositivoRepository>();

             //Ciudad Departamento
             services.AddTransient<ICiudadRepository, CiudadRepository>();   
             services.AddTransient<IDepartamentoRepository, DepartamentoRepository>();
             
             //Usuarios y Proyectos
            services.AddTransient<ITipoUsuarioRepository, TipoUsuarioRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IProyectoUsuarioRepository, ProyectoUsuarioRepository>();
            services.AddTransient<IProyectoRepository, ProyectoRepository>();
            services.AddTransient<IProyectoEmpresaRepository, ProyectoEmpresaRepository>();
            
            //Cables
            services.AddTransient<ICableRepository, CableRepository>();
            services.AddTransient<ITipoCableRepository, TipoCableRepository>(); 
            services.AddTransient<IDetalleTipoCableRepository, DetalleTipoCableRepository>(); 
            //Novedad repository
            services.AddTransient<ITipoNovedadRepository, TipoNovedadRepository>();
            services.AddTransient<IDetalleTipoNovedadRepository, DetalleTipoNovedadRepository>(); 

            //Async
            services.AddTransient<IMaterialRepository, MaterialRepository>();
            services.AddTransient<IEstadoRepository, EstadoRepository>();
            services.AddTransient<ITipo_PerdidaRepository, Tipo_PerdidaRepository>();
            services.AddTransient<INivelTensionElementoRepository, NivelTensionElementoRepository>();
            services.AddTransient<ILongitudElementoRepository, LongitudElementoRepository>();

            //Equipos
            services.AddTransient<ITipoEquipoRepository, TipoEquipoRepository>(); 

            //AsynPost
            services.AddTransient<IElementoRepository, ElementoRepository>(); 
            services.AddTransient<IElementoCableRepository, ElementoCableRepository>(); 
            services.AddTransient<IEquipoElementoRepository, EquipoElementoRepository>(); 
            services.AddTransient<ILocalizacionElementoRepository, LocalizacionElementoRepository>(); 
            services.AddTransient<INovedadRepository, NovedadRepository>(); 
            services.AddTransient<IFotoRepository, FotoRepository>(); 
            services.AddTransient<IPerdidaRepository, PerdidaRepository>(); 

            //Ciudad Empresa
            services.AddTransient<ICiudad_EmpresaRepository, Ciudad_EmpresaRepository>(); 

            //agregar todo las interaces y sus implementacions
             
             
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            
            try
            {
               var context = serviceProvider.GetRequiredService<MyAppContext>();
              /// DbInitializer.Initialize(context);
           
           
            }
            catch (Exception ex)
            {
                   throw ex;
               
            }
           
             services.Configure<Settings>(options =>
                {
                    
                    options.ConnectionString = Configuration.GetSection("MongoConnection:ConnectionString").Value;
                    options.Database = Configuration.GetSection("MongoConnection:Database").Value;
                }); 

               


        
            
               services.AddMvc(); 
               //CONFIG S3 
               //services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
               // services.AddAWSService<IAmazonS3>();  
              /* services.AddDotVVM(options =>
                {
                    options.AddDefaultTempStorages("Temp");
                   
                });*/ 
    
           //  services.AddBusinessPackConfiguration();

        }
       
 
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
             app.UseAuthentication();
             app.UseCors("CorsPolicy"); 
             app.UseMvcWithDefaultRoute();
            // Enable middleware to serve generated Swagger as a JSON endpoint.
    
   // app.UseStaticFiles();
            app.UseDeveloperExceptionPage();

           app.UseSwagger();
        
  
            app.UseSwaggerUI(c =>
            {
                
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Electro");
               // c.InjectOnCompleteJavaScript("/swagger-ui/basic-auth.js"); 
                 c.InjectOnCompleteJavaScript("/swagger-ui/authorization1.js");
               // c.InjectOnCompleteJavaScript("https://cdnjs.cloudflare.com/ajax/libs/crypto-js/3.1.9-1/crypto-js.min.js"); // https://cdnjs.com/libraries/crypto-js
                // c.InjectOnCompleteJavaScript("/swagger-ui/authorization2.js");
            });
            app.UseStaticFiles();
            app.Run(async (context) =>  context.Response.Redirect("/swagger"));
            // app.UseStaticFiles();
            
         
           
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

           // app.UseTokenProvider(_tokenProviderOptions);
           

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           // app.UseDotVVM<DotvvmStartup>(env.ContentRootPath);
           // app.UseMvc();
        }
    }
}
