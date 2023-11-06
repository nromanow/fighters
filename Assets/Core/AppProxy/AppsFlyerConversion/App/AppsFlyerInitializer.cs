using AppsFlyerSDK;
using Core.App;
using Core.AppProxy.AppsFlyerConversion.Api;
using System;
using UnityEngine;

namespace Core.AppProxy.AppsFlyerConversion.App {
	public class AppsFlyerInitializer : IAppsFlyerInitializer {
		private readonly Settings _settings;

		public AppsFlyerInitializer (Settings settings) {
			_settings = settings;
		}

		public void Initialize (AppComponentRegistry componentRegistry) {
			var listener = UnityEngine.Object
				.Instantiate(_settings.listenerPrefab);
			
			componentRegistry.Register(listener);
			
			AppsFlyer.initSDK(_settings.devKey, _settings.appleAppId, listener);
			AppsFlyer.startSDK();
		}

		[Serializable]
		public class Settings {
			[SerializeField]
			private string _devKey;

			[SerializeField]
			private string _appleAppId;

			[SerializeField]
			private AppsFlyerListener _listenerPrefab;

			public string devKey => _devKey;
			public string appleAppId => _appleAppId;
			
			public AppsFlyerListener listenerPrefab => _listenerPrefab;
		}
	}
}
