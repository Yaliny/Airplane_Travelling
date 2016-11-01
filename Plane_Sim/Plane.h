#include <string>
#include <ctime>
#include <cstring>
#pragma once

using namespace std;

class Plane
{
public:
	Plane(int id, int capacity,int  current_AirportID, char direction);
	int getCapacity();
	int getActualCapacity();
	void setActualCapacity(int actualCap);
	char getPlaneDirection();
	void setPlaneDirection(char id);
	int getDest_AiportID();
	int getFirst_AiportID();
	int getcurrent_AirportID();
	void setDest_AiportID(int id_Airp);
	void setcurrent_AirportID(int id_Airp);
	void setcurrent_flight_time();
	string  getdest_arriv_time();
	string getState();
	void setState(string stat);
	void setdest_arriv_time(int connTime);
	~Plane();

private:
	int planeID;
	int capacity;
	int currentCapacity;
	char PlaneDirection;
	int First_AirportID;
	int Dest_AirportID;
	int current_flight_time;
	string dest_arriv_time;
	int current_AirportID;
	string state;

};

