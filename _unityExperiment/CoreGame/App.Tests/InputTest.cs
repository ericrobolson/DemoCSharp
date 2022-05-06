using Xunit;
using App.Input;
using System.Collections.Generic;

namespace App.Tests
{

	public class InputTests
	{
		InputManager input = new InputManager();

		[Fact]
		public void GetOnKeyReturnsFalseByDefault()
		{
			var inputs = new List<InputKey>(){
				InputKey.KbW,
				InputKey.KbA,
				InputKey.KbS,
				InputKey.KbD
			};

			foreach (var key in inputs)
			{
				Assert.Equal((key, false), (key, input.InputKey(key)));
			}
		}

		[Fact]
		public void GetOnKeyReturnsTrueWhenSet()
		{
			var inputs = new List<InputKey>(){
				InputKey.KbW,
				InputKey.KbA,
				InputKey.KbS,
				InputKey.KbD
			};

			foreach (var key in inputs)
			{
				Assert.Equal((key, false), (key, input.InputKey(key)));

				input.SetInputKey(key, true);
				Assert.Equal((key, true), (key, input.InputKey(key)));

				input.SetInputKey(key, false);
				Assert.Equal((key, false), (key, input.InputKey(key)));
			}
		}
	}


}
