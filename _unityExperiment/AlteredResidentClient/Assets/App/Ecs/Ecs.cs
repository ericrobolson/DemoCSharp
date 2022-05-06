using System;

namespace App.Ecs
{


	public struct EntityId : IEquatable<EntityId>
	{
		public readonly UInt64 id;

		public EntityId(UInt64 id)
		{
			this.id = id;
		}

		public bool Equals(EntityId other)
		{
			return id == other.id;
		}
	}
}
