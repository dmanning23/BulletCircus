BulletFlockBuddy
================

a little game that mixes BulletML, CollisionBuddy, and the FlockBuddy.

Why do this:
-Gives the bullet hell scripting abilities of BulletML
-Fast circle-circle collision from the CollisionBuddy
-Flocking behavior can be added to the shoals of missiles:
     alignment
     cohesion
     separation
     obstacleavoidance
     evade
     pursuit
     seek
     flee

The behavior I'm looking for is a shoal of missiles are fired, they group together, pursue the enemy ship, while avoiding interceptor missiles and obstacles like explosions.

None of those three packages were really written to be interchangeable, so there will be a BulletBoid class that encapsulates all three of them.

update loop:
First the boid is updated.  This gives a target heading.
Next the bullet is updated.  If it uses an "aim" action, it will use the heading from the boid.
Next the position of the boid is set to the updated position of the bullet.
Last, the position of the circle is updated.
