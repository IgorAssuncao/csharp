using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using TP3_Auth.Context;
using TP3_Auth.Repository;

namespace TP3_Auth
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            key = Encoding.UTF8.GetBytes(Configuration["Token:Secret"]);
        }

        public IConfiguration Configuration { get; }
        private byte[] key { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Bearer";
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters.ValidIssuer = "TP3-AUTH-API";
                options.TokenValidationParameters.ValidAudience = "TP3-AUTH-API";
                options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(key);
            });

            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("TP3-Auth-Local"))
            );

            services.AddScoped<ApplicationContext, ApplicationContext>();
            services.AddScoped<IFriendRepository, FriendRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
