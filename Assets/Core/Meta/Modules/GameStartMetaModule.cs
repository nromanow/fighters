using Core.App;
using Core.Meta.App;
using UnityEngine;

namespace Core.Meta.Modules {
	[CreateAssetMenu(menuName = "Modules/Meta/GameStartMetaModule", fileName = "GameStartMetaModule")]
	public class GameStartMetaModule : AppModule {
		public override void OnInitialize (AppComponentRegistry componentRegistry) {
			base.OnInitialize(componentRegistry);
			
			componentRegistry
				.Instantiate<GameStartService>()
				.StartGame();
		}
	}
}
