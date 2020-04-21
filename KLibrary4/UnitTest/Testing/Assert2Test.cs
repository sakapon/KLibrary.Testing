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
		public void AreNearlyEqual_Double()
		{
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
		public void AreNearlyEqual_Decimal()
		{
			var target = 43.21M;

			// 10^-12
			Console.WriteLine(Assert.ThrowsException<AssertFailedException>(() => Assert2.AreNearlyEqual(target, 43.2099999999989M)).Message);
			Assert2.AreNearlyEqual(target, 43.209999999999M);
			Assert2.AreNearlyEqual(target, 43.21M);
			Assert2.AreNearlyEqual(target, 43.210000000001M);
			Console.WriteLine(Assert.ThrowsException<AssertFailedException>(() => Assert2.AreNearlyEqual(target, 43.2100000000011M)).Message);

			// 10^-1
			Console.WriteLine(Assert.ThrowsException<AssertFailedException>(() => Assert2.AreNearlyEqual(target, 43.1099M, -1)).Message);
			Assert2.AreNearlyEqual(target, 43.11M, -1);
			Assert2.AreNearlyEqual(target, 43.215M, -1);
			Assert2.AreNearlyEqual(target, 43.31M, -1);
			Console.WriteLine(Assert.ThrowsException<AssertFailedException>(() => Assert2.AreNearlyEqual(target, 43.3101M, -1)).Message);

			// 10^0
			Console.WriteLine(Assert.ThrowsException<AssertFailedException>(() => Assert2.AreNearlyEqual(target, 42.2099M, 0)).Message);
			Assert2.AreNearlyEqual(target, 42.21M, 0);
			Assert2.AreNearlyEqual(target, 43.215M, 0);
			Assert2.AreNearlyEqual(target, 44.21M, 0);
			Console.WriteLine(Assert.ThrowsException<AssertFailedException>(() => Assert2.AreNearlyEqual(target, 44.2101M, 0)).Message);
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
