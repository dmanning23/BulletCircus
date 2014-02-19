using System;
using CollisionBuddy;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using BulletMLLib;
using FlockBuddy;

namespace BulletFlockBuddy
{
	/// <summary>
	/// This class wraps up BulletMLLib.Bullet with the FlockBuddy.Boid and CollisionBuddy.Circle
	/// </summary>
	class BulletBoid : Bullet
	{
		#region Members

		/// <summary>
		/// The flocking object this dude will use
		/// </summary>
		public Boid MyBoid { get; private set; }

		/// <summary>
		/// THe physics data for this guy
		/// </summary>
		public Circle Physics { get; private set; }

		/// <summary>
		/// The poisition of this bullet.
		/// </summary>
		public Vector2 pos;

		#endregion //Members

		#region Properties

		/// <summary>
		/// Whether or not this dude is still valid, or should be removed from the game
		/// </summary>
		public bool Used { get; set; }

		public override float X
		{
			get { return pos.X; }
			set { pos.X = value; }
		}

		public override float Y
		{
			get { return pos.Y; }
			set { pos.Y = value; }
		}
		
		#endregion //Properties

		#region Methods

		/// <summary>
		/// Initializes a new instance of the <see cref="BulletBoid"/> class.
		/// </summary>
		/// <param name="myBulletManager">My bullet manager.</param>
		public BulletBoid(BulletBoidManager myBulletManager) : base(myBulletManager)
		{
			//TODO: store the bulelt boid manager
		}

		public void Init()
		{
			Used = true;

			//set timespeed

			//set scale

			//TODO: set the position of the bullet

			//TODO: set the orientation of the bullet

			//TODO: create the boid

			//TODO: create the physics data with 
		}

		public override void Update()
		{
			//First the boid is updated.  This gives a target heading.

			//Next the bullet is updated.  If it uses an "aim" action, it will use the heading from the boid.

			//Next the position of the boid is set to the updated position of the bullet.

			//Last, the position of the circle is updated.


			//if (X < 0 || X > Game1.graphics.PreferredBackBufferWidth || Y < 0 || Y > Game1.graphics.PreferredBackBufferHeight)
			//{
			//	Used = false;
			//}
		}

		#endregion //Methods
	}
}
