using Firebase.Messaging;

namespace Core.Meta.Messages.App {
	public class MessageProxy {
		public void Initialize () {
			FirebaseMessaging.MessageReceived += FirebaseMessagingOnMessageReceived;
		}

		private void FirebaseMessagingOnMessageReceived (object sender, MessageReceivedEventArgs e) {
			
		}
	}
}
