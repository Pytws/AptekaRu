using System.Data;
using Npgsql;
using AptekaRu.DAL.Interfaces;
using AptekaRu.DAL.Services.RenderTableService;
using AptekaRu.DAL.Repositories;
using AptekaRu.DAL.Extensions;
using System.Reflection;
using System.Text.Json.Serialization;
using AptekaRu.Web.CustomJsonConverter;

namespace AptekaRu.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            var connectionString = ( builder.Environment.IsProduction() ?
                builder.Configuration.GetValue<string>("CONNECTIONS_STRING") :
                builder.Configuration.GetConnectionString("DefaultConnection") ) ??
                throw new NullReferenceException();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Configuration.AddUserSecrets<Program>().Build();

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            if (args.Length != 0)
            {
                DapperMappingExtensions.AddDapperTypeMappings<JsonPropertyNameAttribute>(
                Assembly.LoadFile(args[0]),
                "Name");
            }
            else throw new InvalidOperationException("Pass the absolute path to the DAL layer to the assembly (dll)");

            builder.Services.AddControllers()
                            .AddJsonOptions(options =>
                            {
                                options.JsonSerializerOptions.Converters.Add(new CustomBooleanJsonConverter());
                            });

            builder.Services.AddTransient<IAptekaruRepository, AptekaruRepository>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<AptekaruRepository>>();
                return new AptekaruRepository(connectionString, logger);
            });

            builder.Services.AddTransient<IRenderTable, RenderTable>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<RenderTable>>();
                return new RenderTable(connectionString, logger);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                //app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Data}/{action=Index}");

            app.Run();
        }
    }
}
