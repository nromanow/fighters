using Core.AppProxy.StartupProxy.Api;
using Core.Network.Api;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Core.AppProxy.StartupProxy.App {
	public class AppStartupProxyApi : IAppStartupProxyApi {
		private readonly Settings _settings;
		private readonly IAppHttpRequestsService _appHttpRequestsService;

		public AppStartupProxyApi (
			Settings settings,
			IAppHttpRequestsService appHttpRequestsService) {
			_settings = settings;
			_appHttpRequestsService = appHttpRequestsService;
		}

		public UniTask<Dictionary<string, object>> GetConfig (Dictionary<string, object> conversionData, CancellationToken cancellationToken) {
			return _appHttpRequestsService.SendPostRequest(_settings.configUrl, conversionData, cancellationToken)
				.ContinueWith(JsonConvert.DeserializeObject<Dictionary<string, object>>);
		}

		[Serializable]
		public class Settings {
			[SerializeField]
			private string _configUrl;

			public string configUrl => _configUrl;
		}
	}
}
