using Core.App;
using Core.Gameplay.GameStart.App;
using Core.Modules;
using UnityEngine;

namespace Core.Gameplay.GameStart.Modules {
	[CreateAssetMenu(menuName = "Modules/Gameplay/GameStartMetaModule", fileName = "GameStartMetaModule")]
	public class GameStartMetaModule : AppModule {
		public override void OnInitialize (AppComponentRegistry componentRegistry) {
			base.OnInitialize(componentRegistry);
			
			componentRegistry
				.Instantiate<GameStartService>()
				.StartGame();
		}
	}
}
