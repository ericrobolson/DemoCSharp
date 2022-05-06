namespace App.Ecs.Assemblages
{
	public static partial class Assemblage
	{

		public static bool Controllable(World world)
		{
			var result = true;

			var e = world.AddEntity();
			result = world.position.Add(e, out _) && result;
			result = world.prevPosition.Add(e, out _) && result; ;
			result = world.aabb.Add(e, out var aabbIdx) && result; ;
			result = world.renderable.Add(e, out _) && result;
			result = world.controllable.Add(e, out _) && result;
			world.aabb.components[aabbIdx].size.x = 2;
			world.aabb.components[aabbIdx].size.y = 2;

			if (!result)
			{
				world.RemoveEntity(e);
			}

			return result;
		}
	}
}
