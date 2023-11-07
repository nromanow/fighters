﻿using Core.AppProxy.WView.Api;
using System;
using UniRx;

namespace Core.AppProxy.WView.App {
	public class WViewService : IWViewService, IDisposable {
		public ReactiveCommand windowLoaded { get; } = new();
		
		private readonly UniWebView _uniWView;

		public WViewService (UniWebView uniWView) {
			_uniWView = uniWView;

			_uniWView.OnPageFinished += OnPageLoaded;
		}

		public void LoadWindow (string url) {
			_uniWView.Load(url);
		}
		
		public void ShowWindow () {
			_uniWView.Show();
		}

		private void OnPageLoaded (UniWebView view, int statusCode, string url) {
			windowLoaded.Execute();
		}

		public void Dispose() {
			windowLoaded?.Dispose();

			_uniWView.OnPageFinished -= OnPageLoaded;
		}
	}
}
