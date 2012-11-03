struct Vehicle{
  char  VIN[16];
  int   Altitude;
  int   Latitude;
  int   Longitude;
  int   Heading;
  int   SpeedMph;
};
unsigned int numVehicles=1;
Vehicle Vehicles[1];

void HeartBeatReceived(char vin[16],int alt,int lat,int lon,int head,int mph)
{
  int vIndex=VehicleExists(vin);
  if (vIndex==-1)
  {
     //Vehicle Not in List
     AddVehicle(vin,alt,lat,lon,head,mph);
  }
  else
  {
   // Vehicles[vIndex].VIN=vin;
    Vehicles[vIndex].Altitude=alt;
    Vehicles[vIndex].Latitude=lat;
    Vehicles[vIndex].Longitude=lon;
    Vehicles[vIndex].Heading=head;
    Vehicles[vIndex].SpeedMph=mph;
  }
  CollissionCheck();
}

void CollissionCheck()
{

}
void AddVehicle(char vin[16],int alt,int lat,int lon,int head,int mph)
{
//  /numVehicles++;
//  //Vehicle vs[numVehicles];
//   for(int it=0;it!=numVehicles;it++)
//  {
//    vs[it]=Vehicles[it];
//  }
//
//  Vehicle v;
// // v.VIN=vin;
//  v.Altitude=alt;
//  v.Latitude=lat;
//  v.Longitude=lon;
//  v.Heading=head;
//  v.SpeedMph=mph;
//  //vs[numVehicles]=v;
//  Vehicles=vs;  
}
int VehicleExists(char vin[16])
{
  for(int it=0;it!=numVehicles;it++)
  {
    boolean noMatch=false;
    for(int vit=0;vit!=16;vit++)
    {
      if(vin[vit]!=Vehicles[it].VIN[vit])
        noMatch=true;    
        break;  
    }
    if (noMatch==false)
      return it;
  }
  return -1;
}



