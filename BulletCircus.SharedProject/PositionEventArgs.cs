using Microsoft.Xna.Framework;
using System;

namespace BulletCircus
{
	/// <summary>
	/// custom event argument that includes a position
	/// </summary>
	public class PositionEventArgs : EventArgs
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		public PositionEventArgs(Vector2 pos)
		{
			Position = pos;
		}

		/// <summary>
		/// The previous state
		/// </summary>
		public Vector2 Position { get; set; }
	}
}
