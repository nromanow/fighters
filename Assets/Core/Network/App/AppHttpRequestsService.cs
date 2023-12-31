using Core.Network.Api;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

namespace Core.Network.App {
	public class AppHttpRequestsService : IAppHttpRequestsService {
		public async UniTask<string> SendPostRequest (string url, Dictionary<string, object> body, CancellationToken cancellationToken) {
			using var request = new UnityWebRequest(url, "POST");
			var bodyRaw = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(body));
			
			request.uploadHandler = new UploadHandlerRaw(bodyRaw);
			request.downloadHandler = new DownloadHandlerBuffer();
			
			request.SetRequestHeader("Content-Type", "application/json");
			
			Debug.Log($"Request"
				+ $"\n[URL]: [{request.url}]"
				+ $"\n[UPLOAD DATA]: [{Encoding.UTF8.GetString(request.uploadHandler.data)}]"
				+ $"\n[METHOD]: [{request.method}]"
				+ $"\n[TIME]: [{DateTime.UtcNow:U}]");

			request.SendWebRequest();

			while (!request.isDone) {
				if (cancellationToken.IsCancellationRequested) {
					cancellationToken.ThrowIfCancellationRequested();
				}
				await UniTask.Yield();
			}

			if (request.error != null) {
				Debug.LogError($"Request [{request.url}] error: [{request.error}]");
			}

			var response = request.downloadHandler.text;
			
			Debug.Log($"Response: [DATA]: [{response}]");

			return response;
		}
	}
}
