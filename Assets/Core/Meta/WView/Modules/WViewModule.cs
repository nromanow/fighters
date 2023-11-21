using Core.App;
using Core.Meta.WView.App;
using Core.Modules;
using UnityEngine;

namespace Core.Meta.WView.Modules {
	[CreateAssetMenu(menuName = "Modules/Meta/WView", fileName = "WViewModule")]
	public class WViewModule : AppModule {

		public override void OnInitialize (AppComponentRegistry componentRegistry) {
			base.OnInitialize(componentRegistry);
			
			componentRegistry
				.Instantiate<WViewInitializerService>()
				.Initialize(componentRegistry);
			
			componentRegistry.Instantiate<WViewService>();
		}
	}
}
