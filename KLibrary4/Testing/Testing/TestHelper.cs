using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KLibrary.Testing
{
	/// <summary>
	/// Provides a set of helper methods for test methods.
	/// </summary>
	public static class TestHelper
	{
		public static Action<object[], object> CreateAreEqual(Delegate func)
		{
			if (func == null) throw new ArgumentNullException(nameof(func));
			if (func.Method.ReturnType == typeof(void)) throw new ArgumentException("The delegate must return a value, i.e. must be a Func.", nameof(func));
			return (args, expected) => Assert2.AreEqual(expected, func.Invoke(args));
		}

		public static Action<T, TResult> CreateAreEqual<T, TResult>(Func<T, TResult> func)
		{
			if (func == null) throw new ArgumentNullException(nameof(func));
			return (arg, expected) => Assert2.AreEqual(expected, func(arg));
		}

		public static Action<T1, T2, TResult> CreateAreEqual<T1, T2, TResult>(Func<T1, T2, TResult> func)
		{
			if (func == null) throw new ArgumentNullException(nameof(func));
			return (arg1, arg2, expected) => Assert2.AreEqual(expected, func(arg1, arg2));
		}

		public static Action<T1, T2, T3, TResult> CreateAreEqual<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func)
		{
			if (func == null) throw new ArgumentNullException(nameof(func));
			return (arg1, arg2, arg3, expected) => Assert2.AreEqual(expected, func(arg1, arg2, arg3));
		}

		public static Action<object[], object> CreateAreNearlyEqual(Delegate func, int digits = -12)
		{
			if (func == null) throw new ArgumentNullException(nameof(func));
			if (func.Method.ReturnType == typeof(void)) throw new ArgumentException("The delegate must return a value, i.e. must be a Func.", nameof(func));
			return (args, expected) => Assert2.AreNearlyEqual(expected, func.Invoke(args), digits);
		}

		public static Action<T, TResult> CreateAreNearlyEqual<T, TResult>(Func<T, TResult> func, int digits = -12)
		{
			if (func == null) throw new ArgumentNullException(nameof(func));
			return (arg, expected) => Assert2.AreNearlyEqual(expected, func(arg), digits);
		}

		public static Action<T1, T2, TResult> CreateAreNearlyEqual<T1, T2, TResult>(Func<T1, T2, TResult> func, int digits = -12)
		{
			if (func == null) throw new ArgumentNullException(nameof(func));
			return (arg1, arg2, expected) => Assert2.AreNearlyEqual(expected, func(arg1, arg2), digits);
		}

		public static Action<T1, T2, T3, TResult> CreateAreNearlyEqual<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func, int digits = -12)
		{
			if (func == null) throw new ArgumentNullException(nameof(func));
			return (arg1, arg2, arg3, expected) => Assert2.AreNearlyEqual(expected, func(arg1, arg2, arg3), digits);
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
