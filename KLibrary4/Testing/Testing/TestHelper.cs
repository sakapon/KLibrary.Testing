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

		public static Action<object[], object> CreateAreNearlyEqual(Delegate target, int digits = -12)
		{
			if (target == null) throw new ArgumentNullException(nameof(target));
			return (args, expected) => Assert2.AreNearlyEqual(expected, target.Invoke(args), digits);
		}

		public static Action<T, TResult> CreateAreNearlyEqual<T, TResult>(Func<T, TResult> target, int digits = -12)
		{
			if (target == null) throw new ArgumentNullException(nameof(target));
			return (arg, expected) => Assert2.AreNearlyEqual(expected, target(arg), digits);
		}

		public static Action<T1, T2, TResult> CreateAreNearlyEqual<T1, T2, TResult>(Func<T1, T2, TResult> target, int digits = -12)
		{
			if (target == null) throw new ArgumentNullException(nameof(target));
			return (arg1, arg2, expected) => Assert2.AreNearlyEqual(expected, target(arg1, arg2), digits);
		}

		public static Action<T1, T2, T3, TResult> CreateAreNearlyEqual<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> target, int digits = -12)
		{
			if (target == null) throw new ArgumentNullException(nameof(target));
			return (arg1, arg2, arg3, expected) => Assert2.AreNearlyEqual(expected, target(arg1, arg2, arg3), digits);
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
