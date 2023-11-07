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
			
			Debug.Log($"Request sent {JsonConvert.SerializeObject(request)}");

			await request.SendWebRequest();

			while (!request.isDone) {
				if (cancellationToken.IsCancellationRequested) {
					cancellationToken.ThrowIfCancellationRequested();
				}
				await UniTask.Yield();
			}

			if (request.error != null) {
				var ex = new Exception(request.error);
				Debug.LogException(ex);

				throw ex;
			}

			var response = request.downloadHandler.text;

			return response;
		}
	}
}
