using Core.App;
using Core.AppProxy.WView.Api;
using System;
using UnityEngine;

namespace Core.AppProxy.WView.App {
	public class WViewInitializerService : IWViewInitializerService, IDisposable {
		private UniWebView _wView;
		
		public void Initialize (AppComponentRegistry componentRegistry) {
			UniWebView.SetJavaScriptEnabled(true);
			UniWebView.SetAllowJavaScriptOpenWindow(true);
			
			UniWebView.SetAllowAutoPlay(true);
			UniWebView.SetAllowInlinePlay(true);
			
			var wViewObject = new GameObject("WView");
			_wView = wViewObject.AddComponent<UniWebView>();
			
			SetOrientation(_wView, Screen.orientation);

			_wView.OnOrientationChanged += SetOrientation;
			_wView.OnLoadingErrorReceived += OnLoadingErrorReceived;
			
			_wView.SetBackButtonEnabled(true);
			_wView.SetAllowBackForwardNavigationGestures(true);

			UnityEngine.Object.DontDestroyOnLoad(wViewObject);
			
			componentRegistry.Register(_wView);
		}

		private static void SetOrientation (UniWebView view, ScreenOrientation orientation) {
			Debug.Log($"Safe Area: width - {Screen.safeArea.width}, height - {Screen.safeArea.height}");
			
			view.Frame = orientation switch {
				ScreenOrientation.Portrait => Screen.safeArea,
				ScreenOrientation.LandscapeLeft => Screen.safeArea,
				ScreenOrientation.LandscapeRight => Screen.safeArea,
				_ => view.Frame,
			};
		}
		
		private static void OnLoadingErrorReceived (UniWebView view, int code, string message, UniWebViewNativeResultPayload payload) {
			Debug.Log($"WView error: {code}, {message}");
			
			view.Load((string)payload.Extra["failingURL"]);
		}
		
		public void Dispose() {
			_wView.OnOrientationChanged -= SetOrientation;
			_wView.OnLoadingErrorReceived -= OnLoadingErrorReceived;
		}
	}
}
