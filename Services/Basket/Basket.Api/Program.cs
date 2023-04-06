
using Basket.Api.GrpcServices;
using Basket.Api.Repositories;
using Discount.Grpc.Protos;

namespace Basket.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region Redis Config

            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
            });

            #endregion

            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.AddControllers();
            builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>
            (option =>
            {
                option.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]);
            });
            builder.Services.AddScoped<DiscountGrpcService>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}