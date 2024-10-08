using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MPUTest6050.Service.Interface
{
    public interface IAccelerometerController
    {
        /// <summary>
        /// Calibrates the gyroscope and accelerometer.
        /// </summary>
        /// <returns>Task representing the asynchronous calibration operation.</returns>
        Task CalibrateAsync();

    }
}
