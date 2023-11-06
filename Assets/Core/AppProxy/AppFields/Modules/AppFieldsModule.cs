using Core.App;
using Core.AppProxy.AppFields.App;
using UnityEngine;

namespace Core.AppProxy.AppFields.Modules {
	[CreateAssetMenu(menuName = "Modules/AppProxy/AppFieldsModule", fileName = "AppFieldsModule")]
	public class AppFieldsModule : AppModule {
		public override void OnInitialize (AppComponentRegistry componentRegistry) {
			base.OnInitialize(componentRegistry);

			componentRegistry
				.Instantiate<AppFieldsContainer>()
				.Initialize();
		}
	}
}
