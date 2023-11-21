using Core.Nodes;
using UnityEngine;

namespace Core.App {
	public class AppNodesStartup : MonoBehaviour {
		[SerializeField]
		private bool _dontDestroyOnLoad;
		
		[SerializeField]
		private AppNode[] _nodes;

		private void Awake () {
			if (_dontDestroyOnLoad) 
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
