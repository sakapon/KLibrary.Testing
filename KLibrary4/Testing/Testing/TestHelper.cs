using System;
using System.Reflection;

namespace KLibrary.Testing
{
	/// <summary>
	/// Provides a set of helper methods for test methods.
	/// </summary>
	public static class TestHelper
	{
		public static Action<object[], object> CreateAreEqual(Delegate target)
		{
			if (target == null) throw new ArgumentNullException(nameof(target));
			return (args, expected) => Assert2.AreEqual(expected, target.Invoke(args));
		}

		public static Action<T, TResult> CreateAreEqual<T, TResult>(Func<T, TResult> target)
		{
			if (target == null) throw new ArgumentNullException(nameof(target));
			return (arg, expected) => Assert2.AreEqual(expected, target(arg));
		}

		public static Action<T1, T2, TResult> CreateAreEqual<T1, T2, TResult>(Func<T1, T2, TResult> target)
		{
			if (target == null) throw new ArgumentNullException(nameof(target));
			return (arg1, arg2, expected) => Assert2.AreEqual(expected, target(arg1, arg2));
		}

		public static Action<T1, T2, T3, TResult> CreateAreEqual<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> target)
		{
			if (target == null) throw new ArgumentNullException(nameof(target));
			return (arg1, arg2, arg3, expected) => Assert2.AreEqual(expected, target(arg1, arg2, arg3));
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

		/// <exception cref="ArgumentException">The method represented by the delegate is invoked on an object or a class that does not support it.</exception>
		/// <exception cref="Exception">Any other exception on target invocation.</exception>
		static object Invoke(this Delegate target, object[] args)
		{
			try
			{
				return target.DynamicInvoke(args);
			}
			catch (TargetInvocationException ex)
			{
				throw ex.InnerException;
			}
		}
	}
}
