using Microsoft.VisualStudio.TestTools.UnitTesting;
using QBRating.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace QBRating.ViewModels.Tests {
	[TestClass()]
	public class NotifyDataErrorInfoTests {
		private static TestAbstract _testClass;

		[ClassInitialize]
		public static void TestClassSetup(TestContext testContext) {
			_testClass = new TestAbstract();
		}

		[TestMethod()]
		public void NonNegWholeIntField_Null_True() {
			Assert.IsTrue(_testClass.NonNegWholeIntField(null));
		}
		[TestMethod()]
		public void NonNegWholeIntField_0_True() {
			Assert.IsTrue(_testClass.NonNegWholeIntField(0));
		}
		[TestMethod()]
		public void NonNegWholeIntField_1_True() {
			Assert.IsTrue(_testClass.NonNegWholeIntField(1));
		}
		[TestMethod()]
		public void NonNegWholeIntField_Neg1_False() {
			Assert.IsFalse(_testClass.NonNegWholeIntField(-1));
		}

		[TestMethod()]
		public void NotLargerThanDependentField_4And10_True() {
			Assert.IsTrue(_testClass.NotLargerThanDependentField(4, 10));
		}
		[TestMethod()]
		public void NotLargerThanDependentField_10And10_True() {
			Assert.IsTrue(_testClass.NotLargerThanDependentField(10, 10));
		}
		[TestMethod()]
		public void NotLargerThanDependentField_11And10_False() {
			Assert.IsFalse(_testClass.NotLargerThanDependentField(11, 10));
		}
		[TestMethod()]
		public void NotLargerThanDependentField_NullAnd10_True() {
			Assert.IsTrue(_testClass.NotLargerThanDependentField(null, 10));
		}
		[TestMethod()]
		public void NotLargerThanDependentField_4AndNull_True() {
			Assert.IsTrue(_testClass.NotLargerThanDependentField(4, null));
		}

		[TestMethod()]
		public void FieldLessThan99TimesDependent_99And1_True() {
			Assert.IsTrue(_testClass.FieldLessThan99TimesDependent(99, 1));
		}
		[TestMethod()]
		public void FieldLessThan99TimesDependent_NullAnd1_True() {
			Assert.IsTrue(_testClass.FieldLessThan99TimesDependent(null, 1));
		}
		[TestMethod()]
		public void FieldLessThan99TimesDependent_98AndNull_True() {
			Assert.IsTrue(_testClass.FieldLessThan99TimesDependent(98, null));
		}
		[TestMethod()]
		public void FieldLessThan99TimesDependent_100And1_False() {
			Assert.IsFalse(_testClass.FieldLessThan99TimesDependent(100, 1));
		}
	}

	public class TestAbstract : NotifyDataErrorInfo<TestAbstract> {}
}