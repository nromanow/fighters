using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace Core.Network.Api {
	public interface IAppHttpRequestsService {
		UniTask<string> SendPostRequest (string url, Dictionary<string, object> body, CancellationToken cancellationToken);
	}
}
