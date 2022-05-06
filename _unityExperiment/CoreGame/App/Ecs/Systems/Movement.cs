using App.Ecs.Components;

namespace App.Ecs.Systems
{
	public class SysMovement : ISystem
	{
		ComponentStore<Vec2> _position;
		ComponentStore<Vec2> _prevPosition;


		private Query<Vec2, Vec2> _query = new Query<Vec2, Vec2>();

		public SysMovement(ref ComponentStore<Vec2> position, ref ComponentStore<Vec2> prevPosition)
		{
			_position = position;
			_prevPosition = prevPosition;
		}

		public void Reset()
		{
			_query.Reset();
		}

		public void Tick()
		{
			while (_query.Join(_position, _prevPosition))
			{
				// Copy position to prev position
				var e = _query.Result();

				_prevPosition.components[e.rightComponentIndex].x = _position.components[e.leftComponentIndex].x;
				_prevPosition.components[e.rightComponentIndex].y = _position.components[e.leftComponentIndex].y;
			}
		}
	}
}
