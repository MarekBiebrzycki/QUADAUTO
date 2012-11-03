

enum PacketCommand
{
  HeartBeat,
  Shutdown,
  Hover,
  AddWayPoint,
  TakeOff,
  TravelToWayPoint,
  TravelToWayPointandLand,
  YawToHeading,
  AscendToAltitude,
  DescendToAltitude,
  Land,
  CollisionImminent,
  CollisionImminentResponse,
  RestrictedAitspace
};

struct HearBeatPacket
{
  int  Altitude;
  int Latitude;
  int Longitude;
  int Heading;
  int SpeedMph;
};

struct Packet{
  char VIN[16];
  PacketCommand Command;
  int datalength;
  char PacketData[50];
};

EasyTransfer ComLink;
Packet  data ;

HearBeatPacket   RecievedHeartBeat;
uint8_t * address;
void SetupCommunications()
{
  Serial1.begin(57600);
  ComLink.begin(details(data),&Serial1);
}

void CheckRecieve()
{
  //Check if we Have data
  if (ComLink.receiveData())
  {
    //Check Command Type and memcopy data.PacketData to the correct command data structure
    switch (data.Command)
    {
      case HeartBeat:
        address=details(RecievedHeartBeat);
        GetPacketData(20);
        //call Navigation methods based on that data
        HeartBeatReceived(data.VIN,RecievedHeartBeat.Altitude,RecievedHeartBeat.Latitude,RecievedHeartBeat.Longitude,RecievedHeartBeat.Heading,RecievedHeartBeat.SpeedMph);
        break;
      
    }
  }
//  memcpy(address,PacketData,size); //once we get a packet we will need to read out the command and memcopy the PacketData to the correct structure type for the command. then 
}

void GetPacketData(int length)
{
  memcpy(address,data.PacketData,length);
}


void SendHeartBeat()
{
}

void SendShutDown(){}

void SendHover(){}
void SendAddWayPoint(){}
void SendTakeOff(){}
void SendTravelToWayPoint(){}
void SendTravelToWayPointAndLand(){}
void SendYawToHeading(){}
void SendAscendToAltitude(){}
void SendDescendToAltitude(){}
void SendLand(){}
void SendCollisionIminent(){}
void SendCollisionIminentREsponce(){}
void SendRestrictedAirSpace(){}
