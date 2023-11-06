using Core.App;
using Core.AppProxy.WView.Api;
using UnityEngine;

namespace Core.AppProxy.WView.App {
	public class WViewInitializerService : IWViewInitializerService {
		public void Initialize (AppComponentRegistry componentRegistry) {
			var wViewObject = new GameObject("WView");
			var wView = wViewObject.AddComponent<UniWebView>();
			
			wView.Frame = new Rect(0, 0, Screen.width, Screen.height);
			
			wView.OnOrientationChanged += (view, orientation) => {
				view.Frame = new Rect(0, 0, Screen.width, Screen.height);
			};

			wView.OnLoadingErrorReceived += (view, code, message, payload) => {
				var url = view.Url;
				
				view.Stop();
				view.Load(url);
			};

			UnityEngine.Object.DontDestroyOnLoad(wViewObject);
			
			componentRegistry.Register(wView);
		}
	}
}
