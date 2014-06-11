using BulletMLLib;
using GameTimer;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System;

namespace BulletCircus
{
	/// <summary>
	/// This manager creates simple bullets that use the default BulletML behavior.  
	/// If you don't really need the flocking functionality, use this dude instead.
	/// </summary>
	public class SimpleBulletManager : ISimpleBulletManager
	{
		#region Members

		/// <summary>
		/// crappy object for locking the list
		/// </summary>
		private object _listLock = new object();

		public List<SimpleBullet> Bullets { get; private set; }

		private List<SimpleBullet> TopBullets { get; set; }

		/// <summary>
		/// event that is trigered whenever a bullet is removed
		/// </summary>
		public event EventHandler<PositionEventArgs> BulletRemovedEvent;

		private float _timeSpeed = 1.0f;
		private float _scale = 1.0f;

		public PositionDelegate GetPlayerPosition;

		private float _tier = 0.0f;

		#endregion //Members

		#region Properties

		/// <summary>
		/// How fast time moves in this game.
		/// Can be used to do slowdown, speedup, etc.
		/// </summary>
		/// <value>The time speed.</value>
		public float TimeSpeed
		{
			get
			{
				return _timeSpeed;
			}
			set
			{
				//set my time speed
				_timeSpeed = value;

				//set all the bullet time speeds
				foreach (var myDude in Bullets)
				{
					myDude.TimeSpeed = _timeSpeed;
				}
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
				return _scale;
			}
			set
			{
				//set my scale
				_scale = value;

				//set all the bullet scales
				foreach (var myDude in Bullets)
				{
					myDude.Scale = _scale;
				}
			}
		}

		public Vector2 StartPosition { get; set; }

		public Vector2 StartHeading { get; set; }

		public float StartSpeed { get; set; }

		public Vector2 InitialVelocity { get; set; }

		/// <summary>
		/// convert the initial velocity from pixels/second to pixels/frame
		/// </summary>
		public Vector2 InitialBulletVelocity 
		{ 
			get
			{
				return InitialVelocity / 60.0f;
			}
		}

		#endregion //Properties

		#region Methods

		public SimpleBulletManager(PositionDelegate playerDelegate)
		{
			Debug.Assert(null != playerDelegate);
			GetPlayerPosition = playerDelegate;
			Bullets = new List<SimpleBullet>();
			TopBullets = new List<SimpleBullet>();
			StartHeading = Vector2.UnitY;
		}

		/// <summary>
		/// a mathod to get current position of the player
		/// This is used to target bullets at that position
		/// </summary>
		/// <returns>The position to aim the bullet at</returns>
		/// <param name="targettedBullet">the bullet we are getting a target for</param>
		public Vector2 PlayerPosition(IBullet targettedBullet)
		{
			//just give the player's position
			Debug.Assert(null != GetPlayerPosition);
			return GetPlayerPosition();
		}

		/// <summary>
		/// Create a new bullet.
		/// </summary>
		/// <returns>A shiny new bullet</returns>
		public IBullet CreateBullet()
		{
			//create the new bullet
			SimpleBullet myBullet = new SimpleBullet(this);

			InitBullet(myBullet);

			//return the bullet we created
			return myBullet;
		}

		/// <summary>
		/// Add a bullet to the bulletlist safely
		/// </summary>
		/// <param name="bullet"></param>
		public void InitBullet(SimpleBullet bullet)
		{
			//initialize the bullet
			bullet.Init(StartPosition, 10.0f, StartHeading, StartSpeed, Scale, InitialBulletVelocity);

			//lock the list before adding the bullet
			lock (_listLock)
			{
				Bullets.Add(bullet);
			}
		}

		public IBullet CreateTopBullet()
		{
			//create the new bullet
			SimpleBullet myBullet = new SimpleBullet(this);

			//initialize the bullet
			InitTopBullet(myBullet);

			//return the bullet we created
			return myBullet;
		}

		/// <summary>
		/// Add a bullet to the bulletlist safely
		/// </summary>
		/// <param name="bullet"></param>
		public void InitTopBullet(SimpleBullet bullet)
		{
			//initialize the bullet
			bullet.Init(StartPosition, 10.0f, StartHeading, StartSpeed, Scale, Vector2.Zero);

			//lock the list before adding the bullet
			lock (_listLock)
			{
				TopBullets.Add(bullet);
			}
		}

		public void RemoveBullet(IBullet deadBullet)
		{
			var myMover = deadBullet as SimpleBullet;
			if (myMover != null)
			{
                myMover.KillBullet();
			}
		}

		/// <summary>
		/// called from main game
		/// </summary>
		/// <param name="gameTime"></param>
		public void Update(GameClock gameTime)
		{
			HalfUpdate(gameTime);

			FreeBullets();
		}

		/// <summary>
		/// call this if you want the top bullets to stick to a particular item or something
		/// </summary>
		/// <param name="pos"></param>
		public void UpdateTopBulletPositions(Vector2 pos)
		{
			for (int i = 0; i < TopBullets.Count; i++)
			{
				TopBullets[i].Position = pos;
			}
		}

		/// <summary>
		/// called form missile manager
		/// </summary>
		/// <param name="gameTime"></param>
		internal void HalfUpdate(GameClock gameTime)
		{
			//Update all the top bullets first
			for (int i = 0; i < TopBullets.Count; i++)
			{
				TopBullets[i].Update();
			}

			//create a list of all our tasks
			List<Task> taskList = new List<Task>();

			//grab the number fo bullets in the list, so we dont update new ones
			int numBullets = Bullets.Count;

			//update the bulletboid part of the dude
			for (int i = 0; i < numBullets; i++)
			{
				taskList.Add(Bullets[i].UpdateAsync());
			}

			//wait for all the updates to finish
			Task.WaitAll(taskList.ToArray());

			//Run the post update step
			for (int i = 0; i < numBullets; i++)
			{
				Bullets[i].PostUpdate();
			}
		}

		/// <summary>
		/// Clean up all the unused/dead bullets.
		/// </summary>
		internal void FreeBullets()
		{
			for (int i = 0; i < Bullets.Count; i++)
			{
				if (!Bullets[i].Used)
				{
					//dont need to lock the list, because this isn't called in update loop

					if (BulletRemovedEvent != null)
					{
						BulletRemovedEvent(this, new PositionEventArgs(Bullets[i].Position));
					}

					//remove from the list of bullets
					Bullets.Remove(Bullets[i]);

					i--;
				}
			}

			//clear out top level bullets
			for (int i = 0; i < TopBullets.Count; i++)
			{
				if (TopBullets[i].TasksFinished())
				{
					TopBullets.RemoveAt(i);
					i--;
				}
			}
		}

		/// <summary>
		/// remove all the bullets from play
		/// </summary>
		public void Clear()
		{
			//dont need to lock the list, because this isn't called in update loop

			Bullets.Clear();
			TopBullets.Clear();
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
			_tier = tier;
		}

		public float Tier()
		{
			return _tier;
		}

		#endregion //Methods
	}
}
