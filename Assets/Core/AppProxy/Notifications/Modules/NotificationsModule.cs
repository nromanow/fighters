using Core.App;
using Core.AppProxy.Notifications.App;
using Core.AppProxy.Notifications.UI.App;
using UnityEngine;

namespace Core.AppProxy.Notifications.Modules {
	[CreateAssetMenu(menuName = "Modules/AppProxy/NotificationsModule", fileName = "NotificationsModule")]
	public class NotificationsModule : AppModule {
		[SerializeField]
		private NotificationsUIScreenService.Settings _uiSettings;

		public override void OnInitialize (AppComponentRegistry componentRegistry) {
			base.OnInitialize(componentRegistry);

			componentRegistry.Instantiate<NotificationsUIScreenService>(_uiSettings);
			componentRegistry.Instantiate<NotificationsPermissionService>();
			componentRegistry.Instantiate<NotificationsMessagesListener>();
		}
	}
}
