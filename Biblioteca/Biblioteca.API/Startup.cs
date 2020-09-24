using Biblioteca.Data;
using Biblioteca.Repositories;
using Biblioteca.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Biblioteca.API
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
            services.AddDbContext<BibliotecaContext>();

            services.AddScoped<BookRepository, BookRepository>();
            services.AddScoped<AuthorRepository, AuthorRepository>();
            services.AddScoped<AuthorBookRepository, AuthorBookRepository>();

            services.AddScoped<BookService, BookService>();
            services.AddScoped<AuthorService, AuthorService>();
            services.AddScoped<AuthorBookService, AuthorBookService>();
            services.AddScoped<AuthService, AuthService>();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Bearer";
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = false,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.ASCII.GetBytes("ultralongpassphrasethatshouldnotbehere"))
                    };
                });

            services.AddControllers();
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
