using Core.App;
using Core.Gameplay.Menu.App;
using Core.Gameplay.Menu.UI.App;
using Core.Modules;
using UnityEngine;

namespace Core.Gameplay.Menu.Modules {
	[CreateAssetMenu(menuName = "Modules/Gameplay/MenuMetaModule", fileName = "MenuMetaModule")]
	public class MenuMetaModule : AppModule {
		[SerializeField]
		private MenuUIScreenService.Settings _uiSettings;
		
		[SerializeField]
		private MenuScreenService.Settings _menuScreenSettings;

		public override void OnInitialize (AppComponentRegistry componentRegistry) {
			base.OnInitialize(componentRegistry);

			componentRegistry.Instantiate<MenuUIScreenService>(_uiSettings);
			componentRegistry.Instantiate<MenuScreenService>(_menuScreenSettings);
			componentRegistry.Instantiate<MenuLoaderService>();
		}
	}
}
