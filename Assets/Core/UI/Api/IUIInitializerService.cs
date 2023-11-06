using Core.App;

namespace Core.UI.Api {
	public interface IUIInitializerService {
		void Initialize (AppComponentRegistry componentRegistry);
	}
}
