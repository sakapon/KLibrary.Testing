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
		static ICollection AsCollection(this IEnumerable source) => source is ICollection c ? c : source?.Cast<object>()?.ToArray();

		/// <summary>
		/// Tests whether the specified values are equal and throws an exception if the two values are not equal,
		/// by calling the <see cref="CollectionAssert.AreEqual"/> method if the two values are <see cref="IEnumerable"/>;
		/// the <see cref="Assert.AreEqual&lt;T&gt;"/> method otherwise.
		/// </summary>
		/// <typeparam name="T">The type of values to compare.</typeparam>
		/// <param name="expected">The value the test expects.</param>
		/// <param name="actual">The value produced by the code under test.</param>
		/// <exception cref="AssertFailedException">Thrown if <paramref name="expected"/> is not equal to <paramref name="actual"/>.</exception>
		public static void AreEqual<T>(T expected, T actual)
		{
			if (expected is IEnumerable e && actual is IEnumerable a)
				CollectionAssert.AreEqual(e.AsCollection(), a.AsCollection());
			else
				Assert.AreEqual(expected, actual);
		}

		/// <summary>
		/// Tests whether the specified values are nearly equal and throws an exception if the two values are not nearly equal.
		/// <see cref="float"/>, <see cref="double"/> and <see cref="decimal"/> are supported.
		/// </summary>
		/// <typeparam name="T">The type of values to compare.</typeparam>
		/// <param name="expected">The value the test expects.</param>
		/// <param name="actual">The value produced by the code under test.</param>
		/// <param name="digits">The digits that represent the absolute error.</param>
		/// <exception cref="AssertFailedException">Thrown if <paramref name="expected"/> is not nearly equal to <paramref name="actual"/>.</exception>
		public static void AreNearlyEqual<T>(T expected, T actual, int digits = -12)
		{
			if (expected is float ef && actual is float af)
				AreNearlyEqual(ef, af, digits);
			else if (expected is double ed && actual is double ad)
				AreNearlyEqual(ed, ad, digits);
			else if (expected is decimal em && actual is decimal am)
				AreNearlyEqual(em, am, digits);
			else
				throw new AssertFailedException("The types are invalid.");
		}

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

		/// <summary>
		/// Tests whether the specified delegate action throws an exception of the specified type (or the derived type).
		/// </summary>
		/// <typeparam name="T">The type of the exception expected to be thrown.</typeparam>
		/// <param name="action">The delegate to be tested and which is expected to throw exception.</param>
		/// <returns>The exception that was thrown.</returns>
		/// <exception cref="AssertFailedException">Thrown if <paramref name="action"/> does not throw an exception of type <typeparamref name="T"/>.</exception>
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
