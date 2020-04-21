using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KLibrary.Testing
{
	/// <summary>
	/// Provides a set of methods to extend the <see cref="Assert"/> class.
	/// </summary>
	public static class Assert2
	{
		// absolute error.
		public static void AreNearlyEqual(float expected, float actual, int digits = -6)
		{
			AreNearlyEqual((decimal)expected, (decimal)actual, digits);
		}

		public static void AreNearlyEqual(double expected, double actual, int digits = -12)
		{
			AreNearlyEqual((decimal)expected, (decimal)actual, digits);
		}

		public static void AreNearlyEqual(decimal expected, decimal actual, int digits = -12)
		{
			var delta = Math.Abs(expected - actual);
			if (delta > decimal.Parse(digits < 0 ? $"0.{new string('0', -digits - 1)}1" : $"1{new string('0', digits)}"))
				throw new AssertFailedException($"AreNearlyEqual failed. | expected - actual |:<{delta}>.");
		}

		public static void IsOfType<T>(object obj)
		{
			if (!(obj is T)) throw new AssertFailedException($"The object is not {typeof(T)}.");
		}

		public static void IsNotOfType<T>(object obj)
		{
			if (obj is T) throw new AssertFailedException($"The object is {typeof(T)}.");
		}

		public static T Throws<T>(Action action) where T : Exception
		{
			if (action == null) throw new ArgumentNullException(nameof(action));

			try
			{
				action();
			}
			catch (T tex)
			{
				return tex;
			}
			catch (Exception ex)
			{
				throw new AssertFailedException($"{ex.GetType()} thrown, but {typeof(T)} was expected.", ex);
			}
			throw new AssertFailedException($"No exception thrown. {typeof(T)} was expected.");
		}
	}
}
