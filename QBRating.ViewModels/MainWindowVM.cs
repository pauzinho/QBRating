using QuarterbackCalculator;
using System;
using System.Windows.Input;

namespace QBRating.ViewModels {
	public class MainWindowVM : NotifyDataErrorInfo<MainWindowVM> {
		private readonly PassingCalculator _calculator;
		private readonly DelegateCommand _resetCommand;
		public ICommand ResetCommand => _resetCommand;

		public MainWindowVM() {
			_resetCommand = new DelegateCommand(Reset, CanReset);
			_calculator = new PassingCalculator();

			//throw attempt rules
			Rules.Add(new DelegateRule<MainWindowVM>(
				nameof(ThrowAttempts),
				"Attempts field is required.",
				f => f.ThrowAttempts.HasValue));
			Rules.Add(new DelegateRule<MainWindowVM>(
				nameof(ThrowAttempts),
				"Attempts must be a non-negative whole number.",
				f => f.NonNegWholeIntField(f.ThrowAttempts)));

			//pass completion rules
			Rules.Add(new DelegateRule<MainWindowVM>(
				nameof(PassCompletion),
				"Pass Completion is required.",
				f => f.PassCompletion.HasValue));
			Rules.Add(new DelegateRule<MainWindowVM>(
				nameof(PassCompletion),
				"Pass Completion must be a non-negative whole number.",
				f => f.NonNegWholeIntField(f.PassCompletion)));
			Rules.Add(new DelegateRule<MainWindowVM>(
				nameof(PassCompletion),
				"Pass Completion must less than or equal to Attempts.",
				f => f.NotLargerThanDependentField(f.PassCompletion, f.ThrowAttempts)));

			//Passing Yard rules
			Rules.Add(new DelegateRule<MainWindowVM>(
				nameof(PassingYards),
				"Passing Yards is required.",
				f => f.PassingYards.HasValue));
			Rules.Add(new DelegateRule<MainWindowVM>(
				nameof(PassingYards),
				"Passing Yards cannot be less than -99 * Attempts or greater than 99 * Completed Passes.",
				f => f.FieldLessThan99TimesDependent(f.PassingYards, f.PassCompletion)));

			//Touchdown rules
			Rules.Add(new DelegateRule<MainWindowVM>(
				nameof(Touchdowns),
				"Touchdowns is required.",
				f => f.Touchdowns.HasValue));
			Rules.Add(new DelegateRule<MainWindowVM>(
				nameof(Touchdowns),
				"Touchdowns must be a non-negative whole number.",
				f => f.NonNegWholeIntField(f.Touchdowns)));
			Rules.Add(new DelegateRule<MainWindowVM>(
				nameof(Touchdowns),
				"Touchdowns must less than or equal to Completions.",
				f => f.NotLargerThanDependentField(f.Touchdowns, f.PassCompletion)));

			//Interception rules
			Rules.Add(new DelegateRule<MainWindowVM>(
				nameof(Interceptions),
				"Interceptions is required.",
				f => f.Interceptions.HasValue));
			Rules.Add(new DelegateRule<MainWindowVM>(
				nameof(Interceptions),
				"Interceptions must be a non-negative whole number.",
				f => f.NonNegWholeIntField(f.Interceptions)));
			Rules.Add(new DelegateRule<MainWindowVM>(
				nameof(Interceptions),
				"Interceptions must less than or equal to Attempts.",
				f => f.NotLargerThanDependentField(f.Interceptions, f.ThrowAttempts)));
		}

		private bool CanReset(object arg) {
			return ThrowAttempts.HasValue || PassCompletion.HasValue || PassingYards.HasValue || Touchdowns.HasValue || Interceptions.HasValue;
		}

		private void Reset(object obj) {
			ThrowAttempts = null;
			PassCompletion = null;
			PassingYards = null;
			Touchdowns = null;
			Interceptions = null;
			PasserRatingResult = null;
		}

		private void UpdateFormulaRelated() {
			_resetCommand.RaiseCanExecuteChanged();

			if(ThrowAttempts.HasValue && PassCompletion.HasValue && PassingYards.HasValue && Touchdowns.HasValue && Interceptions.HasValue) {
				_calculator.ThrowAttempts = ThrowAttempts.Value;
				_calculator.PassCompletion = PassCompletion.Value;
				_calculator.PassingYards = PassingYards.Value;
				_calculator.Touchdowns = Touchdowns.Value;
				_calculator.Interceptions = Interceptions.Value;
				if (NFLIsChecked) {
					PasserRatingResult = $"NFL Passer Rating: {Math.Round(_calculator.NFLPassRating(), 2)}";
				}
				if (NCAAIsChecked) {
					PasserRatingResult = $"NCAA Passer Rating: {Math.Round(_calculator.NCAAPassRating(), 2)}";
				}
			}
		}

		private int? _throwAttempts = null;
		public int? ThrowAttempts { 
			get => _throwAttempts;
			set { 
				SetProperty(ref _throwAttempts, value);
				UpdateFormulaRelated();
			}
		}

		private int? _passCompletion = null;
		public int? PassCompletion {
			get => _passCompletion;
			set { 
				SetProperty(ref _passCompletion, value);
				UpdateFormulaRelated();
			}
		}

		private int? _passingYards = null;
		public int? PassingYards {
			get => _passingYards;
			set { SetProperty(ref _passingYards, value);
				UpdateFormulaRelated();
			}
		}

		private int? _touchdowns = null;
		public int? Touchdowns {
			get => _touchdowns;
			set { 
				SetProperty(ref _touchdowns, value);
				UpdateFormulaRelated();
			}
		}

		private int? _interceptions = null;
		public int? Interceptions {
			get => _interceptions;
			set {
				SetProperty(ref _interceptions, value);
				UpdateFormulaRelated();
			}
		}

		private string _passerRatingResult;
		public string PasserRatingResult {
			get => _passerRatingResult;
			set => SetProperty(ref _passerRatingResult, value);
		}

		private int _formulaChosen = 0;
		public int FormulaChosen {
			get { return _formulaChosen; }
			set {
				SetProperty(ref _formulaChosen, value);
				OnPropertyChanged(nameof(NFLIsChecked));
				OnPropertyChanged(nameof(NCAAIsChecked));
				UpdateFormulaRelated();
			}
		}
		public bool NFLIsChecked {
			get { return FormulaChosen.Equals(0); }
			set { FormulaChosen = 0; }
		}
		public bool NCAAIsChecked {
			get { return FormulaChosen.Equals(1); }
			set { FormulaChosen = 1; }
		}
	}
}
