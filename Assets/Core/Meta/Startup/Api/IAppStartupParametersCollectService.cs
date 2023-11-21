using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace Core.Meta.Startup.Api {
	public interface IAppStartupParametersCollectService {
		UniTask<Dictionary<string, object>> GetParams (CancellationToken cancellationToken);
	}
}
