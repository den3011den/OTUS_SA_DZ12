using Microsoft.EntityFrameworkCore;
using OTUS_SA_DZ12_Business.Repository;
using OTUS_SA_DZ12_Business.Repository.IRepository;
using OTUS_SA_DZ12_Common;
using OTUS_SA_DZ12_DataAccess;
using OTUS_SA_DZ12_DataAccess.DbInitializer;
using OTUS_SA_DZ12_WebAPI.Controllers;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using System.Reflection.Metadata;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace OTUS_SA_DZ12_WebAPI
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

            SD.SqlCommandConnectionTimeout = int.Parse(Configuration.GetValue<string>("SqlCommandConnectionTimeout"));


            services.AddControllers().AddMvcOptions(x =>
                x.SuppressAsyncSuffixInActionNames = false);

            //services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();

            //services.AddAuthorization(options =>
            //{
            //    options.FallbackPolicy = options.DefaultPolicy;
            //});

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                // include API xml documentation
                var apiAssembly = typeof(CustomersController).Assembly;
                c.IncludeXmlComments(GetXmlDocumentationFileFor(apiAssembly));

                apiAssembly = typeof(DishesController).Assembly;
                c.IncludeXmlComments(GetXmlDocumentationFileFor(apiAssembly));

                apiAssembly = typeof(FeedbacksController).Assembly;
                c.IncludeXmlComments(GetXmlDocumentationFileFor(apiAssembly));

                apiAssembly = typeof(OrdersController).Assembly;
                c.IncludeXmlComments(GetXmlDocumentationFileFor(apiAssembly));

                apiAssembly = typeof(OrdersDishesController).Assembly;
                c.IncludeXmlComments(GetXmlDocumentationFileFor(apiAssembly));

                apiAssembly = typeof(ReceiveMethodsController).Assembly;
                c.IncludeXmlComments(GetXmlDocumentationFileFor(apiAssembly));

                apiAssembly = typeof(StatesController).Assembly;
                c.IncludeXmlComments(GetXmlDocumentationFileFor(apiAssembly));

                // include models xml documentation

                /*
                var modelsAssembly = typeof(Catalog_Models.CatalogModels.Author.AuthorItemCreateUpdateRequest).Assembly;
                c.IncludeXmlComments(GetXmlDocumentationFileFor(modelsAssembly));
                modelsAssembly = typeof(Catalog_Models.CatalogModels.Author.AuthorItemResponse).Assembly;
                c.IncludeXmlComments(GetXmlDocumentationFileFor(modelsAssembly));

                modelsAssembly = typeof(Catalog_Models.CatalogModels.Publisher.PublisherItemCreateUpdateRequest).Assembly;
                c.IncludeXmlComments(GetXmlDocumentationFileFor(modelsAssembly));
                modelsAssembly = typeof(Catalog_Models.CatalogModels.Publisher.PublisherItemResponse).Assembly;
                c.IncludeXmlComments(GetXmlDocumentationFileFor(modelsAssembly));

                modelsAssembly = typeof(Catalog_Models.CatalogModels.Book.AuthorForBookRequest).Assembly;
                c.IncludeXmlComments(GetXmlDocumentationFileFor(modelsAssembly));
                modelsAssembly = typeof(Catalog_Models.CatalogModels.Book.BookItemResponse).Assembly;
                c.IncludeXmlComments(GetXmlDocumentationFileFor(modelsAssembly));
                modelsAssembly = typeof(Catalog_Models.CatalogModels.Book.BookItemCreateUpdateRequest).Assembly;
                c.IncludeXmlComments(GetXmlDocumentationFileFor(modelsAssembly));

                modelsAssembly = typeof(Catalog_Models.CatalogModels.BookToAuthor.BookToAuthorResponse).Assembly;
                c.IncludeXmlComments(GetXmlDocumentationFileFor(modelsAssembly));

                modelsAssembly = typeof(Catalog_Models.CatalogModels.BookInstance.BookInstanceCreateUpdateRequest).Assembly;
                c.IncludeXmlComments(GetXmlDocumentationFileFor(modelsAssembly));
                modelsAssembly = typeof(Catalog_Models.CatalogModels.BookInstance.BookInstanceResponse).Assembly;
                c.IncludeXmlComments(GetXmlDocumentationFileFor(modelsAssembly));
                */

                //c.OperationFilter<ReApplyOptionalRouteParameterOperationFilter>();
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Restaurant service API (Robots)", Version = "v1" });

            });

            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IDishRepository, DishRepository>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderDishRepository, OrderDishRepository>();
            services.AddScoped<IReceiveMethodRepository, ReceiveMethodRepository>();
            services.AddScoped<IStateRepository, StateRepository>();


            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(AssemblyReference).Assembly));

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("RobotsPostgresSQLConnection"),
                u => u.CommandTimeout(SD.SqlCommandConnectionTimeout));
                options.UseLazyLoadingProxies();
            });


            services.AddOpenApiDocument(options =>
            {
                options.Title = "Restaurant service API (Robots)";
                options.Version = "1.0";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbInitializer dbInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                //app.UseSwagger(c =>
                //{
                //    c.PreSerializeFilters.Add((swagger, httpReq) =>
                //    {
                //        swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"https://{httpReq.Host.Host}" } };
                //    });
                //});

                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseOpenApi();

            app.UseSwaggerUI(x =>
            {
                x.DocExpansion(DocExpansion.List);
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthentication();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            dbInitializer.InitializeDb();
        }

        private static string GetXmlDocumentationFileFor(Assembly assembly)
        {
            var documentationFile = $"{assembly.GetName().Name}.xml";
            var path = Path.Combine(AppContext.BaseDirectory, documentationFile);

            return path;
        }
    }
}
