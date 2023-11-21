using Core.Meta.Notifications.UI.ViewModels;

namespace Core.Meta.Notifications.UI.Api {
	public interface INotificationsUIScreenService {
		void ShowPermissionScreen (NotificationsPermissionScreenViewModel viewModel);
		
		void ClosePermissionScreen ();
	}
}
