using Core.App;
using Core.Meta.GameLoader.App;
using Core.Modules;
using UnityEngine;

namespace Core.Meta.GameLoader.Modules {
	[CreateAssetMenu(menuName = "Modules/Meta/GameLoaderModule", fileName = "GameLoaderModule")]
	public class GameLoaderModule : AppModule {
		public override void OnInitialize (AppComponentRegistry componentRegistry) {
			base.OnInitialize(componentRegistry);

			componentRegistry.Instantiate<GameLoaderService>();
		}
	}
}
