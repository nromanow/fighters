using Core.App;
using Core.AppProxy.WView.App;
using UnityEngine;

namespace Core.AppProxy.WView.Modules {
	[CreateAssetMenu(menuName = "Modules/Meta/WView", fileName = "WViewModule")]
	public class WViewModule : AppModule {

		public override void OnInitialize (AppComponentRegistry componentRegistry) {
			base.OnInitialize(componentRegistry);
			
			componentRegistry
				.Instantiate<WViewInitializerService>()
				.Initialize(componentRegistry);

			//TODO:Remove later
			componentRegistry
				.Instantiate<WViewService>()
				.Load("https://benioosn.com/sTpWMd");
		}
	}
}
