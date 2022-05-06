/*
ALGORITHM:
    Xn+1 = (aXn + c) mod m
    where X is the sequence of pseudo-random values
    m, 0 < m  - modulus
    a, 0 < a < m  - multiplier
    c, 0 ≤ c < m  - increment
    x0, 0 ≤ x0 < m  - the seed or start value
*/

using System;

namespace App.Math
{
	using N = UInt64;

	/// <summary>
	/// Psuedo random number generator.
	/// </summary>
	public class Random
	{
		static N MODULUS = N.MaxValue;
		static N MULTIPLIER = 1103515245;
		static N INCREMENT = 12345;

		private N _value;
		private readonly N _seed;

		/// <summary>
		/// Creates a random number generator when given a seed.
		/// </summary>
		/// <param name="seed"></param>
		public Random(UInt32 seed)
		{
			_value = seed;
			_seed = seed;
		}

		/// <summary>
		/// Calculates a new random number.
		/// </summary>
		/// <returns></returns>
		private N Calculate()
		{
			var value = (_seed * MULTIPLIER * _value + INCREMENT) % MODULUS;
			_value = value;

			return value;
		}

		/// <summary>
		/// Returns a random byte.
		/// </summary>
		/// <returns></returns>
		public byte RandomByte()
		{
			var n = Calculate() % (N)byte.MaxValue;
			return (byte)n;
		}


		/// <summary>
		/// Returns a random UInt16.
		/// </summary>
		/// <returns></returns>
		public UInt16 RandomUInt16()
		{
			var n = Calculate() % (N)UInt16.MaxValue;
			return (UInt16)n;
		}

		/// <summary>
		/// Returns a random UInt32.
		/// </summary>
		/// <returns></returns>
		public UInt32 RandomUInt32()
		{
			var n = Calculate() % (N)UInt32.MaxValue;
			return (UInt32)n;
		}

		/// <summary>
		/// Returns a random SByte.
		/// </summary>
		/// <returns></returns>
		public sbyte RandomSByte()
		{
			const N RANGE = (N)((Int16)sbyte.MaxValue - (Int16)sbyte.MinValue);

			var n = Calculate() % RANGE;
			var y = (Int16)n + (Int16)sbyte.MinValue;

			return (sbyte)y;
		}

		/// <summary>
		/// Returns a random Int16.
		/// </summary>
		/// <returns></returns>
		public Int16 RandomInt16()
		{
			const N RANGE = (N)((Int32)Int16.MaxValue - (Int32)Int16.MinValue);

			var n = Calculate() % RANGE;
			var y = (Int32)n + (Int32)Int16.MinValue;

			return (Int16)y;
		}

		/// <summary>
		/// Returns a random Int32.
		/// </summary>
		/// <returns></returns>
		public Int32 RandomInt32()
		{
			const N RANGE = (N)((Int64)Int32.MaxValue - (Int64)Int32.MinValue);

			var n = Calculate() % RANGE;
			var y = (Int64)n + (Int64)Int32.MinValue;

			return (Int32)y;
		}
	}
}
