using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.DataBase;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using LojaVirtual.Repositories.Interfaces;
using LojaVirtual.Repositories;
using LojaVirtual.Libraries.Sessao;
using LojaVirtual.Libraries.Login;
using System.Net.Mail;
using System.Net;
using LojaVirtual.Libraries.Email;
using LojaVirtual.Libraries.Middleware;
using LojaVirtual.Libraries.CarrinhoCompra;
using AutoMapper;
using LojaVirtual.Libraries.AutoMapper;
using LojaVirtual.Libraries.Gerenciador.Frete;
using WSCorreios;

namespace LojaVirtual
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
            var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);


            services.AddHttpContextAccessor();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<INewsletterRepository, NewsletterRepository>();
            services.AddScoped<IColaboradorRepository, ColaboradorRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IImagemRepository, ImagemRepository>();
           

            //SMTP
            services.AddScoped<SmtpClient>(Options=> {
                SmtpClient smtp = new SmtpClient()
                {
                    Host = Configuration.GetValue<string>("Email:ServerSMTP"),
                    Port = Configuration.GetValue<int>("Email:ServerPort"),
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(Configuration.GetValue<string>("Email:UserName"), Configuration.GetValue<string>("Email:PassWord")),
                    EnableSsl = true

                };
                return smtp;
            });
            services.AddScoped<CalcPrecoPrazoWSSoap>(options => {
                var servico = new CalcPrecoPrazoWSSoapClient(CalcPrecoPrazoWSSoapClient.EndpointConfiguration.CalcPrecoPrazoWSSoap);
                return servico;
            });
            services.AddScoped<GerenciarEmail>();
            services.AddScoped<LojaVirtual.Libraries.Cookie.Cookie>();
            services.AddScoped<CarrinhoCompra>();
            services.AddScoped<WSCorreiosCalcularFrete>();





            //Session configuração
            services.AddMemoryCache();//guardar os dados na memória
            services.AddSession(options =>
            {
            });
            services.AddScoped<Sessao>();
            services.AddScoped<LojaVirtual.Libraries.Cookie.Cookie>();
            services.AddScoped<LoginCliente>();
            services.AddScoped<LoginColaborador>();

            services.AddControllersWithViews();
            string connection = @"Data Source=DESKTOP-TDL5LR3\SQLEXPRESS;Initial Catalog=LojaVirtual;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            services.AddDbContext<LojaVirtualContext>(options => options.UseSqlServer(connection));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseRouting();
            app.UseMiddleware<ValidateAntiForgeryTokenMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                     "areas", "colaborador", "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                      );

                endpoints.MapControllerRoute(

                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");

                



            });
        }
    }
}
