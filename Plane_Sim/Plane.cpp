#include "stdafx.h"
#include "Plane.h"
#pragma warning(disable:4996)


Plane::Plane(int id, int capacityn, int  current_AirpId, char direction)
{
	planeID = id;
	capacity = capacityn;
	currentCapacity = capacityn;
	current_AirportID = current_AirpId;
	First_AirportID = current_AirpId;
	PlaneDirection = direction;
}


void Plane::setcurrent_flight_time()
{
}

string Plane::getdest_arriv_time()
{
	return dest_arriv_time;
}

string Plane::getState()
{
	return state;
}

void Plane::setState(string stat)
{
	state = stat;
}



void Plane::setdest_arriv_time(int connTime)
{
	time_t now = time(0);
	tm *tm_time = localtime(&now);
	if(state=="Arrived"|| state=="Refuelled")
		dest_arriv_time = tm_time->tm_hour+connTime + ':'+tm_time->tm_min +':'+ tm_time->tm_sec;
	else
		dest_arriv_time = tm_time->tm_hour + current_flight_time + ':' + tm_time->tm_min + ':' + tm_time->tm_sec;
}



Plane::~Plane()
{
}

int Plane::getCapacity()
{
	return capacity;
}
int Plane::getActualCapacity()
{
	return currentCapacity;
}
void Plane::setActualCapacity(int actualCap)
{
	currentCapacity = actualCap;
}
char Plane::getPlaneDirection()
{
	return PlaneDirection;
}
void Plane::setPlaneDirection(char id)
{
	PlaneDirection = id;
}

int Plane::getDest_AiportID()
{
	return Dest_AirportID;
}

int Plane::getFirst_AiportID()
{
	return First_AirportID;
}

int Plane::getcurrent_AirportID()
{
	return current_AirportID;
}

void Plane::setDest_AiportID(int id_Airp)
{
	Dest_AirportID = id_Airp;
}

void Plane::setcurrent_AirportID(int id_Airp)
{
	current_AirportID = id_Airp;
}