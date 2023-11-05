using System;

namespace Core.Meta.Menu.Api {
	public interface IMenuLoaderService {
		void LoadMenu (Action onLoaded);
	}
}
