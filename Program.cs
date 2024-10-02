using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.API.Helper;
using Store.API.MiddleWares;
using Store.Data.Context;
using Store.Repository.Interfaces;
using Store.Repository.Repostories;
using Store.Service.HandleResponses;
using Store.Service.Services.ProductService;
using Store.Service.Services.ProductService.Dtos;
using Store.Service.HandleResponses;
using Store.Service.Services.CacheService;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using Store.Repository.BasketRepository;
using Store.API.Extensions;
using Store.Service.Services.TokenService;
using Store.Service.Services.BasketService;
using Store.Service.Services.BasketService.Dtos;
using Store.Service.Services.UserService.Dtos;
using Store.Service.Services.PaymentService;
using Store.Service.Services.OrderServices;

namespace Store.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerDocumentation();
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            builder.Services.AddSingleton<IConnectionMultiplexer>(config =>
            {
                var configuration = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"));
                return ConnectionMultiplexer.Connect(configuration);
            });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ICacheService, cacheService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped< IPaymentService,PaymentService>();
            builder.Services.AddScoped<IorderService, orderService>();
            builder.Services.AddScoped<IBasketService, BasketService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped <IBasketRepository, BasketRepository>();

         builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                // Customize the behavior for model validation errors
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(e => e.Value.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    var result = new ValidationErrorResponse
                    {

                        Errors = errors
                    };

                    return new BadRequestObjectResult(result);
                };
            });

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddAutoMapper(typeof(ProductProfile));
            builder.Services.AddAutoMapper(typeof(BasketProfile));
            builder.Services.AddAutoMapper(typeof(OrderProfile));
            builder.Services.AddIdentityServices(builder.Configuration);
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", Policy =>
                {
                    Policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200", "https://localhost:7254");

                });

            });



            var app = builder.Build();
            await ApplySeeding.ApplySeedingAsync(app);
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<ExceptionMiddleWare>();
           
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("CorsPolicy");

            app.UseStaticFiles();
            app.MapControllers();

            app.Run();
        }
    }
}
