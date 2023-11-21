using Core.AppProxy.Time.Api;
using System;
using UnityEngine;

namespace Core.AppProxy.Time.App {
	public class TimePassedService : ITimePassedService {
		private readonly Settings _settings;

		public TimePassedService (Settings settings) {
			_settings = settings;
		}

		public bool IsCurrentlyTimeHasPassed (DateTime currentlyTime) {
			var passionTime =
				new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
					.AddSeconds(_settings.passionTimeStamp);
			
			return currentlyTime >= passionTime;
		}

		[Serializable]
		public class Settings {
			[SerializeField]
			private int _passionTimeStamp;

			public int passionTimeStamp => _passionTimeStamp;
		}
	}
}
