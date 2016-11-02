#pragma once
#include "Plane.h"

class Airport
{
public:
	Airport(int id, int aId, int aDist, int bId, int bDist );
	int getAirportID();
	int getAId_dest_Airport();
	int getAConn_distance_hr();
	int getBId_dest_Airport();
	int getBConn_distance_hr();
	void setAId_dest_Airport(int id);
	void setAConn_distance_hr(int hr);
	void setBId_dest_Airport(int id);
	void setBConn_distance_hr(int hr);
	bool RefuelPlane(Plane a);
	virtual ~Airport();
private:
	int AirportID;
	int AId_dest_Airport;
	int BId_dest_Airport;
	int AConn_distance_hr;
	int BConn_distance_hr;
	bool RefueldPlane;
};

