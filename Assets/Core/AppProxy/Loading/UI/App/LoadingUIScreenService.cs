using Core.AppProxy.Loading.UI.Api;
using Core.AppProxy.Loading.UI.ViewModels;
using Core.UI.Api;
using Core.UI.Data.Forms;
using System;
using UnityEngine;

namespace Core.AppProxy.Loading.UI.App {
	public class LoadingUIScreenService : ILoadingUIScreenService {
		private readonly Settings _settings;
		private readonly IUIScreenService _uiScreenService;

		public LoadingUIScreenService (Settings settings, IUIScreenService uiScreenService) {
			_settings = settings;
			_uiScreenService = uiScreenService;
		}

		public void OpenLoadingScreen (LoadingScreenViewModel viewModel) {
			_uiScreenService.ShowForm(_settings.loadingScreenReference, viewModel);
		}

		public void CloseLoadingScreen () {
			_uiScreenService.CloseForm(_settings.loadingScreenReference);
		}

		[Serializable]
		public class Settings {
			[SerializeField]
			private GUIForm _loadingScreenReference;

			public GUIForm loadingScreenReference => _loadingScreenReference;
		}
	}
}
