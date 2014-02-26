using BulletMLLib;
using CollisionBuddy;
using Microsoft.Xna.Framework;
using Vector2Extensions;
using BasicPrimitiveBuddy;

namespace BulletFlockBuddy
{
	/// <summary>
	/// This class is a simple BuleltML.Bullet with collisionbuddy.circle
	/// </summary>
	public class SimpleBullet : Bullet
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

		public Vector2 Position
		{
			get
			{
				return _position;
			}
		}
		
		#endregion //Properties

		#region Methods

		/// <summary>
		/// Initializes a new instance of the <see cref="BulletBoid"/> class.
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

			//create the physics data with 
			Physics = new Circle(pos, radius);
		}

		public override void Update()
		{
			//the bullet is updated
			base.Update();

			//Last, the position of the circle is updated.
			Physics.Pos = _position;
		}

		public virtual void Render(IBasicPrimitive prim, Color color)
		{
			prim.Circle(Position, Physics.Radius, color);
		}

		#endregion //Methods
	}
}
