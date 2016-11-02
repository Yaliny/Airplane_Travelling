#include "stdafx.h"
#include "Airport.h"


Airport::Airport(int id, int aId, int aDist, int bId, int bDist)
{
	AirportID = id;
	AId_dest_Airport = aId;
	AConn_distance_hr = aDist;
	BConn_distance_hr = bDist;
	BId_dest_Airport = bId;
}


Airport::~Airport()
{

}

int Airport::getAirportID()
{
	return AirportID;
}

int Airport::getAId_dest_Airport()
{
	return AId_dest_Airport;
}

int Airport::getAConn_distance_hr()
{
	return AConn_distance_hr;
}

int Airport::getBId_dest_Airport()
{
	return BId_dest_Airport;
}

int Airport::getBConn_distance_hr()
{
	return BConn_distance_hr;
}

void Airport::setAId_dest_Airport(int id)
{
	AId_dest_Airport = id;
}

void Airport::setAConn_distance_hr(int hr)
{
	AConn_distance_hr = hr;
}

void Airport::setBId_dest_Airport(int id)
{
	BId_dest_Airport = id;
}

void Airport::setBConn_distance_hr(int hr)
{
	BConn_distance_hr = hr;
}

bool Airport::RefuelPlane(Plane a)
{
	int planeCap = a.getCapacity();
	int actualCap = a.getActualCapacity();
	if (actualCap > planeCap / 2)
	{
		if (RefueldPlane == true)
		{
			RefueldPlane = false;
			
		}
	}
	else if(RefueldPlane==false)
	{
		a.setActualCapacity(planeCap);
		RefueldPlane = true;
	}
	else {
		RefueldPlane = false;
	}
	return RefueldPlane;
}