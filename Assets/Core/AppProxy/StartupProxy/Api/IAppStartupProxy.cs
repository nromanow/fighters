using Cysharp.Threading.Tasks;
using System.Threading;

namespace Core.AppProxy.StartupProxy.Api {
	public interface IAppStartupProxy {
		UniTask Startup (CancellationToken cancellationToken);
	}
}
