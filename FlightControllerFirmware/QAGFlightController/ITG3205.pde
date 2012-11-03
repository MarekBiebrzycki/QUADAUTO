
#define GYRO 0x68 
#define G_SMPLRT_DIV 0x15  
#define G_DLPF_FS 0x16     
#define G_INT_CFG 0x17    
#define G_PWR_MGM 0x3E     

#define G_TO_READ 8 

int16 g_offx = 0;
int16 g_offy = 0;
int16 g_offz = 0;


void initGyro(void)
{
 
  writeTo(GYRO, G_PWR_MGM, 0x00);
  writeTo(GYRO, G_SMPLRT_DIV, 0x07); // EB, 50, 80, 7F, DE, 23, 20, FF
  writeTo(GYRO, G_DLPF_FS, 0x1E); // +/- 2000 dgrs/sec, 1KHz, 1E, 19
  writeTo(GYRO, G_INT_CFG, 0x00);
}

void getGyroscopeData(int16 * result)
{
  

  uint8 regAddress = 0x1B;
  int16 temp, x, y, z;
  uint8 buff[G_TO_READ];

  readFrom(GYRO, regAddress, G_TO_READ, buff); 

  result[0] = (((int16)buff[2] << 8) | buff[3]) + g_offx;
  result[1] = (((int16)buff[4] << 8) | buff[5]) + g_offy;
  result[2] = (((int16)buff[6] << 8) | buff[7]) + g_offz;
  result[3] = ((int16)buff[0] << 8) | buff[1]; 
}
