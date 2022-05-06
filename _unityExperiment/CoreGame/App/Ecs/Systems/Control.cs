using App.Ecs.Components;

namespace App.Ecs.Systems
{
	public class SysControl : ISystem
	{
		ComponentStore<Vec2> _position;
		ComponentStore<Controllable> _controllable;


		private Query<Vec2, Controllable> _query = new Query<Vec2, Controllable>();

		public SysControl(ref ComponentStore<Vec2> position, ref ComponentStore<Controllable> controllable)
		{
			_position = position;
			_controllable = controllable;
		}

		public void Reset()
		{
			_query.Reset();
		}

		public void Tick()
		{
			while (_query.Join(_position, _controllable))
			{
				// Copy position to prev position
				var e = _query.Result();


				if (_controllable.components[e.rightComponentIndex].upHeld)
				{
					_position.components[e.leftComponentIndex].y += 1;
				}
				else if (_controllable.components[e.rightComponentIndex].downHeld)
				{
					_position.components[e.leftComponentIndex].y -= 1;
				}

				if (_controllable.components[e.rightComponentIndex].leftHeld)
				{
					_position.components[e.leftComponentIndex].x -= 1;
				}
				else if (_controllable.components[e.rightComponentIndex].rightHeld)
				{
					_position.components[e.leftComponentIndex].x += 1;
				}
			}
		}
	}
}
