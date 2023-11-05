using System;
using UniRx;

namespace Core.Meta.Menu.UI.ViewModels {
	public class MenuScreenViewModel : IDisposable {
		public ReactiveCommand openPrivacyPolicy { get; } = new();
		public ReactiveCommand play { get; } = new();

		public void Play () {
			play.Execute();
		}
		
		public void OpenPrivacyPolicy () {
			openPrivacyPolicy.Execute();
		}
		
		public void Dispose() {
			openPrivacyPolicy?.Dispose();
			play?.Dispose();
		}
	}
}
