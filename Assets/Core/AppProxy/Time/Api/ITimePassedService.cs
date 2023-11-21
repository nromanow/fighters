using System;

namespace Core.AppProxy.Time.Api {
	public interface ITimePassedService {
		bool IsCurrentlyTimeHasPassed (DateTime currentlyTime);
	}
}
