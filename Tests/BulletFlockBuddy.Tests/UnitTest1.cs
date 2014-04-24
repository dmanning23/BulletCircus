using BulletMLLib;
using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;
using System;

namespace BulletCircus.Tests
{
	public class UnitTest1
	{
		[Test]
		public void GetAimDir_fromBoid()
		{
			//create a fake bulletmanager
			var manager = new Mock<SimpleMissileManager>(new PositionDelegate(() => { return Vector2.UnitX; })) { CallBase = true };

			//create the boid object
			var boid = new SimpleMissile(manager.Object);
			boid.Init(Vector2.Zero, 10.0f, Vector2.UnitY, 1.0f, 1.0f);

			//set the heading of the boid object
			boid.MyBoid.Heading = Vector2.UnitX;

			//verify that the getaimdir method returns that same heading
			float rad = MathHelper.ToRadians(0.0f);
			float expected = (float)Math.Round(rad, 4);
			Assert.AreEqual(expected, boid.GetAimDir());
		}

		[Test]
		public void GetAimDir_fromBoid_1()
		{
			//create a fake bulletmanager
			var manager = new Mock<SimpleMissileManager>(new PositionDelegate(() => { return Vector2.UnitX; })) { CallBase = true };

			//create the boid object
			var boid = new SimpleMissile(manager.Object);
			boid.Init(Vector2.Zero, 10.0f, Vector2.UnitY, 1.0f, 1.0f);

			//set the heading of the boid object
			boid.MyBoid.Heading = Vector2.UnitY;

			//verify that the getaimdir method returns that same heading
			float rad = MathHelper.ToRadians(90.0f);
			float expected = (float)Math.Round(rad, 4);
			Assert.AreEqual(expected, (float)Math.Round(boid.GetAimDir(), 4));
		}

		[Test]
		public void GetAimDir_fromBoid_2()
		{
			//create a fake bulletmanager
			var manager = new Mock<SimpleMissileManager>(new PositionDelegate(() => { return Vector2.UnitX; })) { CallBase = true };

			//create the boid object
			var boid = new SimpleMissile(manager.Object);
			boid.Init(Vector2.Zero, 10.0f, Vector2.UnitY, 1.0f, 1.0f);

			//set the heading of the boid object
			boid.MyBoid.Heading = -Vector2.UnitX;

			//verify that the getaimdir method returns that same heading
			float rad = MathHelper.ToRadians(-180.0f);
			float expected = (float)Math.Round(rad, 4);
			Assert.AreEqual(expected, (float)Math.Round(boid.GetAimDir(), 4));
		}

		[Test]
		public void Update_Bullet()
		{
			//create a fake bulletmanager
			var manager = new Mock<SimpleMissileManager>(new PositionDelegate(() => { return Vector2.UnitX; })) { CallBase = true };

			//create the boid object
			var boid = new SimpleMissile(manager.Object);
			boid.Init(Vector2.Zero, 10.0f, Vector2.UnitX, 1.0f, 1.0f);

			//update it once
			boid.Update();

			//verify everything updated
			Assert.AreEqual(1.0f, boid.Position.X);
			Assert.AreEqual(0.0f, boid.Position.Y);
			Assert.AreEqual(1.0f, boid.Speed);
			Assert.AreEqual(0.0f, boid.Direction);
		}

		[Test]
		public void Update_Boid()
		{
			//create a fake bulletmanager
			var manager = new Mock<SimpleMissileManager>(new PositionDelegate(() => { return Vector2.UnitX; })) { CallBase = true };

			//create the boid object
			var boid = new SimpleMissile(manager.Object);
			boid.Init(Vector2.Zero, 10.0f, Vector2.UnitX, 1.0f, 1.0f);

			//update it once
			boid.Update();

			//verify everything updated
			Assert.AreEqual(1.0f, boid.MyBoid.Position.X);
			Assert.AreEqual(0.0f, boid.MyBoid.Position.Y);
			Assert.AreEqual(60.0f, boid.MyBoid.Speed);
			Assert.AreEqual(0.0f, boid.MyBoid.Rotation);
		}

		[Test]
		public void Update_BulletBoid()
		{
			//create a fake bulletmanager
			var manager = new Mock<SimpleMissileManager>(new PositionDelegate(() => { return Vector2.UnitX; })) { CallBase = true };

			//create the boid object
			var boid = new SimpleMissile(manager.Object);
			boid.Init(Vector2.Zero, 10.0f, Vector2.UnitY, 1.0f, 1.0f);

			//update it once
			boid.Update();

			//verify everything updated
			Assert.AreEqual(boid.Position.X, boid.MyBoid.Position.X);
			Assert.AreEqual(boid.Position.Y, boid.MyBoid.Position.Y);
			Assert.AreEqual(boid.Speed * 60.0f, boid.MyBoid.Speed);
			Assert.AreEqual(boid.Direction, boid.MyBoid.Rotation);
		}

		[Test]
		public void Update_BulletBoid_1()
		{
			//create a fake bulletmanager
			var manager = new Mock<SimpleMissileManager>(new PositionDelegate(() => { return Vector2.UnitX; })) { CallBase = true };

			//create the boid object
			var boid = new SimpleMissile(manager.Object);
			boid.Init(new Vector2(100, 200), 10.0f, Vector2.UnitY, 1.0f, 1.0f);

			//update it once
			boid.Update();

			//verify everything updated
			Assert.AreEqual(boid.Position.X, boid.MyBoid.Position.X);
			Assert.AreEqual(boid.Position.Y, boid.MyBoid.Position.Y);
			Assert.AreEqual(boid.Speed * 60.0f, boid.MyBoid.Speed);
			Assert.AreEqual(boid.Direction, boid.MyBoid.Rotation);
		}

		[Test]
		public void Update_BulletBoid_2()
		{
			//create a fake bulletmanager
			var manager = new Mock<SimpleMissileManager>(new PositionDelegate(() => { return Vector2.UnitX; })) { CallBase = true };

			//create the boid object
			var boid = new SimpleMissile(manager.Object);
			boid.Init(new Vector2(100, 200),
				10.0f, 
				new Vector2(10, -20), 11.0f, 1.0f);

			//update it once
			boid.Update();

			//verify everything updated
			Assert.AreEqual(boid.Position.X, boid.MyBoid.Position.X);
			Assert.AreEqual(boid.Position.Y, boid.MyBoid.Position.Y);
			Assert.AreEqual(boid.Speed * 60.0f, boid.MyBoid.Speed);
			Assert.AreEqual(boid.Direction, boid.MyBoid.Rotation);
		}
	}
}
