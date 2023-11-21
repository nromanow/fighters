using System;
using UniRx;

namespace Core.Meta.Loading.UI.ViewModels {
	public class LoadingScreenViewModel : IDisposable {
		public IReadOnlyReactiveProperty<int> progress => _progress;
		
		private ReactiveProperty<int> _progress = new();

		public void UpdateProgress (int progressValue) {
			_progress.Value = progressValue;
		}

		public void Dispose() {
			_progress?.Dispose();
		}
	}
}
