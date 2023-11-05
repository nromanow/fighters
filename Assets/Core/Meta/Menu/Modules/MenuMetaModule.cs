using Core.App;
using Core.Meta.Menu.App;
using Core.Meta.Menu.UI.App;
using UnityEngine;

namespace Core.Meta.Menu.Modules {
	[CreateAssetMenu(menuName = "Modules/Meta/MenuMetaModule", fileName = "MenuMetaModule")]
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
