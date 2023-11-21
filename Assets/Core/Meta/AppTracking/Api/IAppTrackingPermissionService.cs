using Cysharp.Threading.Tasks;
using System.Threading;

namespace Core.Meta.AppTracking.Api {
	public interface IAppTrackingPermissionService {
		bool hasPermission { get; }
		
		UniTask RequestPermission (CancellationToken cancellationToken);
	}
}
