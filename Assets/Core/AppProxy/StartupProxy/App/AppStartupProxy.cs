using Core.AppProxy.AppFields.App;
using Core.AppProxy.AppsFlyerConversion.Api;
using Core.AppProxy.GameLoader.Api;
using Core.AppProxy.StartupProxy.Api;
using Core.AppProxy.WView.Api;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using UnityEngine;

namespace Core.AppProxy.StartupProxy.App {
	public class AppStartupProxy : IAppStartupProxy, IDisposable {
		private const string PLAYER_PREFS_CAN_START_APP_KEY = "can_start_app";
		private const string URL_FIELD_KEY = "url";

		private readonly IAppStartupProxyApi _api;
		private readonly IGameLoaderService _gameLoaderService;
		private readonly IAppsFlyerListener _appsFlyerListener;
		private readonly IWViewService _wViewService;
		private readonly AppFieldsContainer _appFieldsContainer;
		private readonly CompositeDisposable _disposable = new();
		private readonly CancellationTokenSource _cancellationTokenSource = new();

		public AppStartupProxy (
			IAppStartupProxyApi api,
			IGameLoaderService gameLoaderService,
			IAppsFlyerListener appsFlyerListener,
			IWViewService wViewService,
			AppFieldsContainer appFieldsContainer) {
			_api = api;
			_gameLoaderService = gameLoaderService;
			_appsFlyerListener = appsFlyerListener;
			_wViewService = wViewService;
			_appFieldsContainer = appFieldsContainer;
		}

		public void Startup () {
			// if (PlayerPrefs.GetInt(PLAYER_PREFS_CAN_START_APP_KEY, 0) == 1) {
			// 	_gameLoaderService.LoadGame();
			// 	return;
			// }
			//
			// if (_appFieldsContainer.HasNotExpiredValue(URL_FIELD_KEY, out var url)) {
			// 	_wViewService.Load((string)url);
			// 	return;
			// }
			//
			// _appsFlyerListener.onConversionDataReceived
			// 	.Subscribe(OnConversionDataReceived)
			// 	.AddTo(_disposable);
			//
			// _appsFlyerListener.onConversionDataFailed
			// 	.Subscribe(_ => OnConversionDataFailed())
			// 	.AddTo(_disposable);
		}

		private void OnConversionDataReceived (Dictionary<string, object> conversionData) {
			_api
				.GetConfig(conversionData, _cancellationTokenSource.Token)
				.ContinueWith(OnConfigReceived)
				.Forget(Debug.LogException);
		}
		
		private void OnConversionDataFailed () {
			_gameLoaderService.LoadGame();
		}

		private void OnConfigReceived (Dictionary<string, object> response) {
			var okStatus = (bool)response["ok"];

			if (okStatus) {
				var url = (string)response["url"];
				var expirationTimeStamp = (int)response["expires"];

				_appFieldsContainer.AddValue(URL_FIELD_KEY, url, expirationTimeStamp);
				_wViewService.Load(url);
			}
			else {
				PlayerPrefs.SetInt(PLAYER_PREFS_CAN_START_APP_KEY, 1);
				_gameLoaderService.LoadGame();
			}
		}

		public void Dispose () {
			_disposable?.Dispose();
			_cancellationTokenSource?.Dispose();
		}
	}
}
