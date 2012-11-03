
#define	PPM_MAX		1000
#define PPM_MIN		-125
#define PPM_HIGH	100
#define PPM_LOW		-100
#define PPM_SIG_HIGH	2000
#define PPM_SIG_NEU	1500
#define PPM_SIG_LOW	1000


#define	STICKGATE	 70	// Stick move gate when setting 
#define SOFT_IDLE	 0X02
#define SOFT_EXP	 0X01
#define SOFT_FLT	 0X00

#define AXIS_CROSS	 0X00	
#define AXIS_X		 0XFF	

#define Channel1Pin  D31  
#define Channel2Pin  D32
#define Channel3Pin  D33
#define Channel4Pin  D34


volatile unsigned int chan1begin = 0;  
volatile unsigned int chan1end   = 0;  
volatile unsigned int chan1PPM  = 0;  

volatile unsigned int chan2begin = 0;
volatile unsigned int chan2end   = 0;
volatile unsigned int chan2PPM  = 0;  

volatile unsigned int chan3begin = 0;
volatile unsigned int chan3end   = 0;
volatile unsigned int chan3PPM  = 0;  

volatile unsigned int chan4begin = 0;
volatile unsigned int chan4end   = 0;
volatile unsigned int chan4PPM  = 0;  


void capturePPMInit(void)   
{

  pinMode(Channel1Pin, INPUT);
  pinMode(Channel2Pin, INPUT);
  pinMode(Channel3Pin, INPUT);
  pinMode(Channel4Pin, INPUT);

  attachInterrupt(Channel1Pin,handler_CH1,CHANGE);  
  attachInterrupt(Channel2Pin,handler_CH2,CHANGE);
  attachInterrupt(Channel3Pin,handler_CH3,CHANGE);
  attachInterrupt(Channel4Pin,handler_CH4,CHANGE);  
}

void handler_CH1(void)
{  
  unsigned int total = 0;  
  if(digitalRead(Channel1Pin)  == 1)  
  {
    chan1begin = micros(); 
  }  
  else
  {
    if(chan1begin != 0)  
    {
       chan1end = micros();
      total = chan1end - chan1begin;
      if((total > 950) && (total < 2000)) 
      {
        chan1PPM = total;
      }
      chan1begin = 0;
     }
  }
}    

void handler_CH2(void) 
{              
  unsigned int total = 0;  
  if(digitalRead(Channel2Pin)  == 1)  
  {
    chan2begin = micros(); 
  }  
  else
  {
    if(chan2begin != 0)  
    {
       chan2end = micros();
      total = chan2end - chan2begin;
      if((total > 950) && (total < 2000)) 
      {
        chan2PPM = total;
      }
      chan2begin = 0;
     }
  }
} 

void handler_CH3(void) 
{              
  unsigned int total = 0;  
  if(digitalRead(Channel3Pin)  == 1)  
  {
    chan3begin = micros(); 
  }  
  else
  {
    if(chan3begin != 0)  
    {
       chan3end = micros();
      total = chan3end - chan3begin;
      if((total > 950) && (total < 2000)) 
      {
        chan3PPM = total;
      }
      chan3begin = 0;
     }
  }
}     

void handler_CH4(void)
{              
  unsigned int total = 0;  
  if(digitalRead(Channel4Pin)  == 1) 
  {
    chan4begin = micros(); 
  }  
  else
  {
    if(chan4begin != 0)  
    {
       chan4end = micros();
      total = chan4end - chan4begin;
      if((total > 950) && (total < 2000))
      {
        chan4PPM = total;
      }
      chan4begin = 0;
     }
  }
}  






