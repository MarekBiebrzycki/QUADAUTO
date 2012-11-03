#include <TinyGPS.h>

#include <EasyTransfer.h>
#include <stdlib.h>
#include <stdio.h>
#include "wirish.h"
#include "i2c.h"

extern uint16 MotorData[6];  

extern volatile unsigned int chan1PPM;  
extern volatile unsigned int chan2PPM;
extern volatile unsigned int chan3PPM;
extern volatile unsigned int chan4PPM;


char str[512]; 
TinyGPS gps;

void setup()
{
  motorInit();       
  capturePPMInit();  

  //configure I2C port 1 (pins 5, 9) with no special option flags (second argument)
  i2c_master_enable(I2C1, 0);  

  initAcc();           
  initGyro();          
  bmp085Calibration(); 
  compassInit(false);  
  //compassCalibrate(1);
  //commpassSetMode(0); 
}

void loop()
{

  int16 acc[3];
  getAccelerometerData(acc);  
  SerialUSB.print("Xacc=");
  SerialUSB.print(acc[0]);
  SerialUSB.print("    ");
  SerialUSB.print("Yacc=");  
  SerialUSB.print(acc[1]);
  SerialUSB.print("    ");
  SerialUSB.print("Zacc=");  
  SerialUSB.print(acc[2]);
  SerialUSB.print("    ");
  //delay(100);
  int16 gyro[4];
  getGyroscopeData(gyro);    

  SerialUSB.print("Xg=");
  SerialUSB.print(gyro[0]);
  SerialUSB.print("    ");
  SerialUSB.print("Yg=");  
  SerialUSB.print(gyro[1]);
  SerialUSB.print("    ");
  SerialUSB.print("Zg=");  
  SerialUSB.print(gyro[2]);
  SerialUSB.print("    ");

  CollissionCheck();
  RefreshNavigation();

  int16 temperature = 0;
  int32 pressure = 0;
  int32 centimeters = 0;
  temperature = bmp085GetTemperature(bmp085ReadUT());
  pressure = bmp085GetPressure(bmp085ReadUP());
  centimeters = bmp085GetAltitude(); 
  //SerialUSB.print("Temperature: ");
  SerialUSB.print(temperature, DEC);
  SerialUSB.print(" *0.1 deg C ");
  //SerialUSB.print("Pressure: ");
  SerialUSB.print(pressure, DEC);
  SerialUSB.print(" Pa ");
  SerialUSB.print("Altitude: ");
  SerialUSB.print(centimeters, DEC);
  SerialUSB.print(" cm ");
  //SerialUSB.println("    ");
  //  //delay(1000);
  //  float Heading;
  //  Heading = compassHeading();
  //SerialUSB.print("commpass: ");
  //  SerialUSB.print(Heading, DEC);
  //  SerialUSB.println(" degree");
  delay(100);
  MotorData[0] = 1;  
  MotorData[1] = 1;
  MotorData[2] = 1;
  MotorData[3] = 1;
  MotorData[4] = 1;
  MotorData[5] = 1; 
  motorCcontrol();   
  delay(1000); 
  MotorData[0] = 500; 
  MotorData[1] = 500;
  MotorData[2] = 500;
  MotorData[3] = 500;
  MotorData[4] = 500;
  MotorData[5] = 500; 
  motorCcontrol();   
  while(1);  
  SerialUSB.print("PPM Channel 1: ");
  SerialUSB.print(chan1PPM, DEC);
  SerialUSB.print("  ");  
  SerialUSB.print("PPM Channel 2: ");
  SerialUSB.print(chan2PPM, DEC);
  SerialUSB.print("  ");  
  SerialUSB.print("PPM Channel 3: ");
  SerialUSB.print(chan3PPM, DEC);
  SerialUSB.print("  ");  
  SerialUSB.print("PPM Channel 4: ");
  SerialUSB.print(chan4PPM, DEC);
  SerialUSB.println("  ");  
  delay(100);

}


