using App.Input;
using App.Ecs;
using App.Ecs.Components;

namespace App.Engine
{
	public enum UpdateResult
	{
		Updated,
		NoChange
	}

	public class Engine
	{
		const float TICK_HZ = 60.0f;
		const float TICK_RATE = 1.0f / TICK_HZ;
		const int MAX_TICKS_PER_UPDATE = 10;
		const int RENDER_CAPACITY = 100;

		private float _accumulatedTime = 0.0f;

		private InputManager _inputManager;

		public World world = new World();

		public Engine()
		{
			_inputManager = new InputManager();
		}

		/// <summary>
		/// Performs an update of the engine.
		/// This should be called often, and does not need to be a set rate.
		/// It will run the sim at 60hz though.
		/// </summary>
		/// <param name="deltaT"></param>
		public UpdateResult Update(float deltaT)
		{
			var result = UpdateResult.NoChange;

			// https://gafferongames.com/post/fix_your_timestep/
			_accumulatedTime += deltaT;

			for (int tick = 0; tick < MAX_TICKS_PER_UPDATE; tick++)
			{
				if (_accumulatedTime >= TICK_RATE)
				{
					_accumulatedTime -= TICK_RATE;
					result = UpdateResult.Updated;
					Tick();
				}
			}

			return result;
		}

		/// <summary>
		/// Executes a single tick of the engine.
		/// </summary>
		private void Tick()
		{
			// Update input on entities
			Controllable component;
			for (int i = 0; i < world.controllable.activeComponents; i++)
			{
				component = world.controllable.components[i];
				component.upHeld = _inputManager.InputKey(InputKey.KbW);
				component.downHeld = _inputManager.InputKey(InputKey.KbS);
				component.leftHeld = _inputManager.InputKey(InputKey.KbA);
				component.rightHeld = _inputManager.InputKey(InputKey.KbD);
			}

			world.Tick();
		}

		public InputManager Input()
		{
			return _inputManager;
		}

		public void Render(float deltaT)
		{
		}
	}
}
