using Core.Meta.UI.Data.Forms;

namespace Core.Meta.UI.Api {
	public interface IUIScreenService {
		void ShowForm<T> (GUIForm form, T item = default);

		void CloseForm (GUIForm form);
	}
}
