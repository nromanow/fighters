using UnityEngine;

namespace Core.App {
	public class AppStartup : MonoBehaviour {
		[SerializeField]
		private AppNode[] _nodes;

		private void Awake () {
			DontDestroyOnLoad(this.gameObject);
		}

		private void Start () {
			foreach (var node in _nodes) {
				node.InitializeNode();
			}
		}
		
		private void OnDestroy () {
			foreach (var node in _nodes) {
				node.DisposeNode();
			}
		}
	}
}
