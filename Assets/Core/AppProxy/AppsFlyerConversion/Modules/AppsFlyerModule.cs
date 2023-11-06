using Core.App;
using Core.AppProxy.AppsFlyerConversion.App;
using UnityEngine;

namespace Core.AppProxy.AppsFlyerConversion.Modules {
	[CreateAssetMenu(menuName = "Modules/AppProxy/AppsFlyerModule", fileName = "AppsFlyerModule")]
	public class AppsFlyerModule : AppModule {
		[SerializeField]
		private AppsFlyerInitializer.Settings _settings;

		public override void OnInitialize (AppComponentRegistry componentRegistry) {
			base.OnInitialize(componentRegistry);
			
			componentRegistry
				.Instantiate<AppsFlyerInitializer>(_settings)
				.Initialize(componentRegistry); }
	}
}
