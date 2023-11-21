using Core.Gameplay.Menu.UI.ViewModels;

namespace Core.Gameplay.Menu.UI.Api {
	public interface IMenuUIScreenService {
		void ShowMenuScreen (MenuScreenViewModel viewModel);
		
		void CloseMenuScreen ();
	}
}
