using System;
using System.Collections.Generic;
using System.Linq;
using KLibrary.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Testing
{
	[TestClass]
	public class RandomHelperTest
	{
		[TestMethod]
		public void CreateData()
		{
			var actual1 = RandomHelper.CreateData(100);
			var actual2 = RandomHelper.CreateData(100, -520, -500);
			var actual3 = RandomHelper.CreateData(50, 300, 800);
			var actual4 = RandomHelper.CreateData(50, Math.PI / 2, Math.PI);

			Assert.Inconclusive("Start debugging and check actual data.");
		}

		[TestMethod]
		public void ShuffleRange()
		{
			var set = Enumerable.Range(-10, 30).ToHashSet();
			var actual = RandomHelper.ShuffleRange(-10, 30);

			foreach (var v in actual)
				Console.WriteLine(v);
			Assert.IsTrue(set.SetEquals(actual));
		}
	}
}
