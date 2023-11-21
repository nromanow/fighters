using UniRx;

namespace Core.Meta.WView.Api {
	public interface IWViewService {
		ReactiveCommand windowLoaded { get; }

		void LoadWindow (string url);
		void ShowWindow ();
	}
}
