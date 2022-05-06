namespace App.Math
{
	/// <summary>
	/// Helper math methods.
	/// </summary>
	public static class Util
	{
		/// <summary>
		/// Lerps a value.
		/// </summary>
		/// <param name="start">The start value.</param>
		/// <param name="end">The end value.</param>
		/// <param name="by">0.0 - 1.0 range</param>
		/// <returns></returns>
		public static float Lerp(float start, float end, float by)
		{
			return start * (1.0f - by) + end * by;
		}
	}
}
