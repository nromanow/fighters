using Core.Binding;
using Core.Meta.Notifications.UI.ViewModels;

namespace Core.Meta.Notifications.UI.Binding {
	public class NotificationsPermissionScreenViewModelBinding : BindingItemView<NotificationsPermissionScreenViewModel> {
		public void AcceptPermission () {
			target.AcceptPermission();
		}
		
		public void DenyPermission () {
			target.DenyPermission();
		}
	}
}
