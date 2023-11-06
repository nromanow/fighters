using System.Collections.Generic;
using UniRx;

namespace Core.AppProxy.AppsFlyerConversion.Api {
	public interface IAppsFlyerListener {
		ReactiveCommand<Dictionary<string, object>> onConversionDataReceived { get; }
		ReactiveCommand onConversionDataFailed { get; }
	}
}
