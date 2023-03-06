// See https://aka.ms/new-console-template for more information
using DronePackageDelivery;
using DronePackageDelivery.Service;
using Microsoft.Extensions.DependencyInjection;
using DronePackageDelivery.Common;

var builder = new ServiceCollection()
    .AddSingleton<IDataSource, DataSource>()
    .AddSingleton<ITripService, TripService>()
    .AddScoped<IDeliveryService, DeliveryService>()
    .BuildServiceProvider();

string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Data\File1.txt");
var service = builder.GetService<IDeliveryService>();
var result = service.Execute(filePath);

Console.WriteLine(result);
Console.WriteLine("Press any key to continue");
Console.ReadKey();
