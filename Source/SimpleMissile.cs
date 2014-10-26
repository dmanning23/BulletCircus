using BasicPrimitiveBuddy;
using FlockBuddy;
using Microsoft.Xna.Framework;
using Vector2Extensions;

namespace BulletCircus
{
	/// <summary>
	/// This class wraps up BulletMLLib.Bullet with the FlockBuddy.Boid and CollisionBuddy.Circle
	/// </summary>
	public class SimpleMissile : SimpleBullet, IMover
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
		}

		public override void Init(Vector2 pos,
			float radius,
			Vector2 dir,
			float speed,
			float bulletmlScale,
			Vector2 initialVelocity)
		{
			//create the boid
			MyBoid = new Boid(SimpleMissileManager, pos, radius, dir, speed);

			//setup his behaviors
			MyBoid.Behaviors.ActivateBehaviors(new EBehaviorType[] {
					EBehaviorType.alignment,
					EBehaviorType.cohesion,
					EBehaviorType.separation,
					EBehaviorType.obstacle_avoidance,
					EBehaviorType.wall_avoidance,
					EBehaviorType.pursuit,
					EBehaviorType.evade
				});

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

		public override void Render(IBasicPrimitive prim, Color color)
		{
			base.Render(prim, color);
			MyBoid.Render(prim, color);
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

		#endregion //Methods
	}
}
