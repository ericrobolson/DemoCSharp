using App.Ecs.Components;

namespace App.Ecs.Systems
{
	public class SysRender : ISystem
	{
		ComponentStore<Vec2> _position;
		ComponentStore<Vec2> _prevPosition;
		ComponentStore<Camera> _camera;
		ComponentStore<Renderable> _renderable;


		private Query<Vec2, Renderable> _query = new Query<Vec2, Renderable>();

		public SysRender(
			ref ComponentStore<Vec2> position,
			ref ComponentStore<Vec2> prevPosition,
			ref ComponentStore<Renderable> renderable,
			ref ComponentStore<Camera> camera)
		{
			_position = position;
			_renderable = renderable;
			_camera = camera;
			_prevPosition = prevPosition;
		}

		public void Reset()
		{
			_query.Reset();
		}

		public void Tick()
		{
			while (_query.Join(_position, _renderable))
			{
				// Copy positions to renderable
				var e = _query.Result();
			}
		}
	}
}

