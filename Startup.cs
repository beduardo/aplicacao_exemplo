using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aplicacao_exemplo.Dados;
using Bogus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace aplicacao_exemplo
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ContextoTreinamento>(opt =>
            {
                opt.UseNpgsql(Configuration.GetConnectionString("ContextoTreinamento"));
            });

            services.AddSwaggerGen();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            ConfiguraBanco(app);
        }

        public void ConfiguraBanco(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var databaseContext = serviceScope.ServiceProvider.GetRequiredService<ContextoTreinamento>();

                databaseContext.Database.Migrate();

                if (!databaseContext.Produtos.Any())
                {
                    var faker = new Faker();

                    for (var p = 0; p < 30; p++)
                    {
                        var novoProduto = new Produto
                        {
                            Nome = faker.Commerce.ProductName(),
                            Descricao = faker.Commerce.ProductDescription(),
                            Material = faker.Commerce.ProductMaterial(),
                            Preco = faker.Commerce.Price(),
                            Color = faker.Commerce.Color()
                        };

                        databaseContext.Produtos.Add(novoProduto);

                    }

                    databaseContext.SaveChanges();

                }
            }
        }
        
        
    }
}