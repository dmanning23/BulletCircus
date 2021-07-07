using BulletMLLib;
using GameTimer;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace BulletCircus
{
	/// <summary>
	/// This manager creates simple bullets that use the default BulletML behavior.  
	/// If you don't really need the flocking functionality, use this dude instead.
	/// </summary>
	public interface ISimpleBulletManager : IBulletManager
	{
		List<SimpleBullet> Bullets { get; }

		/// <summary>
		/// event that is trigered whenever a bullet is removed
		/// </summary>
		event EventHandler<PositionEventArgs> BulletRemovedEvent;

		Vector2 StartPosition { get; set; }

		Vector2 StartHeading { get; set; }

		float StartSpeed { get; set; }

		Vector2 InitialVelocity { get; set; }

		void Update(GameClock gameTime);

		/// <summary>
		/// call this if you want the top bullets to stick to a particular item or something
		/// </summary>
		/// <param name="pos"></param>
		void UpdateTopBulletPositions(Vector2 pos);

		/// <summary>
		/// remove all the bullets from play
		/// </summary>
		void Clear();

		/// <summary>
		/// Easy way to correctly shoot a bullet pattern
		/// </summary>
		/// <param name="pattern"></param>
		void Shoot(BulletPattern pattern);
	}
}
