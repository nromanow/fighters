using System.Collections.Generic;
using UniRx;

namespace Core.Meta.AppsFlyerConversion.Api {
	public interface IAppsFlyerListener {
		ReactiveCommand<Dictionary<string, object>> onConversionDataReceived { get; }
		ReactiveCommand onConversionDataFailed { get; }
	}
}
