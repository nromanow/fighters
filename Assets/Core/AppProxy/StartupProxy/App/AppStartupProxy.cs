using Core.AppProxy.AppFields.App;
using Core.AppProxy.AppsFlyerConversion.Api;
using Core.AppProxy.AppTracking.Api;
using Core.AppProxy.GameLoader.Api;
using Core.AppProxy.Loading.Api;
using Core.AppProxy.Notifications.Api;
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
		private readonly ILoadingScreenService _loadingScreenService;
		private readonly IAppTrackingPermissionService _appTrackingPermissionService;
		private readonly INotificationsMessagesListener _notificationsMessagesListener;
		private readonly INotificationsPermissionService _notificationsPermissionService;
		private readonly IAppStartupParametersCollectService _appStartupParametersCollectService;
		private readonly IGameLoaderService _gameLoaderService;
		private readonly IAppsFlyerListener _appsFlyerListener;
		private readonly IWViewService _wViewService;
		private readonly AppFieldsContainer _appFieldsContainer;
		private readonly CompositeDisposable _disposable = new();
		private readonly CancellationTokenSource _progressToken = new();

		public AppStartupProxy (
			IAppStartupProxyApi api,
			ILoadingScreenService loadingScreenService,
			IAppTrackingPermissionService appTrackingPermissionService,
			INotificationsMessagesListener notificationsMessagesListener,
			INotificationsPermissionService notificationsPermissionService,
			IAppStartupParametersCollectService appStartupParametersCollectService,
			IGameLoaderService gameLoaderService,
			IAppsFlyerListener appsFlyerListener,
			IWViewService wViewService,
			AppFieldsContainer appFieldsContainer) {
			_api = api;
			_loadingScreenService = loadingScreenService;
			_appTrackingPermissionService = appTrackingPermissionService;
			_notificationsMessagesListener = notificationsMessagesListener;
			_notificationsPermissionService = notificationsPermissionService;
			_appStartupParametersCollectService = appStartupParametersCollectService;
			_gameLoaderService = gameLoaderService;
			_appsFlyerListener = appsFlyerListener;
			_wViewService = wViewService;
			_appFieldsContainer = appFieldsContainer;
		}

		public async UniTask Startup (CancellationToken cancellationToken) {
			var progressProperty = new ReactiveProperty<int>().AddTo(_disposable);

			IProgress<int> progress =
				new Progress<int>(value => progressProperty.Value = value);

			SetupLoadingProgress(progress, _progressToken.Token).Forget(Debug.LogException);

			_loadingScreenService.ShowLoadingScreen(progressProperty);

			if (!_appTrackingPermissionService.hasPermission) {
				await _appTrackingPermissionService.RequestPermission(cancellationToken);
			}

			if (PlayerPrefs.GetInt(PLAYER_PREFS_CAN_START_APP_KEY, 0) == 1) {
				StartGame();

				return;
			}

			if (_appFieldsContainer.HasNotExpiredValue(URL_FIELD_KEY, out var url)) {
				Debug.Log($"Load old url: [{url}]");

				await SetupNotifications(cancellationToken);
				
				SetupWView(url);

				return;
			}

			_appsFlyerListener.onConversionDataReceived
				.Take(1)
				.Subscribe(data => OnConversionDataReceived(data, cancellationToken).Forget(Debug.LogException))
				.AddTo(_disposable);

			_appsFlyerListener.onConversionDataFailed
				.Subscribe(_ => OnConversionDataFailed())
				.AddTo(_disposable);

			return;

			async UniTask SetupLoadingProgress (IProgress<int> p, CancellationToken cToken) {
				var currentProgress = 0;

				while (currentProgress < 90) {
					if (cToken.IsCancellationRequested) {
						cToken.ThrowIfCancellationRequested();
					}

					currentProgress++;
					p.Report(currentProgress);

					await UniTask.Delay(200, cancellationToken: cToken);
					await UniTask.Yield();
				}
			}
		}

		private void StartGame () {
			_gameLoaderService.LoadGame();
			_loadingScreenService.HideLoadingScreen();
		}

		private async UniTask SetupNotifications (CancellationToken cancellationToken) {
			_notificationsPermissionService.hasPermission
				.Subscribe(hasPermission => {
					if (hasPermission) {
						_notificationsMessagesListener.SubscribeOnMessageParamByKey("url", SetupWView);
						_notificationsMessagesListener.StartListen();
					}
				})
				.AddTo(_disposable);

			if (_notificationsPermissionService.timeAskPermissionExpired.Value
				&& !_notificationsPermissionService.hasPermission.Value) {
				await _notificationsPermissionService.RequestPermission(cancellationToken);
			}

			//TODO: require subscribe on notifications token updated
			await UniTask.Delay(6000, cancellationToken: cancellationToken);
		}

		private void SetupWView (object url) {
			if (_wViewService.isLoadInit.Value)
				return;

			_wViewService.windowLoaded
				.Take(1)
				.Subscribe(_ => {
					_wViewService.ShowWindow();
					_loadingScreenService.HideLoadingScreen();
				})
				.AddTo(_disposable);

			_wViewService.LoadWindow((string)url);
		}

		private async UniTask OnConversionDataReceived (Dictionary<string, object> conversionData, CancellationToken cancellationToken) {
			Debug.Log($"Conversion data received. Start init game parameters");
			
			if (conversionData.TryGetValue("af_status", out var status)) {
				if (status.ToString() == "Organic") {
					Debug.Log($"Organic install");
					
					StartGame();
					return;
				}
			}

			Debug.Log($"Non-organic install");
			
			await SetupNotifications(cancellationToken);
			
			await _appStartupParametersCollectService
				.GetParams(cancellationToken)
				.ContinueWith(paramsData => {
					foreach (var param in paramsData) {
						conversionData.Add(param.Key, param.Value);
					}

					Debug.Log($"Params inited. Send config request");
					
					_api.GetConfig(conversionData, cancellationToken)
						.ContinueWith(OnConfigReceived)
						.Forget(Debug.LogException);
				});
		}

		private void OnConversionDataFailed () {
			_gameLoaderService.LoadGame();
		}

		private void OnConfigReceived (IReadOnlyDictionary<string, object> response) {
			var okStatus = (bool)response["ok"];

			if (okStatus) {
				var url = (string)response["url"];
				var expirationTimeStamp = (int)(long)response["expires"];

				_appFieldsContainer.AddValue(URL_FIELD_KEY, url, expirationTimeStamp);

				Debug.Log($"Load new url: {url}");

				SetupWView(url);
			}
			else {
				PlayerPrefs.SetInt(PLAYER_PREFS_CAN_START_APP_KEY, 1);
				StartGame();
			}
		}

		public void Dispose () {
			_disposable?.Dispose();
			_progressToken.Dispose();
		}
	}
}
