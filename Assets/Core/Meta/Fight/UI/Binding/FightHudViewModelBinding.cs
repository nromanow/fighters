using Core.Binding;
using Core.Meta.Fight.UI.ViewModels;

namespace Core.Meta.Fight.UI.Binding {
	public class FightHudViewModelBinding : BindingItemView<FightHudViewModel> {
		public void PressRedButton () {
			target.PressRedButton();
		}
		
		public void PressBlueButton () {
			target.PressBlueButton();
		}
	}
}
