using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using MPU6050Test;
using MPU6050Test.Service;
using MPUTest6050.Service.Interface;
using System.IO;

// Load configuration from appSettings.json
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
    .Build();

var serviceCollection = new ServiceCollection();
ConfigureServices(serviceCollection, configuration);

var serviceProvider = serviceCollection.BuildServiceProvider();

try
{
    Console.WriteLine("Starting calibration...");

    // Get the accelerometer controller service
    var accelerometerController = serviceProvider.GetService<IAccelerometerController>();

    // Perform the calibration (replace this with your actual method)
    await accelerometerController.CalibrateAsync();
  

    Console.WriteLine("Calibration completed successfully.");
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
    Console.WriteLine(ex.StackTrace);
}

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    // Configure logging
    services.AddLogging(configure => configure.AddConsole());


    var accelerometerSettings = new Accelerometer();
    configuration.GetSection("Accelerometer").Bind(accelerometerSettings);


    // Register IAccelerometerController (for Linux or mock, depending on platform)
    //if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
    //{
    services.AddSingleton<IAccelerometerController, ImuAccelerometerController>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<ImuAccelerometerController>>();
            return new ImuAccelerometerController(
                    logger,
                    accelerometerSettings.Address,
                    accelerometerSettings.RecalibrationIntervalInMinutes); // I2C address and recalibration interval
        });
    //}
}
