using Cysharp.Threading.Tasks;
using System.Threading;

namespace Core.AppProxy.AppTracking.Api {
	public interface IAppTrackingPermissionService {
		UniTask RequestPermission (CancellationToken cancellationToken);
	}
}
