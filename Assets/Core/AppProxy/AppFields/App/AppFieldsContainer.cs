using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.AppProxy.AppFields.App {
	public class AppFieldsContainer : IDisposable {
		private const string PLAYER_PREFS_KEY = "app_fields";
		
		private Dictionary<string, ValuePack> _values = new();

		public void Initialize () {
			if (!PlayerPrefs.HasKey(PLAYER_PREFS_KEY)) {
				return;
			}
			
			var json = PlayerPrefs.GetString(PLAYER_PREFS_KEY);
			_values = JsonUtility.FromJson<Dictionary<string, ValuePack>>(json);
		}
		
		public void AddValue (string key, object value, int expirationTimeStamp) {
			var dateTime =
				new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
					.AddSeconds(expirationTimeStamp);
			
			_values.Add(key, new ValuePack {
				value = value,
				expirationDate = dateTime,
			});
		}

		public bool HasNotExpiredValue (string key, out object value) {
			if (_values.TryGetValue(key, out var valuePack)) {
				if (valuePack.expirationDate > DateTime.UtcNow) {
					value = valuePack.value;
					return true;
				}
			}

			value = null;
			return false;
		}
		
		private void SaveContainer () {
			var json = JsonUtility.ToJson(_values);
			PlayerPrefs.SetString(PLAYER_PREFS_KEY, json);
		}
		
		public void Dispose() {
			SaveContainer();
		}
		
		private struct ValuePack {
			public object value;
			public DateTime expirationDate;
		}
	}
}
