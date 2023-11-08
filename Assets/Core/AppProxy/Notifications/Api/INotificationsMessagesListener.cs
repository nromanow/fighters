using System;

namespace Core.AppProxy.Notifications.Api {
	public interface INotificationsMessagesListener {
		void Initialize ();

		void SubscribeOnMessageParamByKey (string key, Action<string> onParamReceived);
	}
}
