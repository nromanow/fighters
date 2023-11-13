using Core.App;
using Core.AppProxy.StartupProxy.App;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.AppProxy.StartupProxy.Modules {
	[CreateAssetMenu(menuName = "Modules/AppProxy/StartupProxyModule", fileName = "StartupProxyModule")]
	public class AppProxyModule : AppModule {
		[SerializeField]
		private AppStartupProxyApi.Settings _apiSettings;

		[SerializeField]
		private AppStartupParametersCollectService.Settings _paramsSettings;

		public override void OnInitialize (AppComponentRegistry componentRegistry) {
			base.OnInitialize(componentRegistry);

			componentRegistry.Instantiate<AppStartupParametersCollectService>(_paramsSettings);
			componentRegistry.Instantiate<AppStartupProxyApi>(_apiSettings);
			
			componentRegistry
				.Instantiate<AppStartupProxy>()
				.Startup(moduleCancellationTokenSource.Token)
				.Forget(Debug.LogException);
		}
	}
}
