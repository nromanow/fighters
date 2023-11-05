using UnityEngine;

namespace Core.App {
	public class AppModule : ScriptableObject {
		public virtual void OnInitialize (AppComponentRegistry componentRegistry) { }
		public virtual void OnDispose () { }
	}
}
