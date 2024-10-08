Test repo to replicate error on MPU6050

MPU Version;

Adafruit MPU-6050 6-DoF Accel and Gyro Sensor - STEMMA QT Qwiic × 1 - connected to board through STEMMA

https://thepihut.com/products/adafruit-mpu-6050-6-dof-accel-and-gyro-sensor-stemma-qt-qwiic 

Reporpduced on Raspberry Pi 2 running;

PRETTY_NAME="Raspbian GNU/Linux 12 (bookworm)"
NAME="Raspbian GNU/Linux"
VERSION_ID="12"
VERSION="12 (bookworm)"
VERSION_CODENAME=bookworm
ID=raspbian
ID_LIKE=debian
HOME_URL="http://www.raspbian.org/"
SUPPORT_URL="http://www.raspbian.org/RaspbianForums"
BUG_REPORT_URL="http://www.raspbian.org/RaspbianBugs"

Kernal;

Linux raspberrypi 6.6.47+rpt-rpi-v7 #1 SMP Raspbian 1:6.6.47-1+rpt1 (2024-09-02) armv7l GNU/Linux

System Arch;

armv7l

i2cdetect -y 1

     0  1  2  3  4  5  6  7  8  9  a  b  c  d  e  f
00:                         -- -- -- -- -- -- -- --
10: -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --
20: -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --
30: -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --
40: -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --
50: -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --
60: -- -- -- -- -- -- -- -- 68 -- -- -- -- -- -- --
70: -- -- -- -- -- -- -- --

