using System;
using UniRx;

namespace Core.Meta.Notifications.UI.ViewModels {
	public class NotificationsPermissionScreenViewModel : IDisposable {
		public ReactiveCommand<bool> permissionExcecuted { get; } = new();

		public void AcceptPermission () {
			permissionExcecuted.Execute(true);
		}

		public void DenyPermission () {
			permissionExcecuted.Execute(false);
		}
		
		public void Dispose () {
			permissionExcecuted?.Dispose();
		}
	}
}
