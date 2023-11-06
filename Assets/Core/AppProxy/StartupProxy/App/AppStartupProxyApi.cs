using Core.AppProxy.StartupProxy.Api;
using Core.Utils;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Core.AppProxy.StartupProxy.App {
	public class AppStartupProxyApi : IAppStartupProxyApi {
		private readonly Settings _settings;

		public AppStartupProxyApi (Settings settings) {
			_settings = settings;
		}

		public UniTask<Dictionary<string, object>> GetConfig (Dictionary<string, object> conversionData, CancellationToken cancellationToken) {
			return AppRequestUtils.GetPostRequestResponse(_settings.configUrl, conversionData, cancellationToken);
		}

		[Serializable]
		public class Settings {
			[SerializeField]
			private string _configUrl;

			public string configUrl => _configUrl;
		}
	}
}
