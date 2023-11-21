using Core.Binding;
using Core.Gameplay.Fight.UI.ViewModels;

namespace Core.Gameplay.Fight.UI.Binding {
	public class FightHudViewModelBinding : BindingItemView<FightHudViewModel> {
		public void PressRedButton () {
			target.PressRedButton();
		}
		
		public void PressBlueButton () {
			target.PressBlueButton();
		}
	}
}
