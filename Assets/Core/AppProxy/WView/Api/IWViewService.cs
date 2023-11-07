using UniRx;

namespace Core.AppProxy.WView.Api {
	public interface IWViewService {
		ReactiveCommand windowLoaded { get; }
		
		void LoadWindow (string url);
		void ShowWindow ();
	}
}
