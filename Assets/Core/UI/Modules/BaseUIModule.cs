using Core.App;
using Core.Modules;
using Core.UI.App;
using UnityEngine;

namespace Core.UI.Modules {
	[CreateAssetMenu(menuName = "Modules/Core/BaseUIModule", fileName = "BaseUIModule")]
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
