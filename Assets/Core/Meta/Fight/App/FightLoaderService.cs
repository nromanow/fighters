using Core.Meta.Fight.Api;
using System;
using UnityEngine.SceneManagement;

namespace Core.Meta.Fight.App {
	public class FightLoaderService : IFightLoaderService {
		public void LoadFightScene (Action onLoaded) {
			SceneManager
				.LoadSceneAsync("Scenes/FightScene")
				.completed += operation => { onLoaded?.Invoke(); };
		}
	}
}
