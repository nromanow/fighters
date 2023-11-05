using Core.App;
using Core.WView.App;
using UnityEngine;

namespace Core.WView.Modules {
	[CreateAssetMenu(menuName = "Modules/Meta/WView", fileName = "WViewModule")]
	public class WViewModule : AppModule {

		public override void OnInitialize (AppComponentRegistry componentRegistry) {
			base.OnInitialize(componentRegistry);
			
			componentRegistry
				.Instantiate<WViewInitializerService>()
				.Initialize(componentRegistry);

			//TEST
			componentRegistry
				.Instantiate<WViewService>()
				.Load("https://www.youtube.com/");
		}
	}
}
