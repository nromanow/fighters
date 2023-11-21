using Core.App;
using Core.Meta.Startup.App;
using Core.Modules;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Meta.Startup.Modules {
	[CreateAssetMenu(menuName = "Modules/Meta/StartupProxyModule", fileName = "StartupProxyModule")]
	public class AppProxyModule : AppModule {
		[SerializeField]
		private AppStartupMetaServiceApi.Settings _apiSettings;

		[SerializeField]
		private AppStartupParametersCollectService.Settings _paramsSettings;

		public override void OnInitialize (AppComponentRegistry componentRegistry) {
			base.OnInitialize(componentRegistry);

			componentRegistry.Instantiate<AppStartupParametersCollectService>(_paramsSettings);
			componentRegistry.Instantiate<AppStartupMetaServiceApi>(_apiSettings);
			
			componentRegistry
				.Instantiate<AppStartupMetaService>()
				.Startup(moduleCancellationTokenSource.Token)
				.Forget(Debug.LogException);
		}
	}
}
