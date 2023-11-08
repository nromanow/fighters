using Core.AppProxy.Notifications.UI.Api;
using Core.AppProxy.Notifications.UI.ViewModels;
using Core.UI.Api;
using Core.UI.Data.Forms;
using System;
using UnityEngine;

namespace Core.AppProxy.Notifications.UI.App {
	public class NotificationsUIScreenService : INotificationsUIScreenService {
		private readonly Settings _settings;
		private readonly IUIScreenService _uiScreenService;

		public NotificationsUIScreenService (Settings settings, IUIScreenService uiScreenService) {
			_settings = settings;
			_uiScreenService = uiScreenService;
		}

		public void ShowPermissionScreen (NotificationsPermissionScreenViewModel viewModel) {
			_uiScreenService.ShowForm(_settings.permissionScreenReference, viewModel);
		}

		public void ClosePermissionScreen () {
			_uiScreenService.CloseForm(_settings.permissionScreenReference);
		}

		[Serializable]
		public class Settings {
			[SerializeField]
			private GUIForm _permissionScreenReference;

			public GUIForm permissionScreenReference => _permissionScreenReference;
		}
	}
}
