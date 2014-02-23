using FlockBuddy;
using Vector2Extensions;
using Microsoft.Xna.Framework;

namespace BulletFlockBuddy
{
	/// <summary>
	/// This class wraps up BulletMLLib.Bullet with the FlockBuddy.Boid and CollisionBuddy.Circle
	/// </summary>
	public class BulletBoid : SimpleBullet
	{
		#region Members

		/// <summary>
		/// The flocking object this dude will use
		/// </summary>
		public Boid MyBoid { get; private set; }

		private BulletBoidManager BulletBoidManager { get; set; }

		#endregion //Members

		#region Methods

		/// <summary>
		/// Initializes a new instance of the <see cref="BulletBoid"/> class.
		/// </summary>
		/// <param name="myBulletManager">My bullet manager.</param>
		public BulletBoid(BulletBoidManager myBulletManager) : base(myBulletManager)
		{
			//store the bulelt boid manager
			BulletBoidManager = myBulletManager;
		}

		public void Init(Vector2 pos,
			float radius,
			Vector2 dir,
			float speed,
			float bulletmlScale)
		{
			base.Init(pos, radius, dir, speed, bulletmlScale);

			//create the boid
			MyBoid = new Boid(BulletBoidManager, pos, radius, dir, speed, 1.0f, 500.0f, 1.0f, 100.0f);

			//setup his behaviors
			MyBoid.Behaviors.ActivateBehaviors(new EBehaviorType[] {
					EBehaviorType.alignment,
					EBehaviorType.cohesion,
					EBehaviorType.separation,
					EBehaviorType.obstacle_avoidance,
					EBehaviorType.wall_avoidance
				});
		}

		public override void Update()
		{
			//the boid is updated in the mananger update.  that will give a target heading.

			//Next the bullet is updated.  If it uses an "aim" action, it will use the heading from the boid.
			base.Update();

			//Next the position of the boid is set to the updated position of the bullet.
			MyBoid.Position = _position;
			MyBoid.Speed = Speed * 60.0f;
			MyBoid.Heading = Direction.ToVector2();

			//Last, the position of the circle is updated.
			Physics.Pos = _position;
		}

		/// <summary>
		/// Get the direction the boid in us wants to go!
		/// </summary>
		/// <returns></returns>
		public override float GetAimDir()
		{
			return MyBoid.Rotation;
		}

		#endregion //Methods
	}
}
