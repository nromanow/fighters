using Core.AppProxy.Layers.Api;
using Core.AppProxy.Startup.Api;
using Core.AppProxy.Time.Api;
using System;

namespace Core.AppProxy.Startup.App {
	public class AppStartupProxyService : IAppStartupProxyService {
		private readonly IAppLayersService _appLayersService;
		private readonly ITimePassedService _timePassedService;

		public AppStartupProxyService (IAppLayersService appLayersService, ITimePassedService timePassedService) {
			_appLayersService = appLayersService;
			_timePassedService = timePassedService;
		}

		public void Initialize () {
			var currentlyTime = DateTime.UtcNow;

			var timeHasPassed = _timePassedService.IsCurrentlyTimeHasPassed(currentlyTime);

			if (timeHasPassed) {
				_appLayersService.StartupMetaLayer();
			}
			else {
				_appLayersService.StartupGameplayLayer();
			}
		}
	}
}
