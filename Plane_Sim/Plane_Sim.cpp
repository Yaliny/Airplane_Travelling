// Plane_Sim.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "Airport.h"
#include "Plane.h"
#include <iostream>
#include <fstream>
#include <vector>

using namespace std;



int main()
{
	int nbr_airports = 0;
	int nbr_planes = 0;
	//Creating a file stream handler (for open, read and save actions)
	fstream opener;
	opener.open("Topology.txt");
	string tmp;
	int tmpid;
	int tmpidA;
	int tmpidB;
	int tmpdistA;
	int tmpdistB;
	opener >> nbr_airports;
	cout << "Number of Airports:  " << nbr_airports<<endl;
	vector<Airport> airport_List;
	int count = 1;
	while (opener >> tmpid && count <= nbr_airports)
	{
		opener >> tmpidA;
		opener >> tmpdistA;
		opener >> tmpidB;
		opener >> tmpdistB;
		Airport airport = Airport(tmpid,tmpidA,tmpdistA,tmpidB,tmpdistB);
		airport_List.push_back(airport);
		cout << "Airport : " << " " << tmpid << " " << tmpidA << " " << tmpdistA << " " << tmpidB <<" " << tmpdistB << endl;
		count++;
	}
	cout << endl;
	count = 0;
	opener.close();
	opener.open("Scenario.txt");
	int tmpidPlane, tmpCapacity, tmpfirst_AirportID;
	char tmpDirection;
	opener >> nbr_planes;
	cout << "Number of Planes:  " << nbr_airports<<endl;
	vector<Plane> planes_list;
	while (opener >> tmpidPlane && count <= nbr_planes)
	{
		opener >> tmpCapacity;
		opener >> tmpfirst_AirportID;
		opener >> tmpDirection;
		Plane plane = Plane(tmpidPlane, tmpCapacity, tmpfirst_AirportID,tmpDirection);
		planes_list.push_back(plane);
		cout <<  "Plane :  " << " "<< tmpidPlane << " " << tmpCapacity<< " " << tmpfirst_AirportID << " "<<tmpDirection<<endl;
		count++;
	}
	opener.close();
	
	return 0;
}

