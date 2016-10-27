// Plane_Sim.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "Airport.h"

int main()
{
	Airport A = Airport(5);
	printf("%d", A.getAirportID());
	
	return 1;
}

