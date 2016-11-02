// Plane_Sim.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "Airport.h"
#include "Plane.h"
#include <iostream>
#include <sstream>
#include <string>
#include <fstream>
#include <vector>
#include <windows.h>
#include <algorithm>

using namespace std;

vector<Airport> airport_List;
vector<Plane> planes_list;

string run_scenario(Plane plane)
{
	
	int departAirport = plane.getcurrent_AirportID();
	Airport current_Airport = airport_List[departAirport - 1];
	int destdistance;
	if (plane.getPlaneDirection() == 'A')
	{
		destdistance = current_Airport.getAConn_distance_hr();
		plane.setDest_AiportID(current_Airport.getAId_dest_Airport());
	}
	else
	{
	    destdistance = current_Airport.getBConn_distance_hr();
		plane.setDest_AiportID(current_Airport.getBId_dest_Airport());
	}
		int actualcap = plane.getActualCapacity();
	
		if ( actualcap/ 100 < destdistance)
		{
			plane.setState("!!!CRASH!!!");
			cout << "Plane :  " << " " << plane.getPlaneID() << " " << plane.getCapacity() << " " << plane.getFirst_AiportID() << " " << plane.getPlaneDirection() << "Current_cap: " << plane.getActualCapacity() << endl;
			plane.setcurrent_flight_time(actualcap/100);
			plane.setdest_arriv_time(plane.getcurrent_flight_time());
			Sleep(1 * actualcap / 100);
			string toprint;
			ostringstream oss;
			oss <<plane.getdest_arriv_time();
			toprint += oss.str();
			toprint += "\t ";
			ostringstream oss1;
			oss1 << plane.getPlaneID();
			toprint += oss1.str();
			toprint += " \t"; 
			ostringstream oss2;
			oss2 << plane.getDest_AiportID();
			toprint += oss2.str();
			toprint += "\t ";
			ostringstream oss3;
			oss3 << plane.getState();
			toprint += oss3.str();
			toprint += "\n";
			cout << toprint;
			planes_list[plane.getPlaneID() - 1] = plane;
			plane.~Plane();
			return toprint;
			
		}
		else
		{
			plane.setState("ARRIVED");
			plane.setdest_arriv_time(destdistance);
			plane.setActualCapacity(actualcap - 100 * destdistance);
			Sleep(1 * destdistance);
			int prev_dest_airportID(plane.getDest_AiportID());
			int prev_current_airportID = plane.getcurrent_AirportID();
			plane.setcurrent_AirportID(prev_current_airportID);
			Airport currentairp = airport_List[prev_dest_airportID-1];
			string toprint;
			ostringstream oss;
			oss << plane.getdest_arriv_time() ;
			toprint = oss.str();
			toprint += "\t ";
			ostringstream oss1;
			oss1 << plane.getPlaneID();
			toprint += oss1.str();
			toprint += " \t";
			ostringstream oss2;
			oss2 << prev_dest_airportID;
			toprint += oss2.str();
			toprint += " From : ";
			ostringstream oss3;
			oss3<< plane.getPlaneDirection();
			toprint += oss3.str();
			toprint += " is refueled :";
			ostringstream oss4;
			oss4<< currentairp.RefuelPlane(plane);
			toprint += oss4.str();
			toprint += "\n ";
			if(currentairp.getAId_dest_Airport() == prev_current_airportID)
			{
				plane.setPlaneDirection('B');
			}
			else
			{
				plane.setPlaneDirection('A');
			}
			cout << "Plane :  " << " " << plane.getPlaneID() << " " << plane.getCapacity() << " " << plane.getDest_AiportID() << " " << plane.getPlaneDirection() << "Current_cap: " << plane.getActualCapacity() << endl;
			airport_List[prev_dest_airportID-1] = currentairp;
			planes_list[plane.getPlaneID()-1] = plane;
			cout << toprint << endl;
			run_scenario(plane);
			return toprint;
		}
		
	
	
}

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
	
	while (opener >> tmpidPlane && count <= nbr_planes)
	{
		opener >> tmpCapacity;
		opener >> tmpfirst_AirportID;
		opener >> tmpDirection;
		Plane plane = Plane(tmpidPlane, tmpCapacity, tmpfirst_AirportID,tmpDirection);
		planes_list.push_back(plane);
		cout <<  "Plane :  " << " "<< plane.getPlaneID()<< " " << plane.getCapacity()<< " " << plane.getFirst_AiportID() << " "<<plane.getPlaneDirection()<<"Current_cap: "<< plane.getActualCapacity()<<endl;
		count++;
		cout << endl << endl << endl << "========================================================================================" << endl;
	}
	opener.close();
	
	for (int i = 0; i < nbr_planes; i++)
	{
		Plane plane = planes_list[i];
		run_scenario(plane);
	}
	cin.ignore();
	return 0;
}

