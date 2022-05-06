namespace App.Ecs
{
	public struct JoinResult
	{
		public EntityId entity;
		/// <summary>
		/// The component index for a.
		/// </summary>
		public int leftComponentIndex;
		/// <summary>
		/// The component index for b.
		/// </summary>
		public int rightComponentIndex;
	}

	public struct Query<Left, Right> where Left : new() where Right : new()
	{
		private int _idxLeft;
		private int _idxRight;
		private bool _finished;
		private bool _increment;
		private JoinResult _joinResult;

		public void Reset()
		{
			_idxLeft = 0;
			_idxRight = 0;
			_finished = false;
			_increment = false;
		}


		public JoinResult Result()
		{
			return _joinResult;
		}

		/// <summary>
		/// Joinable iterator.
		/// </summary>

		public bool Join(ComponentStore<Left> left, ComponentStore<Right> right)
		{
			while (!_finished)
			{
				// Don't increment first time
				if (_increment)
				{
					_idxRight += 1;
				}
				_increment = true;


				if (_idxRight >= right.activeComponents)
				{
					_idxLeft += 1;
					_idxRight = 0;
				}

				if (_idxLeft >= left.activeComponents)
				{
					_finished = true;
				}
				else if (left.entities[_idxLeft].Equals(right.entities[_idxRight]))
				{
					_joinResult.leftComponentIndex = _idxLeft;
					_joinResult.rightComponentIndex = _idxRight;
					_joinResult.entity = left.entities[_idxLeft];

					return true;
				}
			}





			return false;
		}
	}
}
