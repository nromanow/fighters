using Core.App;
using Core.AppProxy.GameLoader.App;
using UnityEngine;

namespace Core.AppProxy.GameLoader.Modules {
	[CreateAssetMenu(menuName = "Modules/AppProxy/GameLoaderModule", fileName = "GameLoaderModule")]
	public class GameLoaderModule : AppModule {
		public override void OnInitialize (AppComponentRegistry componentRegistry) {
			base.OnInitialize(componentRegistry);

			componentRegistry.Instantiate<GameLoaderService>();
		}
	}
}
