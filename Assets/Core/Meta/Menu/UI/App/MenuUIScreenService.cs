using Core.Meta.Menu.UI.Api;
using Core.Meta.Menu.UI.ViewModels;
using Core.UI.Api;
using Core.UI.Data.Forms;
using System;
using UnityEngine;

namespace Core.Meta.Menu.UI.App {
	public class MenuUIScreenService : IMenuUIScreenService {
		private readonly Settings _settings;
		private readonly IUIScreenService _uiScreenService;

		public MenuUIScreenService (Settings settings, IUIScreenService uiScreenService) {
			_settings = settings;
			_uiScreenService = uiScreenService;
		}

		public void ShowMenuScreen (MenuScreenViewModel viewModel) {
			_uiScreenService.ShowForm(_settings.menuScreenReference, viewModel);
		}
		
		public void CloseMenuScreen () {
			_uiScreenService.CloseForm(_settings.menuScreenReference);
		}

		[Serializable]
		public class Settings {
			[SerializeField]
			private GUIForm _menuScreenReference;

			public GUIForm menuScreenReference => _menuScreenReference;
		}
	}
}
