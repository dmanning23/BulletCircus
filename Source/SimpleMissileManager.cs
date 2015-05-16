using BulletMLLib;
using FlockBuddy;
using GameTimer;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BulletCircus
{
	public class SimpleMissileManager : Flock, ISimpleBulletManager
	{
		#region Members

		protected SimpleBulletManager BulletManager { get; set; }

		/// <summary>
		/// event that is trigered whenever a bullet is removed
		/// </summary>
		public event EventHandler<PositionEventArgs> BulletRemovedEvent;

		#endregion //Members

		#region Properties

		public List<SimpleBullet> Bullets
		{
			get
			{
				return BulletManager.Bullets;
			}
		}

		/// <summary>
		/// How fast time moves in this game.
		/// Can be used to do slowdown, speedup, etc.
		/// </summary>
		/// <value>The time speed.</value>
		public float TimeSpeed 
		{ 
			get
			{
				return BulletManager.TimeSpeed;
			}
			set
			{
				BulletManager.TimeSpeed = value;
			}
		}

		/// <summary>
		/// Change the size of this bulletml script
		/// If you want to reuse a script for a game but the size is wrong, this can be used to resize it
		/// </summary>
		/// <value>The scale.</value>
		public float Scale
		{ 
			get
			{
				return BulletManager.Scale;
			}
			set
			{
				BulletManager.Scale = value;
			}
		}

		public Vector2 StartPosition 
		{
			get
			{
				return BulletManager.StartPosition;
			}
			set
			{
				BulletManager.StartPosition = value;
			}
		}

		public Vector2 StartHeading
		{
			get
			{
				return BulletManager.StartHeading;
			}
			set
			{
				BulletManager.StartHeading = value;
			}
		}

		public float StartSpeed
		{
			get
			{
				return BulletManager.StartSpeed;
			}
			set
			{
				BulletManager.StartSpeed = value;
			}
		}

		public Vector2 InitialVelocity
		{
			get
			{
				return BulletManager.InitialVelocity;
			}
			set
			{
				BulletManager.InitialVelocity = value;
			}
		}

		public virtual float BulletRadius
		{
			get
			{
				return BulletManager.BulletRadius;
			}
			set
			{
				//make sure to set the radius of the boid too
				BoidTemplate.Radius = value;
				BulletManager.BulletRadius = value;
			}
		}

		#endregion //Properties

		#region Methods

		public SimpleMissileManager()
		{
			BulletManager = new SimpleBulletManager(() => { return Vector2.Zero; } );
		}

		/// <summary>
		/// a mathod to get current position of the player
		/// This is used to target bullets at that position
		/// </summary>
		/// <returns>The position to aim the bullet at</returns>
		/// <param name="targettedBullet">the bullet we are getting a target for</param>
		public Vector2 PlayerPosition(IBullet targettedBullet)
		{
			Debug.Assert(false); //This should never get called!!!
			return BulletManager.PlayerPosition(targettedBullet);
		}
		
		/// <summary>
		/// Create a new bullet.
		/// </summary>
		/// <returns>A shiny new bullet</returns>
        public virtual IBullet CreateBullet()
		{
			//create the new bullet
			SimpleMissile myBullet = new SimpleMissile(this);

			BulletManager.InitBullet(myBullet);

			return myBullet;
		}

		public virtual IBullet CreateTopBullet()
		{
			//create the new bullet
			SimpleMissile myBullet = new SimpleMissile(this);

			BulletManager.InitTopBullet(myBullet);

			return myBullet;
		}
		
		public void RemoveBullet(IBullet deadBullet)
		{
			BulletManager.RemoveBullet(deadBullet);
		}

		public override void Update(GameClock curTime)
		{
			//the base class updates the flocking part of the dudes
			base.Update(curTime);

			//update the bullet part of the dude
			BulletManager.HalfUpdate(curTime);

			FreeBullets();
		}

		/// <summary>
		/// call this if you want the top bullets to stick to a particular item or something
		/// </summary>
		/// <param name="pos"></param>
		public void UpdateTopBulletPositions(Vector2 pos)
		{
			BulletManager.UpdateTopBulletPositions(pos);
		}

		/// <summary>
		/// Clean up all the unused/dead bullets.
		/// </summary>
		private void FreeBullets()
		{
			for (int i = 0; i < BulletManager.Bullets.Count; i++)
			{
				SimpleMissile bullet = BulletManager.Bullets[i] as SimpleMissile;
				Debug.Assert(null != bullet);

				if (!bullet.Used)
				{
					if (BulletRemovedEvent != null)
					{
						BulletRemovedEvent(this, new PositionEventArgs(Bullets[i].Position));
					}

					//remove from the flock also
					RemoveBoid(bullet.MyBoid);
				}
			}

			BulletManager.FreeBullets();
		}

		/// <summary>
		/// remove all the bullets from play
		/// </summary>
		public override void Clear()
		{
			//clear out the flock
			base.Clear();

			//clear out the bullets
			BulletManager.Clear();
		}

		/// <summary>
		/// Easy way to correctly shoot a bullet pattern
		/// </summary>
		/// <param name="pattern"></param>
		public void Shoot(BulletPattern pattern)
		{
			//add a new bullet in the center of the screen
			var bullet = CreateTopBullet();
			bullet.InitTopNode(pattern.RootNode);
		}

		public void Tier(float tier)
		{
			BulletManager.Tier(tier);
		}

		public double Tier()
		{
			return BulletManager.Tier();
		}

		#endregion //Methods
	}
}
