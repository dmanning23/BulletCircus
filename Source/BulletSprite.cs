using BulletMLLib;
using MatrixExtensions;
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

		Vector2 Offset { get; set; }

		#endregion //Properties

		#region Methods

		public BulletSprite(Texture2D tex)
		{
			Tex = tex;

			Offset = new Vector2(Tex.Bounds.Width * -0.5f, Tex.Bounds.Height * -0.5f);
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
			//get the location to draw at:
			
			//get the offset vector
			Vector2 pos = new Vector2((Offset.X * bullet.Scale), (Offset.Y * bullet.Scale));

			//rotate the offset
			var matrix = MatrixExt.Orientation(bullet.Direction);
			pos = matrix.Multiply(pos);

			//move to the correct location
			pos += new Vector2(bullet.X, bullet.Y);

			//draw the bullet
			spriteBatch.Draw(Tex,
				pos,
				null,
				color,
				bullet.Direction,
				Vector2.Zero,
				bullet.Scale,
				SpriteEffects.None,
				0.0f);


		}

		#endregion //Methods
	}
}