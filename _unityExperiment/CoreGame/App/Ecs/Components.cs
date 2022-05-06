using App.Math;

namespace App.Ecs.Components
{
	public class Camera
	{
		public bool orthographic;

		public float xRotationDegrees;
		public float yRotationDegrees;
		public float zRotationDegrees;
	}

	public class Collideable { }

	public class Collision
	{
		public bool isColliding;
		public EntityId collidingEntity;
		public Vec2 normal = new Vec2();

		public void Reset()
		{
			isColliding = false;
			normal.x = 0;
			normal.y = 0;
		}
	}

	public class Controllable
	{
		public bool upHeld;
		public bool downHeld;
		public bool rightHeld;
		public bool leftHeld;
	}

	public class Vec2
	{
		public int x = 0;
		public int y = 0;
	}

	public class Aabb
	{
		public Vec2 size = new Vec2();
	}

	/// <summary>
	/// A renderable in Unity
	/// </summary>
	public class Renderable
	{
		public int z;
	}
}
