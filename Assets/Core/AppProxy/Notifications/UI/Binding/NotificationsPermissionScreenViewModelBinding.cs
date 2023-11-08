using Core.AppProxy.Notifications.UI.ViewModels;
using Core.Binding;

namespace Core.AppProxy.Notifications.UI.Binding {
	public class NotificationsPermissionScreenViewModelBinding : BindingItemView<NotificationsPermissionScreenViewModel> {
		public void AcceptPermission () {
			target.AcceptPermission();
		}
		
		public void DenyPermission () {
			target.DenyPermission();
		}
	}
}
