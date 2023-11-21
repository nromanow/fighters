using Core.Meta.AppTracking.Api;
using Cysharp.Threading.Tasks;
using System.Threading;
using Unity.Advertisement.IosSupport;
using UnityEngine;

namespace Core.Meta.AppTracking.App {
	public class AppTrackingPermissionService : IAppTrackingPermissionService {
		public bool hasPermission => ATTrackingStatusBinding.GetAuthorizationTrackingStatus()
			== ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED;
		
		public async UniTask RequestPermission (CancellationToken cancellationToken) {
#if UNITY_IOS
			// Check with iOS to see if the user has accepted or declined tracking
			if (ATTrackingStatusBinding.GetAuthorizationTrackingStatus()
				!= ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED) return;

			Debug.Log("Unity iOS Support: Requesting iOS App Tracking Transparency native dialog.");

			ATTrackingStatusBinding.RequestAuthorizationTracking();

			await RequestPermissionStatus(cancellationToken);
#else
            Debug.Log("Unity iOS Support: App Tracking Transparency status not checked, because the platform is not iOS.");
#endif
		}

		private static async UniTask RequestPermissionStatus (CancellationToken cancellationToken) {
			while (!cancellationToken.IsCancellationRequested) {
				if (ATTrackingStatusBinding.GetAuthorizationTrackingStatus()
					== ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED) {
					return;
				}

				await UniTask.Yield();
			}
		}
	}
}
