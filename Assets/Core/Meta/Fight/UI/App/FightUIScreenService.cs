using Core.Meta.Fight.UI.Api;
using Core.Meta.Fight.UI.ViewModels;
using Core.UI.Api;
using Core.UI.Data.Forms;
using System;
using UnityEngine;

namespace Core.Meta.Fight.UI.App {
	public class FightUIScreenService : IFightUIScreenService {
		private readonly Settings _settings;
		private readonly IUIScreenService _uiScreenService;

		public FightUIScreenService (Settings settings, IUIScreenService uiScreenService) {
			_settings = settings;
			_uiScreenService = uiScreenService;
		}

		public void ShowFightHUD (FightHudViewModel viewModel) {
			_uiScreenService.ShowForm(_settings.fightHUDForm, viewModel);
		}

		public void ShowPostMatchScreen (PostMatchScreenViewModel viewModel) {
			_uiScreenService.ShowForm(_settings.postMatchScreenForm, viewModel);
		}

		public void HideFightHUD () {
			_uiScreenService.CloseForm(_settings.fightHUDForm);
		}

		public void HidePostMatchScreen () {
			_uiScreenService.CloseForm(_settings.postMatchScreenForm);
		}

		[Serializable]
		public class Settings {
			[SerializeField]
			private GUIForm _fightHUDForm;

			[SerializeField]
			private GUIForm _postMatchScreenForm;

			public GUIForm fightHUDForm => _fightHUDForm;
			public GUIForm postMatchScreenForm => _postMatchScreenForm;
		}
	}
}
