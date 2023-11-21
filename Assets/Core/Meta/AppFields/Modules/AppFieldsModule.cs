using Core.App;
using Core.Meta.AppFields.App;
using Core.Modules;
using UnityEngine;

namespace Core.Meta.AppFields.Modules {
	[CreateAssetMenu(menuName = "Modules/Meta/AppFieldsModule", fileName = "AppFieldsModule")]
	public class AppFieldsModule : AppModule {
		public override void OnInitialize (AppComponentRegistry componentRegistry) {
			base.OnInitialize(componentRegistry);

			componentRegistry
				.Instantiate<AppFieldsContainer>()
				.Initialize();
		}
	}
}
