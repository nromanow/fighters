using Core.AppProxy.Notifications.Api;
using Firebase.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Core.AppProxy.Notifications.App {
	public class NotificationsMessagesListener : INotificationsMessagesListener, IDisposable {
		public IReadOnlyReactiveProperty<string> pushToken => _pushToken;

		private readonly ReactiveProperty<string> _pushToken = new();
		private readonly Dictionary<string, Action<string>> _params = new();

		public void StartListen () {
			FirebaseMessaging.TokenReceived += OnTokenReceived;
			FirebaseMessaging.MessageReceived += OnMessageReceived;
		}

		private void OnTokenReceived (object sender, TokenReceivedEventArgs e) {
			_pushToken.Value = e.Token;
			
			Debug.Log($"New push token: [{e.Token}]");
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

		public void Dispose () {
			_pushToken?.Dispose();
		}
	}
}
