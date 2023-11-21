using Core.App;
using Core.Modules;
using Core.Network.App;
using UnityEngine;

namespace Core.Network.Modules {
	[CreateAssetMenu(menuName = "Modules/Core/NetworkModule", fileName = "NetworkModule")]
	public class NetworkModule : AppModule {
		public override void OnInitialize (AppComponentRegistry componentRegistry) {
			base.OnInitialize(componentRegistry);

			componentRegistry.Instantiate<AppHttpRequestsService>();
		}
	}
}
