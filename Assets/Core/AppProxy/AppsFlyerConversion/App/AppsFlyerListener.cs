﻿using AppsFlyerSDK;
using Core.AppProxy.AppsFlyerConversion.Api;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Core.AppProxy.AppsFlyerConversion.App {
	public class AppsFlyerListener : MonoBehaviour, IAppsFlyerListener, IAppsFlyerConversionData {
		public ReactiveCommand<Dictionary<string, object>> onConversionDataReceived { get; } = new();
		public ReactiveCommand onConversionDataFailed { get; } = new();

		public void onConversionDataSuccess (string conversionData) {
			AppsFlyer.AFLog("onConversionDataSuccess", conversionData);
			
			onConversionDataReceived
				.Execute(AppsFlyer.CallbackStringToDictionary(conversionData));
		}

		public void onConversionDataFail (string error) {
			AppsFlyer.AFLog("onConversionDataFail", error);
			onConversionDataFailed.Execute();
		}

		public void onAppOpenAttribution (string attributionData) {
			AppsFlyer.AFLog("onAppOpenAttribution: This method was replaced by UDL. This is a fake call.", attributionData);
		}

		public void onAppOpenAttributionFailure (string error) {
			AppsFlyer.AFLog("onAppOpenAttributionFailure: This method was replaced by UDL. This is a fake call.", error);
		}

		private void OnDestroy () {
			onConversionDataReceived.Dispose();
			onConversionDataFailed.Dispose();
		}
	}
}
