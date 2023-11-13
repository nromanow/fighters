using System;
using UniRx;

namespace Core.AppProxy.Notifications.Api {
	public interface INotificationsMessagesListener {
		IReadOnlyReactiveProperty<string> pushToken { get; }

		void StartListen ();

		void SubscribeOnMessageParamByKey (string key, Action<string> onParamReceived);
	}
}
