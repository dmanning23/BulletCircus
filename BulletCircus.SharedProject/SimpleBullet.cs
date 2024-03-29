﻿using BulletMLLib;
using CollisionBuddy;
using FlockBuddy.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PrimitiveBuddy;
using Vector2Extensions;

namespace BulletCircus
{
	/// <summary>
	/// This class is a simple BulletML.Bullet with collisionbuddy.circle
	/// </summary>
	public class SimpleBullet : Bullet, IMover
	{
		#region Members
		
		/// <summary>
		/// THe physics data for this guy
		/// </summary>
		public Circle Physics { get; private set; }

		/// <summary>
		/// The poisition of this bullet.
		/// </summary>
		protected Vector2 _position = Vector2.Zero;
		protected Vector2 _oldPosition = Vector2.Zero;

		/// <summary>
		/// a vector perpendicular to the heading vector
		/// </summary>
		private Vector2 _side;

		#endregion //Members

		#region Properties

		/// <summary>
		/// Whether or not this dude is still valid, or should be removed from the game
		/// </summary>
		public bool Used { get; private set; }

		public override float X
		{
			get { return _position.X; }
			set 
			{
				_oldPosition.X = _position.Y;
				_position.X = value; 
			}
		}

		public override float Y
		{
			get { return _position.Y; }
			set 
			{
				_oldPosition.Y = _position.Y;
				_position.Y = value; 
			}
		}

		/// <summary>
		/// its location in the environment
		/// Used by the cell space IMovingEntity thing
		/// </summary>
		public Vector2 Position
		{
			get
			{
				return _position;
			}
			set
			{
				_oldPosition = _position;
				_position = value;
			}
		}

		/// <summary>
		/// its location in the environment
		/// Used by the cell space IMovingEntity thing
		/// </summary>
		public Vector2 OldPosition 
		{
			get
			{
				return _oldPosition;
			}
			private set
			{
				_oldPosition = value;
			}
		}

		public Vector2 Velocity
		{
			get
			{
				return (Heading * Speed * Scale) * 60.0f;
			}
		}

		public Vector2 Heading
		{
			get
			{
				return Direction.ToVector2();
			}
		}

		public override float Direction
		{
			get
			{
				return base.Direction;
			}
			set
			{
				base.Direction = value;
				_side = Heading.Perp();
			}
		}

		public Vector2 Side
		{
			get
			{
				return _side;
			}
		}

		public float Radius
		{
			get
			{
				return Physics.Radius;
			}
			set
			{
				Physics.Radius = value;
			}
		}

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Initializes a new instance of the <see cref="SimpleMissile"/> class.
		/// </summary>
		/// <param name="myBulletManager">My bullet manager.</param>
		public SimpleBullet(IBulletManager myBulletManager)
			: base(myBulletManager)
		{
		}

		public virtual void Init(Vector2 pos,
			float radius,
			Vector2 dir,
			float speed,
			float bulletmlScale,
			Vector2 initialVelocity)
		{
			Used = true;

			//set timespeed
			TimeSpeed = 1.0f;

			//set scale
			Scale = bulletmlScale;

			//set the position of the bullet
			_position = pos;
			OldPosition = Position;

			//set the orientation of the bullet
			Direction = dir.Angle();

			//set the speed of the bullet
			Speed = speed;

			InitialVelocity = initialVelocity;

			//create the physics data with 
			Physics = new Circle(pos, radius);
		}

		/// <summary>
		/// This method gets called after the update method
		/// </summary>
		public override void PostUpdate()
		{
			//Last, the position of the circle is updated.
			Physics.Pos = _position;
		}

		/// <summary>
		/// Draw the debug info for this bullet
		/// </summary>
		/// <param name="prim"></param>
		/// <param name="color"></param>
		public virtual void Render(IPrimitive prim, Color color)
		{
			prim.Circle(Position, Radius, color);
		}

		/// <summary>
		/// Render a texture over this bullet
		/// Override in child classes to draw more stuff, like trails or particle effects
		/// </summary>
		/// <param name="spriteBatch"></param>
		/// <param name="sprite"></param>
		/// <param name="color"></param>
		public virtual void Render(SpriteBatch spriteBatch, BulletSprite sprite, Color color)
		{
			sprite.Render(spriteBatch, this, color);
		}

        /// <summary>
        /// Call this method to kill a bullet.
        /// </summary>
        public virtual void KillBullet()
        {
            Used = false;
        }

		#endregion //Methods
	}
}
