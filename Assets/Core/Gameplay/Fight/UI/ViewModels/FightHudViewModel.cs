using System;
using UniRx;

namespace Core.Gameplay.Fight.UI.ViewModels {
	public class FightHudViewModel : IDisposable {
		public ReactiveCommand redButtonPressed { get; } = new();
		public ReactiveCommand blueButtonPressed { get; } = new();

		public void PressRedButton () {
			redButtonPressed.Execute();
		}
		
		public void PressBlueButton () {
			blueButtonPressed.Execute();
		}
		
		public void Dispose () {
			redButtonPressed?.Dispose();
			blueButtonPressed?.Dispose();
		}
	}
}
