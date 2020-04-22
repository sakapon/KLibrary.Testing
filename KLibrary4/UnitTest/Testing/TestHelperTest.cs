using System;
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
			int Get123() => 123;

			var test = TestHelper.CreateAreEqual((Func<int>)Get123);
			test(null, 123);
		}

		[TestMethod]
		public void CreateAreEqual_Delegate_14()
		{
			int IndexOf(string s, char value, int startIndex, int count) => s.IndexOf(value, startIndex, count);

			var test = TestHelper.CreateAreEqual((Func<string, char, int, int, int>)IndexOf);
			test(new object[] { "abcde", 'c', 0, 5 }, 2);
			test(new object[] { "abcde", 'c', 3, 2 }, -1);
		}
	}
}
