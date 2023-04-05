using Microsoft.Extensions.Hosting;
using Npgsql;

namespace Discount.Grpc.Extentions;

public static class HostExtensions
{
    public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0)
    {
        int retryForAvailability = retry.Value;
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            IConfiguration configuration = services.GetRequiredService<IConfiguration>();

            ILogger logger = services.GetRequiredService<ILogger<TContext>>();

            #region Migrate Database
            try
            {
                logger.LogInformation("migrating postgresql database");

                using var connection = new NpgsqlConnection
                    (configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
                connection.Open();

                using var command = new NpgsqlCommand
                {
                    Connection = connection
                };

                command.CommandText = "DROP TABLE IF EXISTS Coupon";
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY,
                    ProductName VARCHAR(200) NOT NULL ,
                    Description TEXT ,
                    Amount INT)";

                command.ExecuteNonQuery();

                #region Seed Data
                command.CommandText =
                    "INSERT INTO Coupon(ProductName,Description,Amount) VALUES ('IPhone 14 Pro Max','Best Phone in 2023',150);";
                command.ExecuteNonQuery();

                command.CommandText =
                    "INSERT INTO Coupon(ProductName,Description,Amount) VALUES ('IPhone 14 Pro','Best Phone in 2023',100);";
                command.ExecuteNonQuery();
                #endregion

                logger.LogInformation("Migration has been completed...");
            }
            catch (NpgsqlException ex)
            {
                logger.LogError("an error has been occurred");
                if (retryForAvailability < 50)
                {
                    retryForAvailability++;
                    Thread.Sleep(2000);
                    MigrateDatabase<TContext>(host, retryForAvailability);
                }
                throw;
            }
            #endregion

            return host;
        }
    }
}