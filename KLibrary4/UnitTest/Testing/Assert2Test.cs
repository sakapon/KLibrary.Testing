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
		public void AreNearlyEqual_Double()
		{
			// 10^-12
			Console.WriteLine(Assert.ThrowsException<AssertFailedException>(() => Assert2.AreNearlyEqual(43.21, 43.2099999999989)).Message);
			AssertHelper.AreNearlyEqual(43.21, 43.209999999999);
			AssertHelper.AreNearlyEqual(43.21, 43.21);
			AssertHelper.AreNearlyEqual(43.21, 43.210000000001);
			Console.WriteLine(Assert.ThrowsException<AssertFailedException>(() => Assert2.AreNearlyEqual(43.21, 43.2100000000011)).Message);

			// 10^-1
			Console.WriteLine(Assert.ThrowsException<AssertFailedException>(() => Assert2.AreNearlyEqual(43.21, 43.1099, 1)).Message);
			AssertHelper.AreNearlyEqual(43.21, 43.11, 1);
			AssertHelper.AreNearlyEqual(43.21, 43.215, 1);
			AssertHelper.AreNearlyEqual(43.21, 43.31, 1);
			Console.WriteLine(Assert.ThrowsException<AssertFailedException>(() => Assert2.AreNearlyEqual(43.21, 43.3101, 1)).Message);

			// 10^0
			Console.WriteLine(Assert.ThrowsException<AssertFailedException>(() => Assert2.AreNearlyEqual(43.21, 42.2099, 0)).Message);
			AssertHelper.AreNearlyEqual(43.21, 42.21, 0);
			AssertHelper.AreNearlyEqual(43.21, 43.215, 0);
			AssertHelper.AreNearlyEqual(43.21, 44.21, 0);
			Console.WriteLine(Assert.ThrowsException<AssertFailedException>(() => Assert2.AreNearlyEqual(43.21, 44.2101, 0)).Message);
		}

		[TestMethod]
		public void AreNearlyEqual_Double_9()
		{
			double D9(int digits) => Enumerable.Range(1, digits).Sum(i => Math.Pow(0.1, i));

			// 1/9 = 0.1111...
			var expected = 1.0 / 9;
			var actual = D9(20);
			Console.WriteLine(Assert.ThrowsException<AssertFailedException>(() => Assert.AreEqual(expected, actual)).Message);
			Assert2.AreNearlyEqual(expected, actual);
			Assert2.AreNearlyEqual(expected, actual, 15);
		}
	}
}
