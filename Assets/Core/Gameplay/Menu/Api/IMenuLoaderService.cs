using System;

namespace Core.Gameplay.Menu.Api {
	public interface IMenuLoaderService {
		void LoadMenu (Action onLoaded);
	}
}
