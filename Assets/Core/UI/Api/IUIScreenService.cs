using Core.UI.Data.Forms;

namespace Core.UI.Api {
	public interface IUIScreenService {
		void ShowForm<T> (GUIForm form, T item = default) where T : class;

		void CloseForm (GUIForm form);
	}
}
