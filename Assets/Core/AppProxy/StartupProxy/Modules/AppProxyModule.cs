using Core.App;
using Core.AppProxy.StartupProxy.App;
using UnityEngine;

namespace Core.AppProxy.StartupProxy.Modules {
	[CreateAssetMenu(menuName = "Modules/AppProxy/StartupProxyModule", fileName = "StartupProxyModule")]
	public class AppProxyModule : AppModule {
		[SerializeField]
		private AppStartupProxyApi.Settings _apiSettings;

		public override void OnInitialize (AppComponentRegistry componentRegistry) {
			base.OnInitialize(componentRegistry);

			componentRegistry.Instantiate<AppStartupProxyApi>(_apiSettings);
			
			componentRegistry
				.Instantiate<AppStartupProxy>()
				.Startup();
		}
	}
}
