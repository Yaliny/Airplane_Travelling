#include "stdafx.h"
#include "Airport.h"


Airport::Airport(int id)
{
	AirportID = id;
}


Airport::~Airport()
{

}

int Airport::getAirportID()
{
	return AirportID;
}

void Airport::RefuelPlane(Plane a)
{
	int planeCap = a.getCapacity();
	int actualCap = a.getActualCapacity();
	if (actualCap > planeCap / 2)
	{
		if (RefueldPlane == true)
		{
			RefueldPlane = false;
			return;
		}
	}
	else
	{
		a.setActualCapacity() = planeCap;
		RefueldPlane = true;
	}
}