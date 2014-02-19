﻿using System;
using System.Diagnostics;
using System.Collections.Generic;
using BulletMLLib;
using Microsoft.Xna.Framework;
using FlockBuddy;

namespace BulletFlockBuddy
{
	class BulletBoidManager : Flock, IBulletManager
	{
		#region Members

		public List<BulletBoid> Bullets = new List<BulletBoid>();

		public PositionDelegate GetPlayerPosition;

		private float _timeSpeed = 1.0f;
		private float _scale = 1.0f;

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

		#endregion //Properties

		public BulletBoidManager(PositionDelegate playerDelegate)
		{
			Debug.Assert(null != playerDelegate);
			GetPlayerPosition = playerDelegate;
		}

		/// <summary>
		/// a mathod to get current position of the player
		/// This is used to target bullets at that position
		/// </summary>
		/// <returns>The position to aim the bullet at</returns>
		/// <param name="targettedBullet">the bullet we are getting a target for</param>
		public Vector2 PlayerPosition(Bullet targettedBullet)
		{
			//just give the player's position
			Debug.Assert(null != GetPlayerPosition);
			return GetPlayerPosition();
		}
		
		/// <summary>
		/// Create a new bullet.
		/// </summary>
		/// <returns>A shiny new bullet</returns>
		public Bullet CreateBullet()
		{
			//create the new bullet
			BulletBoid mover = new BulletBoid(this);

			//set the speed and scale of the bullet
			mover.TimeSpeed = TimeSpeed;
			mover.Scale = Scale;

			//initialize, store in our list, and return the bullet
			mover.Init();
			Bullets.Add(mover);

			//TODO: store the boid too

			return mover;
		}
		
		public void RemoveBullet(Bullet deadBullet)
		{
			var myMover = deadBullet as BulletBoid;
			if (myMover != null)
			{
				myMover.Used = false;
			}
		}

		public override void Update(GameTime curTime)
		{
			//the base class will update all the items
			base.Update(curTime);

			FreeBullets();
		}

		public override void UpdateBoid(Boid dude)
		{
			//the base class updates the flocking part of the dude
			base.Update(dude);

			//TODO: update the bulletboid part of the dude
		}

		/// <summary>
		/// Clean up all the unused/dead bullets.
		/// </summary>
		private void FreeBullets()
		{
			for (int i = 0; i < Bullets.Count; i++)
			{
				if (!Bullets[i].Used)
				{
					//remove from the list of bullets
					Bullets.Remove(Bullets[i]);

					//TODO: remove from the flock also

					i--;
				}
			}
		}
	}
}