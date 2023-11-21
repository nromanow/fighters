using Core.App;
using Core.Meta.AppTracking.App;
using Core.Modules;
using UnityEngine;

namespace Core.Meta.AppTracking.Modules {
	[CreateAssetMenu(menuName = "Modules/Meta/AppTrackingModule", fileName = "AppTrackingModule")]
	public class AppTrackingModule : AppModule {
		public override void OnInitialize (AppComponentRegistry componentRegistry) {
			base.OnInitialize(componentRegistry);

			componentRegistry.Instantiate<AppTrackingPermissionService>();
		}
	}
}
