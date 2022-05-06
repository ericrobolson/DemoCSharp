using App.Ecs.Components;

namespace App.Ecs.Assemblages
{
	public static partial class Assemblage
	{
		/// <summary>
		/// Creates a new platformer camera
		/// </summary>
		/// <param name="world"></param>
		/// <returns></returns>
		public static bool CameraPlatformer(World world)
		{
			var result = true;

			var cam = world.AddEntity();
			result = world.position.Add(cam, out var posIdx) && result;
			world.position.components[posIdx].x = 0;
			world.position.components[posIdx].y = 0;
			result = world.controllable.Add(cam, out _) && result;

			result = world.prevPosition.Add(cam, out var prevIdx) && result;
			world.prevPosition.components[prevIdx] = new Vec2 { x = 0, y = 0 };
			world.prevPosition.components[prevIdx].x = world.position.components[posIdx].x;
			world.prevPosition.components[prevIdx].y = world.position.components[posIdx].y;

			result = world.renderable.Add(cam, out var camIdx) && result;
			world.renderable.components[camIdx].z = -10;
			result = world.camera.Add(cam, out camIdx) && result;
			world.camera.components[camIdx].orthographic = false;
			world.camera.components[camIdx].xRotationDegrees = 0.0f;
			world.camera.components[camIdx].yRotationDegrees = 0.0f;

			if (!result)
			{
				world.RemoveEntity(cam);
			}

			return result;
		}

		/// <summary>
		/// Creates a new isometric camera
		/// </summary>
		/// <param name="world"></param>
		/// <returns></returns>
		public static bool CameraIsometric(World world)
		{
			var result = true;

			var cam = world.AddEntity();
			result = world.position.Add(cam, out var posIdx) && result;
			world.position.components[posIdx].x = -5;
			world.position.components[posIdx].y = -5;
			result = world.controllable.Add(cam, out _) && result;

			result = world.prevPosition.Add(cam, out var prevIdx) && result;
			world.prevPosition.components[prevIdx] = new Vec2 { x = -5, y = -5 };

			result = world.renderable.Add(cam, out var camIdx) && result;
			world.renderable.components[camIdx].z = 5;
			result = world.camera.Add(cam, out camIdx) && result;
			world.camera.components[camIdx].orthographic = true;
			world.camera.components[camIdx].xRotationDegrees = 30.0f;
			world.camera.components[camIdx].yRotationDegrees = 45.0f;

			if (!result)
			{
				world.RemoveEntity(cam);
			}

			return result;
		}
	}
}
