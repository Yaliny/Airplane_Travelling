#pragma once
class Plane
{
public:
	Plane(int id, int capacity);

	int getCapacity();
	int getActualCapacity();
	void setActualCapacity(int actualCap);
	virtual ~Plane();
private:
	int planeID;
	int capacity;
	int actualCapacity

};

