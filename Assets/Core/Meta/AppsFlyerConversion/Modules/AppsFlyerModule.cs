using Core.App;
using Core.Meta.AppsFlyerConversion.App;
using Core.Modules;
using UnityEngine;

namespace Core.Meta.AppsFlyerConversion.Modules {
	[CreateAssetMenu(menuName = "Modules/Meta/AppsFlyerModule", fileName = "AppsFlyerModule")]
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
