using PrimitiveBuddy;
using FlockBuddy;
using Microsoft.Xna.Framework;
using Vector2Extensions;

namespace BulletCircus
{
	/// <summary>
	/// This class wraps up BulletMLLib.Bullet with the FlockBuddy.Boid and CollisionBuddy.Circle
	/// </summary>
	public class SimpleMissile : SimpleBullet, IBoid
	{
		#region Members

		/// <summary>
		/// The flocking object this dude will use
		/// </summary>
		public Boid MyBoid { get; private set; }

		private SimpleMissileManager SimpleMissileManager { get; set; }

		#endregion //Members

		#region Properties

		/// <summary>
		/// Gets or sets the speed
		/// </summary>
		/// <value>The speed, in pixels/frame</value>
		public override float Speed
		{
			get
			{
				return base.Speed;
			}
			set
			{
				base.Speed = value;
				MyBoid.Speed = value;
			}
		}

		public float Mass
		{
			get
			{
				return MyBoid.Mass;
			}
			set
			{
				MyBoid.Mass = value;
			}
		}

		public float MinSpeed
		{
			get
			{
				return MyBoid.MinSpeed;
			}
			set
			{
				MyBoid.MinSpeed = value;
			}
		}

		public float WalkSpeed
		{
			get
			{
				return MyBoid.WalkSpeed;
			}
			set
			{
				MyBoid.WalkSpeed = value;
			}
		}

		public float MaxSpeed
		{
			get
			{
				return MyBoid.MaxSpeed;
			}
			set
			{
				MyBoid.MaxSpeed = value;
			}
		}

		public float MaxForce
		{
			get
			{
				return MyBoid.MaxForce;
			}
			set
			{
				MyBoid.MaxForce = value;
			}
		}

		public float MaxTurnRate
		{
			get
			{
				return MyBoid.MaxTurnRate;
			}
			set
			{
				MyBoid.MaxTurnRate = value;
			}
		}

		public ESummingMethod SummingMethod
		{
			set
			{
				MyBoid.SummingMethod = value;
			}
		}

		public float NeighborsQueryRadius
		{
			get
			{
				return MyBoid.NeighborsQueryRadius;
			}
			set
			{
				MyBoid.NeighborsQueryRadius = value;
			}
		}

		public float PredatorsQueryRadius
		{
			get
			{
				return MyBoid.PredatorsQueryRadius;
			}
			set
			{
				MyBoid.PredatorsQueryRadius = value;
			}
		}

		public float PreyQueryRadius
		{
			get
			{
				return MyBoid.PreyQueryRadius;
			}
			set
			{
				MyBoid.PreyQueryRadius = value;
			}
		}

		public float VipQueryRadius
		{
			get
			{
				return MyBoid.VipQueryRadius;
			}
			set
			{
				MyBoid.VipQueryRadius = value;
			}
		}

		public float WallQueryRadius
		{
			get
			{
				return MyBoid.WallQueryRadius;
			}
			set
			{
				MyBoid.WallQueryRadius = value;
			}
		}

		public float ObstacleQueryRadius
		{
			get
			{
				return MyBoid.ObstacleQueryRadius;
			}
			set
			{
				MyBoid.ObstacleQueryRadius = value;
			}
		}

		public float WaypointQueryRadius
		{
			get
			{
				return MyBoid.WaypointQueryRadius;
			}
			set
			{
				MyBoid.WaypointQueryRadius = value;
			}
		}

		public float RetargetTime
		{
			set
			{
				MyBoid.RetargetTime = value;
			}
		}

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Initializes a new instance of the <see cref="SimpleMissile"/> class.
		/// </summary>
		/// <param name="myBulletManager">My bullet manager.</param>
		public SimpleMissile(SimpleMissileManager myBulletManager) : base(myBulletManager)
		{
			//store the bulelt boid manager
			SimpleMissileManager = myBulletManager;

			//create the boid
			MyBoid = new Boid(SimpleMissileManager, Vector2.Zero, Vector2.Zero, 0f);
		}

		public override void Init(Vector2 pos,
			float radius,
			Vector2 dir,
			float speed,
			float bulletmlScale,
			Vector2 initialVelocity)
		{
			MyBoid.Position = pos;
			MyBoid.Radius = radius;
			MyBoid.Speed = speed;
			MyBoid.Heading = dir;

			//setup the bullet
			base.Init(pos, radius, dir, speed, bulletmlScale, initialVelocity);

			SetBoidToBullet();
		}

		/// <summary>
		/// This method gets called after the update method
		/// </summary>
		public override void PostUpdate()
		{
			//update the base class, which will do the physics circle
			base.PostUpdate();

			//Next the position of the boid is set to the updated position of the bullet.
			SetBoidToBullet();
		}

		/// <summary>
		/// Get the direction the boid in us wants to go!
		/// </summary>
		/// <returns></returns>
		public override float GetAimDir()
		{
			return MyBoid.Rotation;
		}

		public override void Render(IPrimitive prim, Color color)
		{
			base.Render(prim, color);
			MyBoid.Draw(prim, color);
		}

		/// <summary>
		/// update the boid to match the bullet params
		/// </summary>
		private void SetBoidToBullet()
		{
			MyBoid.Position = _position;
			MyBoid.Speed = Speed * 60.0f;
			MyBoid.Heading = Direction.ToVector2();
		}

		public void AddBehavior(EBehaviorType behaviorType, float weight)
		{
			MyBoid.AddBehavior(behaviorType, weight);
		}

		public void AddBehavior(IBehavior behavior)
		{
			MyBoid.AddBehavior(behavior);
		}

		public void RemoveBehavior(EBehaviorType behaviorType)
		{
			MyBoid.RemoveBehavior(behaviorType);
		}

		#endregion //Methods
	}
}
