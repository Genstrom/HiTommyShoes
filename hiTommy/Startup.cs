using hiTommy.Data;
using hiTommy.Data.Repositories;
using hiTommy.Data.Services;
using hiTommy.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace hiTommy
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration.GetConnectionString("ShoeStoreConnectionString");
        }

        public string ConnectionString { get; set; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<HiTommyApplicationDbContext>(options => options.UseSqlServer(ConnectionString));
            //Configure the Services
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
                    options => Configuration.Bind("JwtSettings", options))
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                    options => Configuration.Bind("CookieSettings", options));
            services.AddTransient<ShoeServices>();
            services.AddTransient<OrderService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<CustomerService>();
            services.AddTransient<QuantityService>();
            services.AddSession();
            services.AddHttpContextAccessor();



            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "ShoeStore", Version = "v1"}); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShoeStore v1"));
            }

            app.UseHttpsRedirection();
            app.UseSession();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}