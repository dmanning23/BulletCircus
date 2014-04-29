using BulletMLLib;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using GameTimer;

namespace BulletCircus
{
	/// <summary>
	/// This manager creates simple bullets that use the default BulletML behavior.  
	/// If you don't really need the flocking functionality, use this dude instead.
	/// </summary>
	public interface ISimpleBulletManager : IBulletManager
	{
		#region Members

		List<SimpleBullet> Bullets { get; }

		#endregion //Members

		#region Properties

		Vector2 StartPosition { get; set; }

		Vector2 StartHeading { get; set; }

		float StartSpeed { get; set; }

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Add a bullet to the bulletlist safely
		/// </summary>
		/// <param name="bullet"></param>
		void InitBullet(SimpleBullet bullet);

		void Update(GameClock gameTime);

		/// <summary>
		/// remove all the bullets from play
		/// </summary>
		void Clear();

		#endregion //Methods
	}
}
