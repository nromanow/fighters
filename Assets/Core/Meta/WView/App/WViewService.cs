using Core.Meta.WView.Api;
using System;
using UniRx;
using UnityEngine;

namespace Core.Meta.WView.App {
	public class WViewService : IWViewService, IDisposable {
		public ReactiveCommand windowLoaded { get; } = new();
		private readonly UniWebView _uniWView;

		private int _attemptionsCount;

		public WViewService (UniWebView uniWView) {
			_uniWView = uniWView;

			_uniWView.OnPageFinished += OnPageLoaded;
		}

		public void LoadWindow (string url) {
			Debug.Log($"Start loading [{_attemptionsCount++}] page {url}");
			
			_uniWView.Load(url);
		}
		
		public void ShowWindow () {
			_uniWView.Show();
		}

		private void OnPageLoaded (UniWebView view, int statusCode, string url) {
			Debug.Log($"WView page loaded: {statusCode}, {url}");
			
			windowLoaded.Execute();
		}

		public void Dispose() {
			windowLoaded?.Dispose();
			
			_uniWView.OnPageFinished -= OnPageLoaded;
		}
	}
}
