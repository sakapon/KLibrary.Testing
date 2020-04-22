using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KLibrary.Testing
{
	/// <summary>
	/// Provides a set of helper methods for test methods.
	/// </summary>
	public static class TestHelper
	{
		public static Action<T, TResult> Create<T, TResult>(Func<T, TResult> target) =>
			(arg, expected) => Assert.AreEqual(expected, target(arg));

		public static Action<T1, T2, TResult> Create<T1, T2, TResult>(Func<T1, T2, TResult> target) =>
			(arg1, arg2, expected) => Assert.AreEqual(expected, target(arg1, arg2));

		public static Action<T1, T2, T3, TResult> Create<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> target) =>
			(arg1, arg2, arg3, expected) => Assert.AreEqual(expected, target(arg1, arg2, arg3));

		public static Action<object[], object> CreateAreEqual(Delegate target)
		{
			if (target == null) throw new ArgumentNullException(nameof(target));
			return (args, expected) => Assert.AreEqual(expected, target.DynamicInvoke(args));
		}

		public static Action<T, TResult> CreateForNearlyEqual<T, TResult>(Func<T, TResult> target)
		{
			if (typeof(TResult) == typeof(float))
				return (arg, expected) => Assert2.AreNearlyEqual((float)(object)expected, (float)(object)target(arg));
			else if (typeof(TResult) == typeof(double))
				return (arg, expected) => Assert2.AreNearlyEqual((double)(object)expected, (double)(object)target(arg));
			else if (typeof(TResult) == typeof(decimal))
				return (arg, expected) => Assert2.AreNearlyEqual((decimal)(object)expected, (decimal)(object)target(arg));
			else
				throw new InvalidOperationException("<TResult> is invalid.");
		}

		public static Action<T, TResult[]> CreateForCollection<T, TResult>(Func<T, TResult[]> target) =>
			(arg, expected) => CollectionAssert.AreEqual(expected, target(arg));
	}
}
