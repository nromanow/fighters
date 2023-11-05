using Core.Meta.Fight.Api;
using Core.Meta.Menu.Api;
using Core.Meta.Menu.UI.Api;
using Core.Meta.Menu.UI.ViewModels;
using Core.Utils;
using System;
using UniRx;
using UnityEngine;

namespace Core.Meta.Menu.App {
	public class MenuScreenService : IMenuScreenService, IDisposable {
		private readonly Settings _settings;
		private readonly IMenuUIScreenService _menuUIScreenService;
		private readonly IFightStartService _fightStartService;

		private CompositeDisposable _disposable = new();

		public MenuScreenService (
			Settings settings, 
			IMenuUIScreenService menuUIScreenService,
			IFightStartService fightStartService) {
			_settings = settings;
			_menuUIScreenService = menuUIScreenService;
			_fightStartService = fightStartService;
		}

		public void OpenMenuScreen () {
			UniRxUtils.RecreateDisposable(ref _disposable);

			var viewModel = new MenuScreenViewModel().AddTo(_disposable);

			viewModel.openPrivacyPolicy
				.Subscribe(_ => Application.OpenURL(_settings.privacyPolicyUrl))
				.AddTo(_disposable);
			
			viewModel.play
				.Subscribe(_ => {
					_menuUIScreenService.CloseMenuScreen();
					
					_fightStartService.InitializeFight();
				})
				.AddTo(_disposable);
			
			_menuUIScreenService.ShowMenuScreen(viewModel);
		}

		public void Dispose () {
			_disposable?.Dispose();
		}

		[Serializable]
		public class Settings {
			[SerializeField]
			private string _privacyPolicyUrl;

			public string privacyPolicyUrl => _privacyPolicyUrl;
		}
	}
}
