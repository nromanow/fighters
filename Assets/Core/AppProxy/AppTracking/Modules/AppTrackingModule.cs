using Core.App;
using Core.AppProxy.AppTracking.App;
using UnityEngine;

namespace Core.AppProxy.AppTracking.Modules {
	[CreateAssetMenu(menuName = "Modules/AppProxy/AppTrackingModule", fileName = "AppTrackingModule")]
	public class AppTrackingModule : AppModule {
		public override void OnInitialize (AppComponentRegistry componentRegistry) {
			base.OnInitialize(componentRegistry);

			componentRegistry.Instantiate<AppTrackingPermissionService>();
		}
	}
}
