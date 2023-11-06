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
				// Set full screen again. If it is now in landscape, it is 640x320.
				wView.Frame = new Rect(0, 0, Screen.width, Screen.height);
			};
			
			UnityEngine.Object.DontDestroyOnLoad(wViewObject);
			
			componentRegistry.Register(wView);
		}
	}
}
