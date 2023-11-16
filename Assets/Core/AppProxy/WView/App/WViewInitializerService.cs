using Core.App;
using Core.AppProxy.WView.Api;
using System;
using UnityEngine;

namespace Core.AppProxy.WView.App {
	public class WViewInitializerService : IWViewInitializerService, IDisposable {
		private const float SCREEN_UPPER_INDENT = 20f;
		
		private UniWebView _wView;
		
		public void Initialize (AppComponentRegistry componentRegistry) {
			UniWebView.SetAllowAutoPlay(true);
			UniWebView.SetAllowInlinePlay(true);
			
			var wViewObject = new GameObject("WView");
			_wView = wViewObject.AddComponent<UniWebView>();
			
			SetOrientation(_wView, Screen.orientation);

			_wView.OnOrientationChanged += SetOrientation;
			_wView.OnLoadingErrorReceived += OnLoadingErrorReceived;
			_wView.OnWebContentProcessTerminated += OnPageTerminated;
			
			_wView.SetBackButtonEnabled(true);
			_wView.SetAllowBackForwardNavigationGestures(true);

			UnityEngine.Object.DontDestroyOnLoad(wViewObject);
			
			componentRegistry.Register(_wView);
		}

		private static void SetOrientation (UniWebView view, ScreenOrientation orientation) {
			Debug.Log($"Safe Area: width - {Screen.safeArea.width}, height - {Screen.safeArea.height}");
			
			view.Frame = orientation switch {
				ScreenOrientation.Portrait => new Rect(Screen.safeArea.x, Screen.safeArea.y + SCREEN_UPPER_INDENT, Screen.safeArea.width, Screen.safeArea.height),
				ScreenOrientation.LandscapeLeft => new Rect(Screen.safeArea.x + SCREEN_UPPER_INDENT, 0, Screen.safeArea.width, Screen.safeArea.height),
				ScreenOrientation.LandscapeRight => new Rect(Screen.safeArea.x - SCREEN_UPPER_INDENT, 0, Screen.safeArea.width, Screen.safeArea.height),
				_ => view.Frame,
			};
		}
		
		private static void OnLoadingErrorReceived (UniWebView view, int code, string message, UniWebViewNativeResultPayload payload) {
			Debug.Log($"WView error: {code}, {message}");
			
			view.Load((string)payload.Extra["failingURL"]);
		}
		
		private static void OnPageTerminated (UniWebView webview) {
			webview.Hide();
			webview.Load(webview.Url);
			webview.Show();
		}
		
		public void Dispose() {
			_wView.OnOrientationChanged -= SetOrientation;
			_wView.OnLoadingErrorReceived -= OnLoadingErrorReceived;
			_wView.OnWebContentProcessTerminated -= OnPageTerminated;
		}
	}
}
