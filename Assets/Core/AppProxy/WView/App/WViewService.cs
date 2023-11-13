﻿using Core.AppProxy.WView.Api;
using System;
using UniRx;
using UnityEngine;

namespace Core.AppProxy.WView.App {
	public class WViewService : IWViewService, IDisposable {
		public ReactiveCommand windowLoaded { get; } = new();
		public IReadOnlyReactiveProperty<bool> isLoadInit => _isLoadInit;
		
		private readonly ReactiveProperty<bool> _isLoadInit = new();
		private readonly UniWebView _uniWView;

		private int _triesCount;

		public WViewService (UniWebView uniWView) {
			_uniWView = uniWView;

			_uniWView.OnPageFinished += OnPageLoaded;
		}

		public void LoadWindow (string url) {
			_uniWView.Load(url);
			
			_isLoadInit.Value = true;
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
			
			_isLoadInit?.Dispose();
			_uniWView.OnPageFinished -= OnPageLoaded;
		}
	}
}
