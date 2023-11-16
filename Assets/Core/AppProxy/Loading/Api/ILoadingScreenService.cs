using System;

namespace Core.AppProxy.Loading.Api {
	public interface ILoadingScreenService {
		void ShowLoadingScreen (IObservable<int> progress = default, int staticProgress = 0);

		void HideLoadingScreen ();
	}
}
