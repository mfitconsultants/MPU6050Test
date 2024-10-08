using Iot.Device.Imu;
using Microsoft.Extensions.Logging;
using MPUTest6050.Service.Interface;
using System.Device.I2c;

namespace MPU6050Test.Service
{
    public class ImuAccelerometerController : IAccelerometerController, IDisposable
    {
        private readonly Mpu6050 _mpu6050;
        private readonly ILogger<ImuAccelerometerController> _logger;
        private readonly int _recalibrationIntervalMinutes;
        private DateTime _lastCalibrationTime;
        private const float DriftThreshold = 0.1f;  // Threshold for detecting drift in G (acceleration)

        public ImuAccelerometerController(
            ILogger<ImuAccelerometerController> logger,
            int i2cAddress,
            int recalibrationIntervalMinutes,
            int i2cBusId = 1)
        {
            _logger = logger;

            try
            {
                var i2cSettings = new I2cConnectionSettings(i2cBusId, i2cAddress);
                var i2cDevice = I2cDevice.Create(i2cSettings);

                
                _mpu6050 = new Mpu6050(i2cDevice);  // Initialize the MPU6050

                _logger.LogInformation($"MPU6050 initialized with I2C address {i2cAddress} on bus {i2cBusId}.");

                try
                {
                    _logger.LogInformation($"Gyroscope bandwidth is set to {_mpu6050.GyroscopeBandwidth}");
                    _logger.LogInformation($"Attempting to set the gyroscope bandwidth to Bandwidth0184Hz.");
                    _mpu6050.GyroscopeBandwidth = GyroscopeBandwidth.Bandwidth0184Hz;
                    _logger.LogInformation($"Gyroscope bandwidth successfully set.");

                    _logger.LogInformation($"Attempting to set the accelerometer bandwidth to Bandwidth0184Hz.");
                    _mpu6050.AccelerometerBandwidth = AccelerometerBandwidth.Bandwidth0184Hz;
                    _logger.LogInformation($"Accelerometer bandwidth successfully set.");
                }
                catch (IOException ex)
                {
                    _logger.LogError(ex, $"Failed to set the bandwidth. Exception: {ex.Message}");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to initialize MPU6050 with I2C address {i2cAddress} on bus {i2cBusId}. Check connections and configuration.");
                //throw new InvalidOperationException("MPU6050 initialization failed. Ensure the device is connected and configured correctly.", ex);
            }

            _recalibrationIntervalMinutes = recalibrationIntervalMinutes;
            _lastCalibrationTime = DateTime.Now;
        }

        /// <summary>
        /// Calibrate the accelerometer and gyroscope.
        /// </summary>
        public async Task CalibrateAsync()
        {
            if (_mpu6050 == null)
            {
                _logger.LogError("Cannot calibrate MPU6050 because it is not initialized.");
                throw new InvalidOperationException("MPU6050 is not initialized.");
            }

            await Task.Run(() =>
            {
                _logger.LogInformation("Calibrating MPU6050...");
                try
                {
                    //going to try and get values 
                    _logger.LogInformation($"Reading MPU values - start");
                    _logger.LogInformation($"MPU6050: Accelerometer X:{_mpu6050.GetAccelerometer().X}, Y:{_mpu6050.GetAccelerometer().Y}, Z:{_mpu6050.GetAccelerometer().Z}");
                    _logger.LogInformation($"MPU6050: Gyroscope X:{_mpu6050.GetGyroscopeReading().X}, Y:{_mpu6050.GetAccelerometer().Y}, Z:{_mpu6050.GetAccelerometer().Z}");
                    _logger.LogInformation($"Reading MPU values - end");


                    _logger.LogInformation($"Gyroscope bandwidth is set to {_mpu6050.GyroscopeBandwidth}");
                    _logger.LogInformation($"Attempting to set the gyroscope bandwidth to Bandwidth0184Hz.");
                    _mpu6050.GyroscopeBandwidth = GyroscopeBandwidth.Bandwidth0184Hz;
                    _logger.LogInformation($"Gyroscope bandwidth now set to {_mpu6050.GyroscopeBandwidth}");

                    //comment out calibration for testting
                    _mpu6050.CalibrateGyroscopeAccelerometer();
                    _logger.LogInformation("Calibration complete.");
                }
                catch (IOException ex)
                {
                    _logger.LogInformation($"Gyroscope bandwidth in the error is {_mpu6050.GyroscopeBandwidth}");
                    _logger.LogError(ex, "Failed to set the GyroscopeBandwidth. Proceeding with default bandwidth.");
                    _logger.LogError(ex.StackTrace);
                }
            });
        }

        public void Dispose()
        {
            _mpu6050?.Dispose();
        }
    }
}
