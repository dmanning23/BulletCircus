using FlockBuddy;
using System.Threading.Tasks;
using Vector2Extensions;
using Microsoft.Xna.Framework;
using BasicPrimitiveBuddy;

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

		public override void Init(Vector2 pos,
			float radius,
			Vector2 dir,
			float speed,
			float bulletmlScale)
		{
			base.Init(pos, radius, dir, speed, bulletmlScale);

			//create the boid
			MyBoid = new Boid(BulletBoidManager, pos, radius, dir, speed, 1.0f, 500.0f, 10.0f, 100.0f);

			//setup his behaviors
			MyBoid.Behaviors.ActivateBehaviors(new EBehaviorType[] {
					EBehaviorType.alignment,
					EBehaviorType.cohesion,
					EBehaviorType.separation,
					EBehaviorType.obstacle_avoidance,
					EBehaviorType.wall_avoidance,
					EBehaviorType.pursuit,
					EBehaviorType.flee
				});

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
