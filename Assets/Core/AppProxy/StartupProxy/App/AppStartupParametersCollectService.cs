using AppsFlyerSDK;
using Core.AppProxy.Notifications.Api;
using Core.AppProxy.StartupProxy.Api;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Core.AppProxy.StartupProxy.App {
	public class AppStartupParametersCollectService : IAppStartupParametersCollectService {
		private readonly Settings _settings;
		private readonly INotificationsMessagesListener _notificationsMessagesListener;

		public AppStartupParametersCollectService (
			Settings settings,
			INotificationsMessagesListener notificationsMessagesListener) {
			_settings = settings;
			_notificationsMessagesListener = notificationsMessagesListener;
		}

		public async UniTask<Dictionary<string, object>> GetParams (CancellationToken cancellationToken) {
			var parameters = new Dictionary<string, object>() {
				{ "af_id", AppsFlyer.getAppsFlyerId() },
				{ "bundle_id", Application.identifier },
				{ "locale", Application.systemLanguage.ToString("G") },
				{ "firebase_project_id", _settings.firebaseProjectId },
			};

			var os = Application.platform switch {
				RuntimePlatform.Android => "Android",
				RuntimePlatform.IPhonePlayer => "iOS",
				_ => string.Empty,
			};

			var storeId = Application.platform switch {
				RuntimePlatform.Android => Application.identifier,
				RuntimePlatform.IPhonePlayer => _settings.appleAppId,
				_ => string.Empty,
			};

			parameters.Add("os", os);
			parameters.Add("store_id", storeId);
			parameters.Add("push_token", _notificationsMessagesListener.pushToken.Value);

			return parameters;
		}

		[Serializable]
		public class Settings {
			[SerializeField]
			private string _firebaseProjectId;

			[SerializeField]
			private string _appleAppId;

			public string firebaseProjectId => _firebaseProjectId;
			public string appleAppId => _appleAppId;
		}
	}
}
