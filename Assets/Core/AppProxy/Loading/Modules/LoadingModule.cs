using Core.App;
using Core.AppProxy.Loading.App;
using Core.AppProxy.Loading.UI.App;
using UnityEngine;

namespace Core.AppProxy.Loading.Modules {
	[CreateAssetMenu(menuName = "Modules/AppProxy/LoadingModule", fileName = "LoadingModule")]
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
