using Core.App;
using Core.AppProxy.Layers.App;
using Core.Modules;
using UnityEngine;

namespace Core.AppProxy.Layers.Modules {
	[CreateAssetMenu(menuName = "Modules/AppProxy/Layers", fileName = "AppProxyAppLayersModule")]
	public class AppProxyAppLayersModule : AppModule {
		public override void OnInitialize (AppComponentRegistry componentRegistry) {
			base.OnInitialize(componentRegistry);

			componentRegistry.Instantiate<AppLayersService>();
		}
	}
}
