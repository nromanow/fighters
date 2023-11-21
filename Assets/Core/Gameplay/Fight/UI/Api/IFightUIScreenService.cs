using Core.Gameplay.Fight.UI.ViewModels;

namespace Core.Gameplay.Fight.UI.Api {
	public interface IFightUIScreenService {
		void ShowFightHUD (FightHudViewModel viewModel);

		void ShowPostMatchScreen (PostMatchScreenViewModel viewModel);

		void HideFightHUD ();
		
		void HidePostMatchScreen ();
	}
}
