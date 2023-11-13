using Newtonsoft.Json;
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
			_values = JsonConvert.DeserializeObject<Dictionary<string, ValuePack>>(json);
		}
		
		public void AddValue (string key, object value, int expirationTimeStamp) {
			var dateTime =
				new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
					.AddSeconds(expirationTimeStamp);

			var valuePack = new ValuePack() {
				value = value,
				expirationDate = dateTime,
			};

			if (_values.TryGetValue(key, out var oldValue)) {
				oldValue.value = valuePack;
			}
			else {
				_values.Add(key, valuePack);
			}
			
			Debug.Log($"Value of key [{key}] added");
			
			SaveContainer();
		}

		public bool HasNotExpiredValue (string key, out object value) {
			if (_values.TryGetValue(key, out var valuePack)) {
				value = valuePack.value;
				
				if (valuePack.expirationDate > DateTime.UtcNow) {
					Debug.Log($"Value of key {key} is available");
					return true;
				}
				
				Debug.Log($"Value of key {key} already expired");
				return false;
			}

			Debug.Log($"Value of key [{key}] not found");
			
			value = null;
			return false;
		}
		
		private void SaveContainer () {
			var json = JsonConvert.SerializeObject(_values);
			PlayerPrefs.SetString(PLAYER_PREFS_KEY, json);
		}
		
		public void Dispose() {
			SaveContainer();
		}
		
		private class ValuePack {
			public object value;
			public DateTime expirationDate;
		}
	}
}
