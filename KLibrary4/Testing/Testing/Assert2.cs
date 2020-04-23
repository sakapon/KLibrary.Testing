using System;
using System.Collections;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KLibrary.Testing
{
	/// <summary>
	/// Provides a set of methods to extend the <see cref="Assert"/> class.
	/// </summary>
	public static class Assert2
	{
		public static void AreEqual<T>(T expected, T actual)
		{
			if (expected is IEnumerable e && actual is IEnumerable a)
				CollectionAssert.AreEqual(e.AsCollection(), a.AsCollection());
			else
				Assert.AreEqual(expected, actual);
		}

		static ICollection AsCollection(this IEnumerable source) => source is ICollection c ? c : source?.Cast<object>()?.ToArray();

		// absolute error.
		public static void AreNearlyEqual(float expected, float actual, int digits = -6)
		{
			if (float.IsNaN(expected) || float.IsInfinity(expected)) throw new ArgumentOutOfRangeException(nameof(expected), expected, "<expected> is not a finite value.");
			if (float.IsNaN(actual) || float.IsInfinity(actual)) throw new AssertFailedException("AreNearlyEqual failed. <actual> is not a finite value.");

			AreNearlyEqual((decimal)expected, (decimal)actual, digits);
		}

		public static void AreNearlyEqual(double expected, double actual, int digits = -12)
		{
			if (double.IsNaN(expected) || double.IsInfinity(expected)) throw new ArgumentOutOfRangeException(nameof(expected), expected, "<expected> is not a finite value.");
			if (double.IsNaN(actual) || double.IsInfinity(actual)) throw new AssertFailedException("AreNearlyEqual failed. <actual> is not a finite value.");

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
