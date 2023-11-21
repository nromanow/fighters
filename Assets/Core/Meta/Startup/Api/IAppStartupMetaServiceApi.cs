using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace Core.Meta.Startup.Api {
	public interface IAppStartupMetaServiceApi {
		UniTask<Dictionary<string, object>> GetConfig (Dictionary<string, object> conversionData, CancellationToken cancellationToken);
	}
}
