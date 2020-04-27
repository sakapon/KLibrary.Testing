using System;
using System.Diagnostics;

namespace KLibrary.Testing
{
	/// <summary>
	/// Provides a set of methods to measure execution time.
	/// </summary>
	public static class TimeHelper
	{
		/// <summary>
		/// Invokes the action and writes the execution time to the standard output.
		/// </summary>
		/// <param name="action">An action.</param>
		/// <param name="category">A category name. This is optional.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public static void Measure(Action action, string category = null)
		{
			if (action == null) throw new ArgumentNullException(nameof(action));

			var sw = Stopwatch.StartNew();
			action();
			sw.Stop();
			Console.WriteLine(string.IsNullOrEmpty(category) ? $"{sw.Elapsed}" : $"{category}: {sw.Elapsed}");
		}

		/// <summary>
		/// Invokes the function and writes the execution time to the standard output.
		/// </summary>
		/// <typeparam name="TResult">The type of the return value of the function.</typeparam>
		/// <param name="func">A function.</param>
		/// <param name="category">A category name. This is optional.</param>
		/// <returns>The return value of the function.</returns>
		/// <exception cref="ArgumentNullException"></exception>
		public static TResult Measure<TResult>(Func<TResult> func, string category = null)
		{
			if (func == null) throw new ArgumentNullException(nameof(func));

			var sw = Stopwatch.StartNew();
			var result = func();
			sw.Stop();
			Console.WriteLine(string.IsNullOrEmpty(category) ? $"{sw.Elapsed}" : $"{category}: {sw.Elapsed}");
			return result;
		}
	}
}
