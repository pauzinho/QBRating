using System;

namespace QuarterbackCalculator {
	public class PassingCalculator {
		public PassingCalculator() { }
		public PassingCalculator(int attempts, int completions, int yards, int touchdowns, int interceptions) {
			ThrowAttempts = attempts;
			PassCompletion = completions;
			PassingYards = yards;
			Touchdowns = touchdowns;
			Interceptions = interceptions;
		}

		public double NCAAPassRating() {
			return ((PassingYards * 8.4) + (Touchdowns * 330) + (PassCompletion * 100) - (Interceptions * 200)) / ThrowAttempts;
		}

		public double NFLPassRating() {
			var completionComp = NflCompeletionComponent();
			var yardGainComp = NflYardGainComponent();
			var touchdownComp = NflTouchdownComponent();
			var interceptComp = NflInterceptComponent();
			return ((completionComp + yardGainComp + touchdownComp + interceptComp) / (double)6) * 100;
		}

		private double NflCompeletionComponent() {
			var component = ((double)PassCompletion / ThrowAttempts - .3) * 5;
			return NflComponentLimitCheck(component);
		}

		private double NflYardGainComponent() {
			var component = ((double)PassingYards / ThrowAttempts - 3) * .25;
			return NflComponentLimitCheck(component);
		}

		private double NflTouchdownComponent() {
			var component = ((double)Touchdowns / ThrowAttempts) * 20;
			return NflComponentLimitCheck(component);
		}

		private double NflInterceptComponent() {
			var component = 2.375 - ((double)Interceptions / ThrowAttempts * 25);
			return NflComponentLimitCheck(component);
		}

		private double NflComponentLimitCheck(double component) {
			if (component < 0) { return 0; }
			if (component > 2.375) { return 2.375; }
			return component;
		}

		public int ThrowAttempts { get; set; }
		public int PassCompletion { get; set; }
		public int PassingYards { get; set; }
		public int Touchdowns { get; set; }
		public int Interceptions { get; set; }
	}
}
