using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace Core.AppProxy.StartupProxy.Api {
	public interface IAppStartupProxyApi {
		UniTask<Dictionary<string, object>> GetConfig (Dictionary<string, object> conversionData, CancellationToken cancellationToken);
	}
}
