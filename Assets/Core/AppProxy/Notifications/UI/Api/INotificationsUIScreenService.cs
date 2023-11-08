using Core.AppProxy.Notifications.UI.ViewModels;

namespace Core.AppProxy.Notifications.UI.Api {
	public interface INotificationsUIScreenService {
		void ShowPermissionScreen (NotificationsPermissionScreenViewModel viewModel);
		
		void ClosePermissionScreen ();
	}
}
