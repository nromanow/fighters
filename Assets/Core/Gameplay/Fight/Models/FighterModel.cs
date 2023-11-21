using System;
using UniRx;

namespace Core.Gameplay.Fight.Models {
	public class FighterModel : IDisposable {
		public ReactiveCommand kick { get; } = new();
		public IReadOnlyReactiveProperty<float> position => _position;

		private readonly ReactiveProperty<float> _position = new();

		public void InitStartPosition (float startPos) {
			_position.Value = startPos;
		}
		
		public void UpdatePositionByStep (float step) {
			_position.Value += step;
		}
		
		public void Kick () {
			kick.Execute();
		}

		public void Dispose () {
			kick?.Dispose();

			_position?.Dispose();
		}
	}
}
