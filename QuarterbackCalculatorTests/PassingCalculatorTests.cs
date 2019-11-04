using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QuarterbackCalculator.Tests {
	[TestClass()]
	public class PassingCalculatorTests {
		
		[TestMethod()]
		public void NCAAPassRating_Worst_LowerLimit() {
			//arrange
			var calculator = new PassingCalculator(100, 100, -9900, 0, 0);

			//act
			var result = calculator.NCAAPassRating();

			//assert
			Assert.AreEqual(-731.6, result);
		}

		[TestMethod()]
		public void NCAAPassRating_AlmostWorst_GreaterThanLowerLimit() {
			//arrange
			var calculator = new PassingCalculator(100, 100, -9899, 0, 0);

			//act
			var result = calculator.NCAAPassRating();

			//assert
			Assert.IsTrue(-731.6 < result);
		}

		[TestMethod()]
		public void NCAAPassRating_InterceptionsOnly_Negative200() {
			//arrange
			var calculator = new PassingCalculator(100, 0, 0, 0, 100);

			//act
			var result = calculator.NCAAPassRating();

			//assert
			Assert.AreEqual(-200, result);
		}

		[TestMethod()]
		public void NCAAPassRating_Best_UpperLimit() {
			//arrange
			var calculator = new PassingCalculator(100, 100, 9900, 100, 0);

			//act
			var result = calculator.NCAAPassRating();

			//assert
			Assert.AreEqual(1261.6, result);
		}

		[TestMethod()]
		public void NCAAPassRating_AlmostBest_LessThanUpperLimit() {
			//arrange
			var calculator = new PassingCalculator(100, 100, 9899, 100, 0);

			//act
			var result = calculator.NCAAPassRating();

			//assert
			Assert.IsTrue(1261.6 > result);
		}

		[TestMethod()]
		public void NFLPassRating_ReachedPerfect_BestScore() {
			//arrange
			var calculator = new PassingCalculator(1000, 775, 12500, 120, 0);

			//act
			var result = calculator.NFLPassRating();

			//assert
			Assert.IsTrue(158.3 <= result && 158.4 > result);
		}

		[TestMethod()]
		public void NFLPassRating_BetterThanPerfect_BestScore() {
			//arrange
			var calculator = new PassingCalculator(1000, 875, 12500, 150, 0);

			//act
			var result = calculator.NFLPassRating();

			//assert
			Assert.IsTrue(158.3 <= result && 158.4 > result);
		}

		[TestMethod()]
		public void NFLPassRating_NotQuitePerfect_LessThanBestScore() {
			//arrange
			var calculator = new PassingCalculator(1000, 774, 12500, 150, 0);

			//act
			var result = calculator.NFLPassRating();

			//assert
			Assert.IsTrue(158.3 > result);
		}

		[TestMethod()]
		public void NFLPassRating_NotQuiteWorst_BetterThanWorstScore() {
			//arrange
			var calculator = new PassingCalculator(1000, 301, 3001, 0, 94);

			//act
			var result = calculator.NFLPassRating();

			//assert
			Assert.IsTrue(0 < result);
		}

		[TestMethod()]
		public void NFLPassRating_JustWorstEnough_WorstScore() {
			//arrange
			var calculator = new PassingCalculator(1000, 300, 3000, 0, 95);

			//act
			var result = calculator.NFLPassRating();

			//assert
			Assert.AreEqual(0, result);
		}

		[TestMethod()]
		public void NFLPassRating_MoreThanWorstEnough_WorstScore() {
			//arrange
			var calculator = new PassingCalculator(1000, 299, 2999, 0, 96);

			//act
			var result = calculator.NFLPassRating();

			//assert
			Assert.AreEqual(0, result);
		}
	}
}