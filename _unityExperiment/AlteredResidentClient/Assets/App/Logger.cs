using System;

namespace App
{
	public static class Logger
	{
		private static Action<string>? _log;


		public static void SetLogger(Action<string> log)
		{
			_log = log;
		}

		public static void Log(string log)
		{
			if (_log != null)
			{
				_log(log);
			}
		}
	}
}
