using BulletMLLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BulletCircus
{
	/// <summary>
	/// This thing takes a bullet and a texture2d and draws the tex over the bullet.
	/// </summary>
	public class BulletSprite
	{
		#region Properties

		/// <summary>
		/// The texture to draw over the bullets
		/// </summary>
		Texture2D Tex { get; set; }

		#endregion //Properties

		#region Methods

		public BulletSprite(Texture2D tex)
		{
			Tex = tex;
		}

		/// <summary>
		/// draw a bunch of bullets
		/// </summary>
		/// <param name="spriteBatch"></param>
		/// <param name="bullets"></param>
		public void Render(SpriteBatch spriteBatch, List<Bullet> bullets, Color color)
		{
			for (int i = 0; i < bullets.Count; i++)
			{
				Render(spriteBatch, bullets[i], color);
			}
		}

		/// <summary>
		/// draw a single bullet
		/// </summary>
		/// <param name="spriteBatch"></param>
		/// <param name="bullet">the bullet to draw</param>
		public void Render(SpriteBatch spriteBatch, Bullet bullet, Color color)
		{
			//TODO: draw the bullet
		}

		#endregion //Methods
	}
}