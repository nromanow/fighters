using Core.Binding;
using Core.Meta.Menu.UI.ViewModels;

namespace Core.Meta.Menu.UI.Binding {
	public class MenuViewModelBinding : BindingItemView<MenuScreenViewModel> {
		public void Play () {
			target.Play();
		}
		
		public void OpenPrivacyPolicy () {
			target.OpenPrivacyPolicy();
		}
		
		protected override void OnInitialize () {
			base.OnInitialize();
			
			this.Subscribe(target.openPrivacyPolicy);
			this.Subscribe(target.play);
		}
	}
}
