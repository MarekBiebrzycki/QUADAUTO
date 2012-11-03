

#define	MOTOR0PIN  D28 
#define	MOTOR1PIN  D27  
#define	MOTOR2PIN  D11 
#define	MOTOR3PIN  D12 
#define MOTOR4PIN   D24  
#define MOTOR5PIN   D14  


uint16 MotorData[6] = {0,0,0,0,0,0};


void motorCcontrol(void)    
{
  uint16 PWMData[6] = {0,0,0,0,0,0};
  uint8 i;
  for(i=0;i<6;i++)
  {
    if(MotorData[i] <= 0)  PWMData[i] = 0;  
      else if(MotorData[i]  >= 1000) PWMData[i] = 50000;  
        else  PWMData[i] = (1000 + MotorData[i])*24;
  }     

  pwmWrite(MOTOR0PIN,PWMData[0] );
  pwmWrite(MOTOR1PIN,PWMData[1] );
  pwmWrite(MOTOR2PIN,PWMData[2] );
  pwmWrite(MOTOR3PIN,PWMData[3] );
  pwmWrite(MOTOR4PIN,PWMData[4] );
  pwmWrite(MOTOR5PIN,PWMData[5] );
}


void motorInit(void)   
{
  
  pinMode(MOTOR0PIN, PWM);
  pinMode(MOTOR1PIN, PWM);
  pinMode(MOTOR2PIN, PWM);
  pinMode(MOTOR3PIN, PWM);
  pinMode(MOTOR4PIN, PWM);
  pinMode(MOTOR5PIN, PWM);
  Timer3.setPeriod(2080);  
  Timer4.setPeriod(2080);  
  MotorData[0] = 0;
  MotorData[1] = 0;
  MotorData[2] = 0;
  MotorData[3] = 0;
  MotorData[4] = 0;
  MotorData[5] = 0; 
  motorCcontrol();   
 }

