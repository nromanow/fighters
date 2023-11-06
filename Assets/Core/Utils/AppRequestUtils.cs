using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

namespace Core.Utils {
	public static class AppRequestUtils {
		public static async UniTask<Dictionary<string, object>> GetPostRequestResponse (string url, Dictionary<string, object> data, CancellationToken cancellationToken) {
			using var request = new UnityWebRequest(url, "POST");

			var bodyRaw = Encoding.UTF8.GetBytes(JsonUtility.ToJson(data));

			request.uploadHandler = new UploadHandlerRaw(bodyRaw);
			request.downloadHandler = new DownloadHandlerBuffer();

			request.SetRequestHeader("Content-Type", "application/json");

			await request.SendWebRequest();

			while (!request.isDone) {
				if (cancellationToken.IsCancellationRequested) {
					Debug.Log("Task {0} cancelled");
					cancellationToken.ThrowIfCancellationRequested();
				}
				await UniTask.Yield();
			}
			
			if (request.error != null) {
				throw new Exception(request.error);
			}
			
			var response = request.downloadHandler.text;
			var responseDictionary = JsonUtility.FromJson<Dictionary<string, object>>(response);
			
			return responseDictionary;
		}
	}
}
