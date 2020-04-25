using System;
using System.Reflection;

namespace KLibrary.Testing
{
	/// <summary>
	/// Provides a set of helper methods for test methods.
	/// </summary>
	public static class TestHelper
	{
		/// <summary>
		/// Creates a test method from the target function, that asserts the return value is equal to the expected value.
		/// Call the test method with arguments, those are the arguments of the target function and the expected value.
		/// </summary>
		/// <param name="func">The function to be tested. This must be a Func.</param>
		/// <returns>A test method.</returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException">The delegate does not return a value.</exception>
		public static Action<object[], object> CreateAreEqual(Delegate func)
		{
			if (func == null) throw new ArgumentNullException(nameof(func));
			if (func.Method.ReturnType == typeof(void)) throw new ArgumentException("The delegate must return a value, i.e. must be a Func.", nameof(func));
			return (args, expected) => Assert2.AreEqual(expected, func.Invoke(args));
		}

		/// <summary>
		/// Creates a test method from the target function, that asserts the return value is equal to the expected value.
		/// Call the test method with arguments, those are the arguments of the target function and the expected value.
		/// </summary>
		/// <typeparam name="T">The type of the parameter of the target function.</typeparam>
		/// <typeparam name="TResult">The type of the return value of the target function.</typeparam>
		/// <param name="func">The function to be tested.</param>
		/// <returns>A test method.</returns>
		/// <exception cref="ArgumentNullException"></exception>
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

		/// <summary>
		/// Creates a test method from the target function, that asserts the return value is nearly equal to the expected value.
		/// Call the test method with arguments, those are the arguments of the target function and the expected value.
		/// </summary>
		/// <param name="func">The function to be tested. This must be a Func.</param>
		/// <param name="digits">The digits that represent the upper bound of the absolute error between the two values.</param>
		/// <returns>A test method.</returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException">The delegate does not return a value.</exception>
		public static Action<object[], object> CreateAreNearlyEqual(Delegate func, int digits = -12)
		{
			if (func == null) throw new ArgumentNullException(nameof(func));
			if (func.Method.ReturnType == typeof(void)) throw new ArgumentException("The delegate must return a value, i.e. must be a Func.", nameof(func));
			return (args, expected) => Assert2.AreNearlyEqual(expected, func.Invoke(args), digits);
		}

		/// <summary>
		/// Creates a test method from the target function, that asserts the return value is nearly equal to the expected value.
		/// Call the test method with arguments, those are the arguments of the target function and the expected value.
		/// </summary>
		/// <typeparam name="T">The type of the parameter of the target function.</typeparam>
		/// <typeparam name="TResult">The type of the return value of the target function.</typeparam>
		/// <param name="func">The function to be tested.</param>
		/// <param name="digits">The digits that represent the upper bound of the absolute error between the two values.</param>
		/// <returns>A test method.</returns>
		/// <exception cref="ArgumentNullException"></exception>
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

		/// <exception cref="ArgumentException">The elements of the parameters array do not match the signature of the delegate.</exception>
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
