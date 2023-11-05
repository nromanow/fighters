using Core.Meta.Fight.UI.ViewModels;

namespace Core.Meta.Fight.UI.Api {
	public interface IFightUIScreenService {
		void ShowFightHUD (FightHudViewModel viewModel);

		void ShowPostMatchScreen (PostMatchScreenViewModel viewModel);

		void HideFightHUD ();
		
		void HidePostMatchScreen ();
	}
}
