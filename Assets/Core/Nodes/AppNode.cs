using Core.App;
using Core.Modules;
using UnityEngine;

namespace Core.Nodes {
	[CreateAssetMenu(menuName = "Nodes/BaseAppNode")]
	public class AppNode : ScriptableObject {
		[SerializeField]
		private AppModule[] _modules;
		
		private AppComponentRegistry _componentRegistry = new();
		
		public void InitializeNode () {
			_componentRegistry = new AppComponentRegistry();
			
			foreach (var module in _modules) {
				module.OnInitialize(_componentRegistry);
			}
		}
		
		public void DisposeNode () {
			foreach (var module in _modules) {
				module.OnDispose();
			}
			
			_componentRegistry.Dispose();
			
			Debug.Log($"Node [{this.name} is disposed]");
		}
	}
}
