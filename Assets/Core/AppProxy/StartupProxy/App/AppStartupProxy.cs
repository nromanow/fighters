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
		private readonly IGameLoaderService _gameLoaderService;
		private readonly IAppsFlyerListener _appsFlyerListener;
		private readonly IWViewService _wViewService;
		private readonly AppFieldsContainer _appFieldsContainer;
		private readonly CompositeDisposable _disposable = new();

		public AppStartupProxy (
			IAppStartupProxyApi api,
			ILoadingScreenService loadingScreenService,
			IAppTrackingPermissionService appTrackingPermissionService,
			INotificationsMessagesListener notificationsMessagesListener,
			INotificationsPermissionService notificationsPermissionService,
			IGameLoaderService gameLoaderService,
			IAppsFlyerListener appsFlyerListener,
			IWViewService wViewService,
			AppFieldsContainer appFieldsContainer) {
			_api = api;
			_loadingScreenService = loadingScreenService;
			_appTrackingPermissionService = appTrackingPermissionService;
			_notificationsMessagesListener = notificationsMessagesListener;
			_notificationsPermissionService = notificationsPermissionService;
			_gameLoaderService = gameLoaderService;
			_appsFlyerListener = appsFlyerListener;
			_wViewService = wViewService;
			_appFieldsContainer = appFieldsContainer;
		}

		public async UniTask Startup (CancellationToken cancellationToken) {
			if (!_appTrackingPermissionService.hasPermission) {
				await _appTrackingPermissionService.RequestPermission(cancellationToken);
			}

			_loadingScreenService.ShowLoadingScreen();

			if (PlayerPrefs.GetInt(PLAYER_PREFS_CAN_START_APP_KEY, 0) == 1) {
				_gameLoaderService.LoadGame();
				return;
			}

			if (_appFieldsContainer.HasNotExpiredValue(URL_FIELD_KEY, out var url)) {
				await SetupNotifications(cancellationToken);

				SetupWView(url);

				return;
			}

			_appsFlyerListener.onConversionDataReceived
				.Subscribe(data => OnConversionDataReceived(data, cancellationToken))
				.AddTo(_disposable);

			_appsFlyerListener.onConversionDataFailed
				.Subscribe(_ => OnConversionDataFailed())
				.AddTo(_disposable);
		}

		private async UniTask SetupNotifications (CancellationToken cancellationToken) {
			_notificationsPermissionService.hasPermission
				.Subscribe(hasPermission => {
					if (hasPermission) {
						_notificationsMessagesListener.Initialize();
						_notificationsMessagesListener.SubscribeOnMessageParamByKey("url", SetupWView);
					}
				})
				.AddTo(_disposable);
			
			if (_notificationsPermissionService.timeAskPermissionExpired.Value
				&& !_notificationsPermissionService.hasPermission.Value) {
				_loadingScreenService.HideLoadingScreen();
				await _notificationsPermissionService.RequestPermission(cancellationToken);
				_loadingScreenService.ShowLoadingScreen();
			}
		}

		private void SetupWView (object url) {
			if (_wViewService.isLoadInit.Value)
				return;

			_wViewService.windowLoaded
				.Subscribe(_ => {
					_wViewService.ShowWindow();
					_loadingScreenService.HideLoadingScreen();
				})
				.AddTo(_disposable);

			_wViewService.LoadWindow((string)url);
		}

		private void OnConversionDataReceived (Dictionary<string, object> conversionData, CancellationToken cancellationToken) {
			SetupNotifications(cancellationToken)
				.ContinueWith(() => {
					_api
						.GetConfig(conversionData, cancellationToken)
						.ContinueWith(OnConfigReceived)
						.Forget(Debug.LogException);
				})
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
				_wViewService.LoadWindow(url);
			}
			else {
				PlayerPrefs.SetInt(PLAYER_PREFS_CAN_START_APP_KEY, 1);
				_gameLoaderService.LoadGame();
			}
		}

		public void Dispose () {
			_disposable?.Dispose();
		}
	}
}
