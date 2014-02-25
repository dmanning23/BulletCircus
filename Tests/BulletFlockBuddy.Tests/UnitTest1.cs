using System;
using NUnit;
using NUnit.Framework;
using Moq;
using BulletFlockBuddy;
using Microsoft.Xna.Framework;
using BulletMLLib;

namespace BulletFlockBuddy.Tests
{
	public class UnitTest1
	{
		[Test]
		public void GetAimDir_fromBoid()
		{
			//create a fake bulletmanager
			var manager = new Mock<BulletBoidManager>(new PositionDelegate(() => { return Vector2.UnitX; })) { CallBase = true };

			//create the boid object
			var boid = new BulletBoid(manager.Object);
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
			var manager = new Mock<BulletBoidManager>(new PositionDelegate(() => { return Vector2.UnitX; })) { CallBase = true };

			//create the boid object
			var boid = new BulletBoid(manager.Object);
			boid.Init(Vector2.Zero, 10.0f, Vector2.UnitY, 1.0f, 1.0f);

			//set the heading of the boid object
			boid.MyBoid.Heading = Vector2.UnitY;

			//verify that the getaimdir method returns that same heading
			float rad = MathHelper.ToRadians(90.0f);
			float expected = (float)Math.Round(rad, 4);
			Assert.AreEqual(expected, boid.GetAimDir());
		}
	}
}
