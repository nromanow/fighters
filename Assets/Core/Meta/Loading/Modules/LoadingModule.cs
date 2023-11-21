using Core.App;
using Core.Meta.Loading.App;
using Core.Meta.Loading.UI.App;
using Core.Modules;
using UnityEngine;

namespace Core.Meta.Loading.Modules {
	[CreateAssetMenu(menuName = "Modules/Meta/LoadingModule", fileName = "LoadingModule")]
	public class LoadingModule : AppModule {
		[SerializeField]
		private LoadingUIScreenService.Settings _uiSettings;

		public override void OnInitialize (AppComponentRegistry componentRegistry) {
			base.OnInitialize(componentRegistry);

			componentRegistry.Instantiate<LoadingUIScreenService>(_uiSettings);
			componentRegistry.Instantiate<LoadingScreenService>();
		}
	}
}
