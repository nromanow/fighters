using Core.App;
using Core.UI.App;
using UnityEngine;

namespace Core.Modules {
	[CreateAssetMenu(menuName = "App/Modules/BaseMetaModule", fileName = "BaseMetaModule")]
	public class BaseUIModule : AppModule {
		[SerializeField]
		private UIInitializerService.Settings _uiInitSettings;
		
		public override void OnInitialize (AppComponentRegistry componentRegistry) {
			componentRegistry
				.Instantiate<UIInitializerService>(_uiInitSettings)
				.Initialize(componentRegistry);
			
			componentRegistry.Instantiate<UIScreenService>();
		}
	}
}
