using System;
using System.Collections.Generic;
using System.Linq;
using KLibrary.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Testing
{
	[TestClass]
	public class AssertHelperTest
	{
		[TestMethod]
		public void AreNearlyEqual()
		{
			double D9(int digits) => Enumerable.Range(1, digits).Sum(i => 1.0 / Math.Pow(10, i));

			// 1/9 = 0.1111...
			var expected = 1.0 / 9;
			var actual = D9(20);
			Assert.ThrowsException<AssertFailedException>(() => Assert.AreEqual(expected, actual));
			AssertHelper.AreNearlyEqual(expected, actual);

			// 0.1
			var actual1 = D9(1);
			Assert.ThrowsException<AssertFailedException>(() => AssertHelper.AreNearlyEqual(expected, actual1, 2));
			AssertHelper.AreNearlyEqual(expected, actual1, 1);

			// 0.11
			var actual2 = D9(2);
			Assert.ThrowsException<AssertFailedException>(() => AssertHelper.AreNearlyEqual(expected, actual2, 3));
			AssertHelper.AreNearlyEqual(expected, actual2, 2);
		}
	}
}
