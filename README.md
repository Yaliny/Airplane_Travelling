# Airplane_Travelling

N airports provide connections between N different cities. Each airport provides basic services for the landing airplanes
– especially it allows them to be refueled, but due to economic reasons, only if there is less then ½ of the airplane's tank
capacity of fuel. Each plane uses 100 l of fuel for every 1 hour of flight.
The airports have organized M cruise connections, where each airport is directly connected with two other (A and B).
Each plane arriving from A would, after having its tank refilled, depart to B. Similarly, the plane arriving from B would
always depart to A.
At the start of simulation, each plane has its fuel tank filled in 100%.<br><br>
<h2>INPUT</h2>
The input consists of 2 text files:<br><br>
1) Topology input<br>
< Number of airports><br>
< Airport ID > < Conn A ID > < Conn. A distance (hours) > < Conn B ID > < Conn. B distance><br>
< Airport ID > < Conn A ID > < Conn. A distance (hours) > < Conn B ID > < Conn. B distance><br>
<br>
SAMPLE:<br>
3<br>
1 2 10 3 5<br>
2 1 10 3 7<br>
3 2 7 1 5<br><br>
2) Scenario input<br>
< Number of planes><br>
< Plane ID > < Tank capacity > < Starting airport ID > < Direction (A/B) ><br>
SAMPLE:<br>
2<br>
1 1500 2 A<br>
2 1500 2 B<br><br><br>
<h2>OUTPUT</h2>
The input consists of a single text file:<br><br>
< time > < plane id > < airport id> < from (A/B)> < is refueled (TRUE / FALSE)><br>
< time> < plane id> < airport id> < from (A/B)> < is refueled (TRUE / FALSE)><br>
< time> < plane id> < airport> !!!CRASH!!!<br><br>
The crash event means that the plane was not able to reach the destination airport because there was not enough fuel.<br><br>
#Modification:
Each airport can handle only 1 plane at a time. If a plane arrives and departs without
refilling fuel, the operation takes 95 minutes. If the tank needs to be refilled, the operation takes 10
minutes for every 100l of fuel. The planes that arrive during one plane's servicing time land, by are
transported to a nearby waiting point. Only 3 planes can stay at the airport at one time, so if an
additional plane arrives it has to wait in the air.
