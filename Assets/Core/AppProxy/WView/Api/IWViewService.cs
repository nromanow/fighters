using UniRx;

namespace Core.AppProxy.WView.Api {
	public interface IWViewService {
		ReactiveCommand windowLoaded { get; }
		IReadOnlyReactiveProperty<bool> isLoadInit { get; }

		void LoadWindow (string url);
		void ShowWindow ();
	}
}
