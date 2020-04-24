using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KLibrary.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Testing
{
	[TestClass]
	public class TestHelperTest
	{
		[TestMethod]
		public void CreateAreEqual_Delegate_00()
		{
			var test = TestHelper.CreateAreEqual((Action)(() => Console.WriteLine("abc")));
			test(null, null);
		}

		[TestMethod]
		public void CreateAreEqual_Delegate_10()
		{
			// struct also works.
			(int, double) GetTuple() => (2, 3);

			var test = TestHelper.CreateAreEqual((Func<(int, double)>)GetTuple);
			test(null, (2, 3.0));
		}

		[TestMethod]
		public void CreateAreEqual_Delegate_14()
		{
			int IndexOf(string s, char value, int startIndex, int count) => s?.IndexOf(value, startIndex, count) ?? throw new ArgumentNullException(nameof(s));

			Assert.ThrowsException<ArgumentNullException>(() => TestHelper.CreateAreEqual(null));

			var test = TestHelper.CreateAreEqual((Func<string, char, int, int, int>)IndexOf);
			Assert.ThrowsException<ArgumentException>(() => test(new object[] { "abcde", "", 0, 0 }, null));
			Assert.ThrowsException<AssertFailedException>(() => test(new object[] { "abcde", 'c', 3, 2 }, 2));

			Assert.ThrowsException<ArgumentNullException>(() => test(new object[] { null, 'c', 0, 0 }, null));
			test(new object[] { "abcde", 'c', 0, 5 }, 2);
			test(new object[] { "abcde", 'c', 3, 2 }, -1);
		}

		[TestMethod]
		public void CreateAreEqual_Func_2()
		{
			Assert.ThrowsException<ArgumentNullException>(() => TestHelper.CreateAreEqual<double, int, double>(null));

			var test = TestHelper.CreateAreEqual<double, int, double>(Math.Round);
			Assert.ThrowsException<AssertFailedException>(() => test(1.23, 1, 0));

			Assert.ThrowsException<ArgumentOutOfRangeException>(() => test(1.23, -1, 0));
			test(1.23, 1, 1.2);
		}

		[TestMethod]
		public void CreateAreEqual_Delegate_Collection()
		{
			IEnumerable GetData()
			{
				yield return 1;
				yield return 2;
			}

			var test = TestHelper.CreateAreEqual((Func<IEnumerable>)GetData);
			Assert.ThrowsException<AssertFailedException>(() => test(null, 0));
			Assert.ThrowsException<AssertFailedException>(() => test(null, new[] { 0, 1, 2 }));
			test(null, new[] { 1, 2 });
		}

		[TestMethod]
		public void CreateAreEqual_Func_1_Collection()
		{
			Assert.ThrowsException<ArgumentNullException>(() => TestHelper.CreateAreEqual<int, double>(null));

			var test = TestHelper.CreateAreEqual<int, IEnumerable<int>>(count => Enumerable.Range(1, count));
			Assert.ThrowsException<AssertFailedException>(() => test(3, new[] { 0, 1, 2 }));

			Assert.ThrowsException<ArgumentOutOfRangeException>(() => test(-1, null));
			test(3, new[] { 1, 2, 3 });
		}

		[TestMethod]
		public void CreateAreNearlyEqual_Delegate_10()
		{
			double GetPi() => Math.PI;

			var test = TestHelper.CreateAreNearlyEqual((Func<double>)GetPi, -2);
			test(null, 3.15);

			var test2 = TestHelper.CreateAreNearlyEqual((Func<double>)GetPi, -3);
			Assert.ThrowsException<AssertFailedException>(() => test2(null, 3.15));
		}
	}
}
