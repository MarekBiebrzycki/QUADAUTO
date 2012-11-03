#define ACC (0x53)    
#define A_TO_READ (6) 
#define XL345_DEVID   0xE5 

#define ADXLREG_BW_RATE      0x2C
#define ADXLREG_POWER_CTL    0x2D
#define ADXLREG_DATA_FORMAT  0x31
#define ADXLREG_FIFO_CTL     0x38
#define ADXLREG_BW_RATE      0x2C
#define ADXLREG_TAP_AXES     0x2A
#define ADXLREG_DUR          0x21


#define ADXLREG_DEVID        0x00
#define ADXLREG_DATAX0       0x32
#define ADXLREG_DATAX1       0x33
#define ADXLREG_DATAY0       0x34
#define ADXLREG_DATAY1       0x35
#define ADXLREG_DATAZ0       0x36
#define ADXLREG_DATAZ1       0x37


int16 a_offx = 0;
int16 a_offy = 0;
int16 a_offz = 0;

void writeTo(uint8 DEVICE, uint8 address, uint8 val) 
{
  // all i2c transactions send and receive arrays of i2c_msg objects 
  i2c_msg msgs[1]; // we dont do any bursting here, so we only need one i2c_msg object
 uint8 msg_data[2];
  
  msg_data = {address,val};  
  msgs[0].addr = DEVICE;
  msgs[0].flags = 0; 
  msgs[0].length = 2; 
  msgs[0].data = msg_data;
  i2c_master_xfer(I2C1, msgs, 1,0);  
}

void readFrom(uint8 DEVICE, uint8 address, uint8 num, uint8 *msg_data) 
{
  i2c_msg msgs[1]; 
  msg_data[0] = address;
  
  msgs[0].addr = DEVICE;
  msgs[0].flags = 0; 
  msgs[0].length = 1; // just one byte for the address to read, 0x00
  msgs[0].data = msg_data;
  i2c_master_xfer(I2C1, msgs, 1,0);
  
  msgs[0].addr = DEVICE;
  msgs[0].flags = I2C_MSG_READ; 
  msgs[0].length = num; 
  msgs[0].data = msg_data;
  i2c_master_xfer(I2C1, msgs, 1,0);
}
void initAcc(void) 
{
    //all i2c transactions send and receive arrays of i2c_msg objects 
  i2c_msg msgs[1]; // we dont do any bursting here, so we only need one i2c_msg object
  uint8 msg_data[2];
  msg_data = {0x00,0x00};
  msgs[0].addr = ACC;
  msgs[0].flags = 0; 
  msgs[0].length = 1; // just one byte for the address to read, 0x00
  msgs[0].data = msg_data;
  
  i2c_master_xfer(I2C1, msgs, 1,0);
  msgs[0].addr = ACC;
  msgs[0].flags = I2C_MSG_READ; 
  msgs[0].length = 1; // just one byte for the address to read, 0x00
  msgs[0].data = msg_data;
  i2c_master_xfer(I2C1, msgs, 1,0);
  // now we check msg_data for our 0xE5 magic number 
  uint8 dev_id = msg_data[0];
  //SerialUSB.print("Read device ID from xl345: ");
  //SerialUSB.println(dev_id,HEX);
  
  if (dev_id != XL345_DEVID) 
  {
    SerialUSB.println("Error, incorrect xl345 devid!");
    SerialUSB.println("Halting program, hit reset...");
    waitForButtonPress(0);
  }

  writeTo(ACC, ADXLREG_POWER_CTL, 0x00);  
  writeTo(ACC, ADXLREG_POWER_CTL, 0xff);
  writeTo(ACC, ADXLREG_POWER_CTL, 0x08); 
  
}
void getAccelerometerData(int16 * result) 
{
  int16 regAddress = ADXLREG_DATAX0;    
  uint8 buff[A_TO_READ];

  readFrom(ACC, regAddress, A_TO_READ, buff); 

  
  result[0] = (((int16)buff[1]) << 8) | buff[0] + a_offx;   
  result[1] = (((int16)buff[3]) << 8) | buff[2] + a_offy;
  result[2] = (((int16)buff[5]) << 8) | buff[4] + a_offz;
}



