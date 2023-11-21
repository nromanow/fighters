using Core.Gameplay.Fight.Api;
using Core.Gameplay.Fight.Models;
using Core.Gameplay.Fight.UI.Api;
using Core.Gameplay.Fight.UI.ViewModels;
using Core.Utils;
using System;
using UniRx;

namespace Core.Gameplay.Fight.App {
	public class FightStartService : IFightStartService, IDisposable {
		private readonly IFightLoaderService _fightLoaderService;
		private readonly IFightUIScreenService _fightUIScreenService;
		private readonly IFightProvider _fightProvider;
		private readonly IFightViewInitializeService _fightViewInitializeService;

		private CompositeDisposable _disposable = new();

		public FightStartService (
			IFightLoaderService fightLoaderService, 
			IFightUIScreenService fightUIScreenService, 
			IFightProvider fightProvider,
			IFightViewInitializeService fightViewInitializeService) {
			_fightLoaderService = fightLoaderService;
			_fightUIScreenService = fightUIScreenService;
			_fightProvider = fightProvider;
			_fightViewInitializeService = fightViewInitializeService;
		}

		public void InitializeFight () {
			_fightLoaderService.LoadFightScene(StartFight);
		}

		private void StartFight () {
			UniRxUtils.RecreateDisposable(ref _disposable);

			var redFighter = new FighterModel().AddTo(_disposable);
			var blueFighter = new FighterModel().AddTo(_disposable);

			_fightProvider.RegisterRedFighter(redFighter);
			_fightProvider.RegisterBlueFighter(blueFighter);

			redFighter.kick
				.Subscribe(_ => _fightProvider.RegisterRedKick())
				.AddTo(_disposable);

			blueFighter.kick
				.Subscribe(_ => _fightProvider.RegisterBlueKick())
				.AddTo(_disposable);

			_fightViewInitializeService.InitFighters(redFighter, blueFighter);

			var hudViewModel = new FightHudViewModel().AddTo(_disposable);

			hudViewModel.redButtonPressed
				.Subscribe(_ => redFighter.Kick())
				.AddTo(_disposable);

			hudViewModel.blueButtonPressed
				.Subscribe(_ => blueFighter.Kick())
				.AddTo(_disposable);

			_fightProvider.win
				.Subscribe(_ => {
					var postMatchViewModel =
						new PostMatchScreenViewModel(
								_fightProvider.redScore.Value > _fightProvider.blueScore.Value,
								_fightProvider.redScore.Value,
								_fightProvider.blueScore.Value)
							.AddTo(_disposable);

					postMatchViewModel.restartButtonPress
						.Subscribe(_ => {
							_fightUIScreenService.HidePostMatchScreen();
							InitializeFight();
						})
						.AddTo(_disposable);

					_fightUIScreenService.ShowPostMatchScreen(postMatchViewModel);
					_fightUIScreenService.HideFightHUD();
				})
				.AddTo(_disposable);

			_fightUIScreenService.ShowFightHUD(hudViewModel);
		}

		public void Dispose () {
			_disposable?.Dispose();
		}
	}
}
