using Core.App;
using Core.Meta.Notifications.App;
using Core.Meta.Notifications.UI.App;
using Core.Modules;
using UnityEngine;

namespace Core.Meta.Notifications.Modules {
	[CreateAssetMenu(menuName = "Modules/Meta/NotificationsModule", fileName = "NotificationsModule")]
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
