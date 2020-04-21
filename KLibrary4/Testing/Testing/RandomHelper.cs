using System;
using System.Collections.Generic;
using System.Linq;

namespace KLibrary.Testing
{
	public static class RandomHelper
	{
		public static Random Random { get; } = new Random();

		static void Swap<T>(T[] a, int i, int j) { var o = a[i]; a[i] = a[j]; a[j] = o; }

		public static double NextDouble(double minValue, double maxValue) =>
			minValue + (maxValue - minValue) * Random.NextDouble();

		public static int[] CreateData(int count) => CreateData(count, 0, count);
		public static int[] CreateData(int count, int minValue, int maxValue)
		{
			var a = new int[count];
			for (var i = 0; i < count; ++i)
				a[i] = Random.Next(minValue, maxValue);
			return a;
		}

		public static double[] CreateData(int count, double minValue, double maxValue)
		{
			var a = new double[count];
			for (var i = 0; i < count; ++i)
				a[i] = NextDouble(minValue, maxValue);
			return a;
		}

		public static IEnumerable<TSource> Shuffle<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));

			var a = source.ToArray();
			for (var i = a.Length - 1; i >= 0; --i)
			{
				var index = Random.Next(i + 1);
				yield return a[index];
				Swap(a, i, index);
			}
		}

		public static int[] ShuffleRange(int start, int count) => Enumerable.Range(start, count).Shuffle().ToArray();
	}
}
