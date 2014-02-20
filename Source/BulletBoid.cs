using System;
using Vector2Extensions;
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
		public Vector2 _position = Vector2.Zero;

		private BulletBoidManager BulletBoidManager { get; set; }

		#endregion //Members

		#region Properties

		/// <summary>
		/// Whether or not this dude is still valid, or should be removed from the game
		/// </summary>
		public bool Used { get; set; }

		public override float X
		{
			get { return _position.X; }
			set { _position.X = value; }
		}

		public override float Y
		{
			get { return _position.Y; }
			set { _position.Y = value; }
		}
		
		#endregion //Properties

		#region Methods

		/// <summary>
		/// Initializes a new instance of the <see cref="BulletBoid"/> class.
		/// </summary>
		/// <param name="myBulletManager">My bullet manager.</param>
		public BulletBoid(BulletBoidManager myBulletManager) : base(myBulletManager)
		{
			//store the bulelt boid manager
			BulletBoidManager = myBulletManager;
		}

		public void Init(Vector2 pos,
			float radius,
			Vector2 dir,
			float speed,
			float bulletmlScale)
		{
			Used = true;

			//set timespeed
			TimeSpeed = 1.0f;

			//set scale
			Scale = bulletmlScale;

			//set the position of the bullet
			_position = pos;

			//set the orientation of the bullet
			Direction = dir.Angle();

			//set the speed of the bullet
			Speed = speed;

			//create the boid
			MyBoid = new Boid(BulletBoidManager, pos, radius, dir, speed, 1.0f, 500.0f, 1.0f, 100.0f);

			//create the physics data with 
			Physics = new Circle(pos, radius);
		}

		public override void Update()
		{
			//the boid is updated in the mananger update.  that will give a target heading.

			//Next the bullet is updated.  If it uses an "aim" action, it will use the heading from the boid.
			base.Update();

			//Next the position of the boid is set to the updated position of the bullet.
			MyBoid.Position = _position;

			//Last, the position of the circle is updated.
			Physics.Pos = _position;
		}

		/// <summary>
		/// Get the direction the boid in us wants to go!
		/// </summary>
		/// <returns></returns>
		public override float GetAimDir()
		{
			return MyBoid.Rotation;
		}

		#endregion //Methods
	}
}
