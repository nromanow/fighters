using System;
using UniRx;

namespace Core.Meta.Fight.UI.ViewModels {
	public class PostMatchScreenViewModel : IDisposable {
		public ReactiveCommand restartButtonPress { get; } = new();
		
		public bool isRedWinner { get; }
		public int redScore { get; }
		public int blueScore { get; }

		public PostMatchScreenViewModel(bool isRedWinner, int redScore, int blueScore) {
			this.isRedWinner = isRedWinner;
			this.redScore = redScore;
			this.blueScore = blueScore;
		}

		public void Restart() {
			restartButtonPress.Execute();
		}
		
		public void Dispose() {
			restartButtonPress?.Dispose();
		}
	}
}
