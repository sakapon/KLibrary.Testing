using System;
using System.Linq;
using KLibrary.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Testing
{
	[TestClass]
	public class TimeHelperTest
	{
		[TestMethod]
		public void Measure()
		{
			var a = Enumerable.Range(0, 100000).Reverse().ToArray();
			var e = Enumerable.Range(0, 100000).Reverse().ToArray();

			a = TimeHelper.Measure(() => a.OrderBy(x => x).ToArray());
			TimeHelper.Measure(() => Array.Sort(e));
			CollectionAssert.AreEqual(e, a);

			Assert.Inconclusive("See the output.");
		}

		[TestMethod]
		public void Measure_Category()
		{
			var a = Enumerable.Range(0, 100000).Reverse().ToArray();
			var e = Enumerable.Range(0, 100000).Reverse().ToArray();

			a = TimeHelper.Measure(() => a.OrderBy(x => x).ToArray(), "LINQ");
			TimeHelper.Measure(() => Array.Sort(e), "Array");
			CollectionAssert.AreEqual(e, a);

			Assert.Inconclusive("See the output.");
		}
	}
}
