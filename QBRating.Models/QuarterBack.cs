using System;
using System.ComponentModel;

namespace QBRating.Models {
	public class Quarterback {
		public int ID { get; set; }
		public string Name { get; set; }
		public int ThrowAttempts { get; set; }
		public int PassCompletion { get; set; }
		public int PassingYards { get; set; }
		public int Touchdowns { get; set; }
		public int Interceptions { get; set; }
		//public double? NFLPasserRating {
		//	get { 
   
  // }
		//}
		//public double? NCAAPasserRating {
		//	get { }
		//}
	}
}
