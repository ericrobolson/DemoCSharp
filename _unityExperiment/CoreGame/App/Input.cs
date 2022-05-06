using System;
using System.Collections.Generic;

namespace App.Input
{
	/// <summary>
	/// Enumeration for on/off keys such as keyboards.
	/// </summary>
	public enum InputKey
	{
		KbW,
		KbA,
		KbS,
		KbD
	}

	/// <summary>
	/// Input manager.
	/// Uses polling.
	/// </summary>
	public class InputManager
	{
		private Dictionary<InputKey, bool> _keys = new Dictionary<InputKey, bool>();


		private UInt16 _mouseX = 0;
		private UInt16 _mouseY = 0;


		public InputManager() { }

		public void SetInputKey(InputKey key, bool on)
		{
			_keys.Remove(key);
			_keys.Add(key, on);
		}

		public bool InputKey(InputKey key)
		{
			var held = false;
			if (_keys.TryGetValue(key, out held))
			{
				return held;
			}

			return false;
		}

		public void SetMouse(UInt16 x, UInt16 y)
		{
			_mouseX = x;
			_mouseY = y;
		}

	}
}
