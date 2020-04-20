using System;
using System.Diagnostics;

namespace KLibrary.Testing
{
	public static class TimeHelper
	{
		public static void Measure(Action action, string category = null)
		{
			if (action == null) throw new ArgumentNullException(nameof(action));

			var sw = Stopwatch.StartNew();
			action();
			sw.Stop();
			Console.WriteLine(string.IsNullOrEmpty(category) ? $"{sw.Elapsed}" : $"{category}: {sw.Elapsed}");
		}

		public static T Measure<T>(Func<T> func, string category = null)
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
