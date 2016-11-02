#include "stdafx.h"
#include "Plane.h"
#include <iostream>
#include <sstream>
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


void Plane::setcurrent_flight_time(int hr)
{
	current_flight_time = hr;
}

int Plane::getcurrent_flight_time()
{
	return current_flight_time;
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
	ostringstream oss;
	ostringstream oss1;
	ostringstream oss2;
	string tmp;
	int thour = 1+tm_time->tm_hour + connTime;
	oss << thour; 
	dest_arriv_time = oss.str();
	int tmin = 1+tm_time->tm_min;
	oss1 << tmin;
	dest_arriv_time += ":";
	dest_arriv_time += oss1.str();
	int tsec = 1+tm_time->tm_sec;
	oss2 << tsec;
	dest_arriv_time += ":";
	dest_arriv_time += oss2.str();
	
}



Plane::~Plane()
{
}

int Plane::getCapacity()
{
	return capacity;
}
int Plane::getPlaneID()
{
	return planeID;
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