using Cysharp.Threading.Tasks;
using System.Threading;

namespace Core.AppProxy.AppTracking.Api {
	public interface IAppTrackingPermissionService {
		bool hasPermission { get; }
		
		UniTask RequestPermission (CancellationToken cancellationToken);
	}
}
