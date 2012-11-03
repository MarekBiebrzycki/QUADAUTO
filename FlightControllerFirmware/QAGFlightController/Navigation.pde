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
