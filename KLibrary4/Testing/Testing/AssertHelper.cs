﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KLibrary.Testing
{
	/// <summary>
	/// Provides a set of methods to extend the <see cref="Assert"/> class.
	/// </summary>
	public static class AssertHelper
	{
		public static void AreNearlyEqual(double expected, double actual, int digits = 12)
		{
			if (Math.Round(expected - actual, digits) != 0)
				throw new AssertFailedException($"AreNearlyEqual failed. expected - actual:<{expected - actual}>.");
		}

		public static Action<T, TResult> Create<T, TResult>(Func<T, TResult> target) =>
			(arg, expected) => Assert.AreEqual(expected, target(arg));

		public static Action<T1, T2, TResult> Create<T1, T2, TResult>(Func<T1, T2, TResult> target) =>
			(arg1, arg2, expected) => Assert.AreEqual(expected, target(arg1, arg2));

		public static Action<T1, T2, T3, TResult> Create<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> target) =>
			(arg1, arg2, arg3, expected) => Assert.AreEqual(expected, target(arg1, arg2, arg3));

		public static Action<T, TResult[]> CreateForCollection<T, TResult>(Func<T, TResult[]> target) =>
			(arg, expected) => CollectionAssert.AreEqual(expected, target(arg));
	}
}