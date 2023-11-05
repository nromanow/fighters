using Core.Meta.Menu.UI.ViewModels;

namespace Core.Meta.Menu.UI.Api {
	public interface IMenuUIScreenService {
		void ShowMenuScreen (MenuScreenViewModel viewModel);
		
		void CloseMenuScreen ();
	}
}
