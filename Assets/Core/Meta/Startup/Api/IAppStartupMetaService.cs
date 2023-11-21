using Cysharp.Threading.Tasks;
using System.Threading;

namespace Core.Meta.Startup.Api {
	public interface IAppStartupMetaService {
		UniTask Startup (CancellationToken cancellationToken);
	}
}
