#pragma once
#include "Plane.h"

class Airport
{
public:
	Airport(int id);
	int getAirportID();
	void RefuelPlane(Plane a);
	virtual ~Airport();
private:
	int AirportID;
	bool RefueldPlane;
};

