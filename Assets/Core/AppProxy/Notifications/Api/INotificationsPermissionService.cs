using Cysharp.Threading.Tasks;
using System.Threading;
using UniRx;

namespace Core.AppProxy.Notifications.Api {
	public interface INotificationsPermissionService {
		IReadOnlyReactiveProperty<bool> hasPermission { get; }
		IReadOnlyReactiveProperty<bool> timeAskPermissionExpired { get; }

		UniTask RequestPermission (CancellationToken cancellationToken);
	}
}
