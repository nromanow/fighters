using Core.AppProxy.Notifications.Api;
using Firebase.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.AppProxy.Notifications.App {
	public class NotificationsMessagesListener : INotificationsMessagesListener {
		private readonly Dictionary<string, Action<string>> _params = new();
		
		public void Initialize () {
			FirebaseMessaging.MessageReceived += OnMessageReceived;
		}

		public void SubscribeOnMessageParamByKey (string key, Action<string> onParamReceived) {
			_params.Add(key, onParamReceived);
		}

		private void OnMessageReceived (object sender, MessageReceivedEventArgs e) {
			var messageData = e.Message.Data;

			var allKeys = messageData.Select(x => x.Key);

			foreach (var key in allKeys) {
				if (_params.Any(x => x.Key == key)) {
					_params[key].Invoke(messageData[key]);
				}
			}
		}
	}
}
