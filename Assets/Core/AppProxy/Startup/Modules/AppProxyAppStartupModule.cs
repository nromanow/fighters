using Core.App;
using Core.AppProxy.Startup.App;
using Core.Modules;
using UnityEngine;

namespace Core.AppProxy.Startup.Modules {
	[CreateAssetMenu(menuName = "Modules/AppProxy/Startup", fileName = "AppProxyAppStartupModule")]
	public class AppProxyAppStartupModule : AppModule {
		public override void OnInitialize (AppComponentRegistry componentRegistry) {
			base.OnInitialize(componentRegistry);
			
			componentRegistry
				.Instantiate<AppStartupProxyService>()
				.Initialize();
		}
	}
}
