using Core.App;
using Core.WView.Api;
using UnityEngine;

namespace Core.WView.App {
	public class WViewInitializerService : IWViewInitializerService {
		public void Initialize (AppComponentRegistry componentRegistry) {
			var wViewObject = new GameObject("WView");
			var wView = wViewObject.AddComponent<UniWebView>();
			
			wView.Frame = new Rect(0, 0, Screen.width, Screen.height);
			
			UnityEngine.Object.DontDestroyOnLoad(wViewObject);
			
			componentRegistry.Register(wView);
		}
	}
}
