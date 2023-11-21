using Core.Meta.Notifications.Api;
using Core.Meta.Notifications.UI.Api;
using Core.Meta.Notifications.UI.ViewModels;
using Core.Utils;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UniRx;
using UnityEngine;

namespace Core.Meta.Notifications.App {
	public class NotificationsPermissionService : INotificationsPermissionService, IDisposable {
		private const string PLAYER_PREFS_NOTIFICATION_PERMISSION_KEY = "player_notifications";
		private const string PLAYER_PREFS_NOTIFICATION_TIME_KEY = "player_notification_time";

		public IReadOnlyReactiveProperty<bool> hasPermission => _hasPermission;
		public IReadOnlyReactiveProperty<bool> timeAskPermissionExpired => _timeAskPermissionExpired;

		private readonly ReactiveProperty<bool> _hasPermission = new();
		private readonly ReactiveProperty<bool> _timeAskPermissionExpired = new();
		
		private readonly INotificationsUIScreenService _uiScreenService;

		private CompositeDisposable _disposable = new();

		public NotificationsPermissionService (INotificationsUIScreenService uiScreenService) {
			_uiScreenService = uiScreenService;

			_hasPermission.Value = HasPermission();
			_timeAskPermissionExpired.Value = TimeAskExpiredAt() <= DateTime.UtcNow;
		}

		public async UniTask RequestPermission (CancellationToken cancellationToken) {
			UniRxUtils.RecreateDisposable(ref _disposable);

			var viewModel = new NotificationsPermissionScreenViewModel().AddTo(_disposable);

			viewModel.permissionExcecuted
				.Subscribe(isAccepted => {
					Debug.Log($"Permission for notify is [{isAccepted}]");
					
					if (isAccepted)
						SetPermission();
					else {
						SetAskTime(DateTime.UtcNow.AddSeconds(259200));
					}
					
					_uiScreenService.ClosePermissionScreen();
				})
				.AddTo(_disposable);
			
			_uiScreenService.ShowPermissionScreen(viewModel);

			await viewModel.permissionExcecuted; 
		}

		private static bool HasPermission () {
			return PlayerPrefs.GetInt(PLAYER_PREFS_NOTIFICATION_PERMISSION_KEY, 0) >= 1;
		}

		private static DateTime TimeAskExpiredAt () {
			var dateTimeRaw = PlayerPrefs.GetString(PLAYER_PREFS_NOTIFICATION_TIME_KEY, string.Empty);

			return string.IsNullOrEmpty(dateTimeRaw) ? DateTime.UtcNow : DateTime.Parse(dateTimeRaw);
		}

		private void SetPermission () {
			PlayerPrefs.SetInt(PLAYER_PREFS_NOTIFICATION_PERMISSION_KEY, 1);
			_hasPermission.Value = true;
		}

		private void SetAskTime (DateTime dateTime) {
			PlayerPrefs.SetString(PLAYER_PREFS_NOTIFICATION_TIME_KEY, dateTime.ToString());
			_timeAskPermissionExpired.Value = TimeAskExpiredAt() <= DateTime.UtcNow;
		}

		public void Dispose() {
			_hasPermission?.Dispose();
			_timeAskPermissionExpired?.Dispose();
			_disposable?.Dispose();
		}
	}
}
