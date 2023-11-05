using Core.App;
using Core.Meta.Fight.App;
using Core.Meta.Fight.UI.App;
using UnityEngine;

namespace Core.Meta.Fight.Modules {
	[CreateAssetMenu(menuName = "Modules/Meta/FightModule", fileName = "FightModule")]
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
