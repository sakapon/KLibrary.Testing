using System;
using System.Collections.Generic;
using System.Linq;
using KLibrary.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Testing
{
	[TestClass]
	public class Assert2Test
	{
		struct Assert2Struct
		{
			public int X;
			public double Y;
		}

		[TestMethod]
		public void AreEqual()
		{
			Console.WriteLine(Assert.ThrowsException<AssertFailedException>(() => Assert2.AreEqual(123, 456)).Message);
			Console.WriteLine(Assert.ThrowsException<AssertFailedException>(() => Assert2.AreEqual(new[] { 0, 1 }, new[] { 1, 2 })).Message);
			Console.WriteLine(Assert.ThrowsException<AssertFailedException>(() => Assert2.AreEqual("", null)).Message);
			Console.WriteLine(Assert.ThrowsException<AssertFailedException>(() => Assert2.AreEqual(null, Enumerable.Range(1, 10))).Message);
			Console.WriteLine(Assert.ThrowsException<AssertFailedException>(() => Assert2.AreEqual<object>(123, 123D)).Message);
			Console.WriteLine(Assert.ThrowsException<AssertFailedException>(() => Assert2.AreEqual<object>(new[] { 123 }, new[] { 123D })).Message);
			Console.WriteLine(Assert.ThrowsException<AssertFailedException>(() => Assert2.AreEqual<object>(0, Enumerable.Range(1, 10))).Message);
			Assert2.AreEqual<Uri>(null, null);
			Assert2.AreEqual<int[]>(null, null);
			Assert2.AreEqual(123, 123);
			Assert2.AreEqual<object>(123, 123);
			Assert2.AreEqual<object>(new int[0], new double[0]);
			Assert2.AreEqual(new[] { 1, 2, 3 }, Enumerable.Range(1, 3));
			Assert2.AreEqual<object>(new[] { 1, 2, 3 }, Enumerable.Range(1, 3));
			Assert2.AreEqual((2, new DateTime(2000, 1, 24)), (2, new DateTime(2000, 1, 23).AddDays(1)));
			Assert2.AreEqual(new Assert2Struct { X = 123, Y = 45.6 }, new Assert2Struct { X = 123, Y = 45.6 });
		}

		[TestMethod]
		public void Pow_Double()
		{
			for (int d = -60; d <= 60; d++)
				Console.WriteLine(double.Parse(d < 0 ? $"0.{new string('0', -d - 1)}1" : $"1{new string('0', d)}"));

			Assert.Inconclusive("See the output and check precision.");
		}

		[TestMethod]
		public void Pow_Decimal()
		{
			for (int d = -30; d <= 28; d++)
				Console.WriteLine(decimal.Parse(d < 0 ? $"0.{new string('0', -d - 1)}1" : $"1{new string('0', d)}"));

			Assert.AreNotEqual(0M, decimal.Parse($"0.{new string('0', 27)}1"));
			Assert.AreEqual(0M, decimal.Parse($"0.{new string('0', 28)}1"));
			Assert.Inconclusive("See the output and check precision.");
		}

		[TestMethod]
		public void AreNearlyEqual_Single_NaN()
		{
			var values = new[] { float.NaN, float.NegativeInfinity, float.PositiveInfinity };
			foreach (var v in values)
			{
				Console.WriteLine(Assert.ThrowsException<ArgumentOutOfRangeException>(() => Assert2.AreNearlyEqual(v, 0)).Message);
				Console.WriteLine(Assert.ThrowsException<AssertFailedException>(() => Assert2.AreNearlyEqual(0, v)).Message);
				Console.WriteLine();
			}
		}

		[TestMethod]
		public void AreNearlyEqual_Double_NaN()
		{
			var values = new[] { double.NaN, double.NegativeInfinity, double.PositiveInfinity };
			foreach (var v in values)
			{
				Console.WriteLine(Assert.ThrowsException<ArgumentOutOfRangeException>(() => Assert2.AreNearlyEqual(v, 0)).Message);
				Console.WriteLine(Assert.ThrowsException<AssertFailedException>(() => Assert2.AreNearlyEqual(0, v)).Message);
				Console.WriteLine();
			}
		}

		[TestMethod]
		public void AreNearlyEqual_Double()
		{
			// The test can be for other value types by casting.
			var target = 43.21;
			var values6 = new[] { 43.2099989, 43.209999, 43.21, 43.210001, 43.2100011 };
			var values1 = new[] { 43.1099, 43.11, 43.215, 43.31, 43.3101 };
			var values0 = new[] { 42.2099, 42.21, 43.215, 44.21, 44.2101 };

			BoundaryTest(values6, -6); // 10^-6
			BoundaryTest(values1, -1); // 10^-1
			BoundaryTest(values0, 0); // 10^0

			void BoundaryTest(double[] values, int digits)
			{
				Console.WriteLine(Assert.ThrowsException<AssertFailedException>(() => Assert2.AreNearlyEqual(target, values[0], digits)).Message);
				Assert2.AreNearlyEqual(target, values[1], digits);
				Assert2.AreNearlyEqual(target, values[2], digits);
				Assert2.AreNearlyEqual(target, values[3], digits);
				Console.WriteLine(Assert.ThrowsException<AssertFailedException>(() => Assert2.AreNearlyEqual(target, values[4], digits)).Message);
			}
		}

		[TestMethod]
		public void AreNearlyEqual_Double_9()
		{
			// A calculation with error.
			double D9(int digits) => Enumerable.Range(1, digits).Sum(i => Math.Pow(0.1, i));

			// 1/9 = 0.1111...
			var expected = 1.0 / 9;
			var actual = D9(20);
			Console.WriteLine(Assert.ThrowsException<AssertFailedException>(() => Assert.AreEqual(expected, actual)).Message);
			Assert2.AreNearlyEqual(expected, actual);
			// They are equal in the decimal.
			Assert2.AreNearlyEqual(expected, actual, -30);

			var actual12 = D9(12);
			Assert2.AreNearlyEqual(expected, actual12);
			Console.WriteLine(Assert.ThrowsException<AssertFailedException>(() => Assert2.AreNearlyEqual(expected, actual12, -13)).Message);
		}

		[TestMethod]
		public void AreNearlyEqual_Special()
		{
			var pi = Math.Sqrt(12) * Enumerable.Range(0, 40).Sum(i => 1 / ((2 * i + 1) * Math.Pow(-3, i)));
			// They are equal in the decimal.
			Assert2.AreNearlyEqual(Math.PI, pi, -30);

			var e = Math.Pow(1.00000001, 100000000);
			Assert2.AreNearlyEqual(Math.E, e, -7);
			Console.WriteLine(Assert.ThrowsException<AssertFailedException>(() => Assert2.AreNearlyEqual(Math.E, e, -8)).Message);
		}

		[TestMethod]
		public void IsOfType()
		{
			Console.WriteLine(Assert.ThrowsException<AssertFailedException>(() => Assert2.IsOfType<object>(null)).Message);
			Assert2.IsOfType<int>(1);
			Assert2.IsOfType<object>(1);
			Console.WriteLine(Assert.ThrowsException<AssertFailedException>(() => Assert2.IsOfType<long>(1)).Message);
		}

		[TestMethod]
		public void IsNotOfType()
		{
			Assert2.IsNotOfType<object>(null);
			Console.WriteLine(Assert.ThrowsException<AssertFailedException>(() => Assert2.IsNotOfType<int>(1)).Message);
			Console.WriteLine(Assert.ThrowsException<AssertFailedException>(() => Assert2.IsNotOfType<object>(1)).Message);
			Assert2.IsNotOfType<long>(1);
		}

		[TestMethod]
		public void Throws()
		{
			void Test(object obj)
			{
				if (obj == null) throw new ArgumentNullException(nameof(obj));
			}

			Console.WriteLine(Assert.ThrowsException<AssertFailedException>(() => Assert.ThrowsException<ArgumentNullException>(() => Test(0))).Message);
			Console.WriteLine(Assert.ThrowsException<AssertFailedException>(() => Assert.ThrowsException<ArgumentOutOfRangeException>(() => Test(null))).Message);
			Console.WriteLine(Assert.ThrowsException<AssertFailedException>(() => Assert.ThrowsException<ArgumentException>(() => Test(null))).Message);
			Console.WriteLine(Assert.ThrowsException<ArgumentNullException>(() => Test(null)).Message);
			Console.WriteLine();
			Console.WriteLine(Assert.ThrowsException<AssertFailedException>(() => Assert2.Throws<ArgumentNullException>(() => Test(0))).Message);
			Console.WriteLine(Assert.ThrowsException<AssertFailedException>(() => Assert2.Throws<ArgumentOutOfRangeException>(() => Test(null))).Message);
			Console.WriteLine(Assert2.Throws<ArgumentException>(() => Test(null)).Message);
			Console.WriteLine(Assert2.Throws<ArgumentNullException>(() => Test(null)).Message);
		}
	}
}
