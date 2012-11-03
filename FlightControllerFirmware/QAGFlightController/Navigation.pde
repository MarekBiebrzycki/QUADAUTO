struct WayPoint
{
  int Id;
  int Altitude;
  int Latitude;
  int Longitude;
};
struct Location
{
  int32 Altitude;
  int Latitude;
  int Longitude;
  float Heading;
};

float flat, flon;
unsigned long fix_age, time, date, speed, course;
unsigned long chars;
unsigned short sentences, failed_checksum;

volatile unsigned int throttle = 0; 
volatile unsigned int yaw      = 1500; 
volatile unsigned int pitch    = 1500;
volatile unsigned int roll     = 1500; 
int CurrentWayPoint=0;
WayPoint WayPoints[255];
Location CurrentLocation;

void RefreshNavigation()
{ 
  CurrentLocation.Altitude = bmp085GetAltitude(); 
  CurrentLocation.Heading = compassHeading();

  //  if (CurrentWayPoint!=0)
  //  {
  //  }
  while (Serial3.available())
  {
    int c = Serial3.read();
    if (gps.encode(c))
    {


      // time in hhmmsscc, date in ddmmyy
      gps.get_datetime(&date, &time, &fix_age);

      gps.f_get_position(&flat, &flon, &fix_age);
      float falt = gps.f_altitude(); // +/- altitude in meters
      float fc = gps.f_course(); // course in degrees
      float fk = gps.f_speed_knots(); // speed in knots
      float fmph = gps.f_speed_mph(); // speed in miles/hr

    }
  }
}
void TakeOffReceived(int altitude)
{

}

void TravelToWaypointReceived(int id)
{

}

//void AddWayPointReceived(WayPoint *wp)
//{
//  //WayPoints[wp.Id]  =  wp;  
//}




