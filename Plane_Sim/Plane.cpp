#include "stdafx.h"
#include "Plane.h"


Plane::Plane(int id, int capacityn)
{
	planeID = id;
	capacity = capacityn;
	actualCapacity = capacityn;
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
	return actualCapacity;
}
void Plane::setActualCapacity(int actualCap)
{
	actualCapacity = actualCap;
}
Plane::~Plane()
{
}
Plane::~Plane()
{
}