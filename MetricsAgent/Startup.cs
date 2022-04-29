using AutoMapper;
using Dapper;
using MetricsAgent.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data.SQLite;
namespace MetricsAgent
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        private const string ConnectionString = "DataSource=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        // This method gets called by the runtime. Use this method to addservices to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);

            services.AddTransient<INotifier, Notifier1>();
            services.AddMvc().AddNewtonsoftJson();
            services.AddControllers();
            //ConfigureSqlLiteConnection(services);
            services.AddScoped<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddScoped<IDotnetMetricsRepository, DotnetMetricsRepository>();
            services.AddScoped<IRamMetricsRepository, RamMetricsRepository>();
            services.AddScoped<IHddMetricsRepository, HddMetricsRepository>();
            services.AddScoped<INetworkMetricsRepository, NetworkMetricsRepository>();
        }
        private void ConfigureSqlLiteConnection(IServiceCollection services)
        {
            /*const string ConnectionString = "DataSource = metrics.db; Version = 3; Pooling = true; Max Pool Size = 100; ";
            var connection = new SQLiteConnection(ConnectionString);
            connection.Open();*/
            PrepareSchema();
        }
        private void PrepareSchema()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("DROP TABLE IF EXISTS cpumetrics");
                connection.Execute("DROP TABLE IF EXISTS rammetrics");
                connection.Execute("DROP TABLE IF EXISTS hddmetrics");
                connection.Execute("DROP TABLE IF EXISTS networkmetrics");
                connection.Execute("DROP TABLE IF EXISTS dotnetmetrics");
                connection.Execute("CREATE TABLE cpumetrics(id INTEGER PRIMARY KEY, " +
                    "value INT, time REAL)");
                connection.Execute("CREATE TABLE rammetrics(id INTEGER PRIMARY KEY, " +
                    "value INT, time REAL)");
                connection.Execute("CREATE TABLE hddmetrics(id INTEGER PRIMARY KEY, " +
                    "value INT, time REAL)");
                connection.Execute("CREATE TABLE networkmetrics(id INTEGER PRIMARY KEY, " +
                    "value INT, time REAL)");
                connection.Execute("CREATE TABLE dotnetmetrics(id INTEGER PRIMARY KEY, " +
                    "value INT, time REAL)");
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
