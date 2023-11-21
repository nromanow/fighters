using Core.App;
using Core.Gameplay.Fight.App;
using Core.Gameplay.Fight.UI.App;
using Core.Modules;
using UnityEngine;

namespace Core.Gameplay.Fight.Modules {
	[CreateAssetMenu(menuName = "Modules/Gameplay/FightModule", fileName = "FightModule")]
	public class FightModule : AppModule {
		[SerializeField]
		private FightUIScreenService.Settings _uiSettings;
		
		public override void OnInitialize (AppComponentRegistry componentRegistry) {
			base.OnInitialize(componentRegistry);

			componentRegistry.Instantiate<FightUIScreenService>(_uiSettings);
			componentRegistry.Instantiate<FightLoaderService>();
			componentRegistry.Instantiate<FightViewInitializeService>();
			componentRegistry.Instantiate<FightProvider>();
			componentRegistry.Instantiate<FightStartService>();
		}
		
	}
}
