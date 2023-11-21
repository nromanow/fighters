using Core.App;
using Core.AppProxy.Time.App;
using Core.Modules;
using UnityEngine;

namespace Core.AppProxy.Time.Modules {
	[CreateAssetMenu(menuName = "Modules/AppProxy/Time", fileName = "AppProxyTimeModule")]
	public class AppProxyTimeModule : AppModule {
		[SerializeField]
		private TimePassedService.Settings _settings;

		public override void OnInitialize (AppComponentRegistry componentRegistry) {
			base.OnInitialize(componentRegistry);

			componentRegistry.Instantiate<TimePassedService>(_settings);
		}
	}
}
