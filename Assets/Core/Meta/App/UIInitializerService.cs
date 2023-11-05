using Core.App;
using System;
using System.Linq;
using UnityEngine;

namespace Core.Meta.App {
	public class UIInitializerService {
		private readonly Settings _settings;

		public UIInitializerService (Settings settings ) {
			_settings = settings;
		}

		public void Initialize (AppComponentRegistry componentRegistry) {
			componentRegistry
				.Register(_settings.layersPrefabs.Select(x => {
						var instance = UnityEngine.Object.Instantiate(x);
						UnityEngine.Object.DontDestroyOnLoad(instance.gameObject);
						return instance;
					})
					.ToArray());
		}

		[Serializable]
		public class Settings {
			[SerializeField]
			private UI.Data.Layers.GUILayer[] _layersPrefabs;

			public UI.Data.Layers.GUILayer[] layersPrefabs => _layersPrefabs;
		}
	}
}
