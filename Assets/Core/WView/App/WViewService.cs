using Core.WView.Api;

namespace Core.WView.App {
	public class WViewService : IWViewService {
		private readonly UniWebView _uniWView;

		public WViewService (UniWebView uniWView) {
			_uniWView = uniWView;
		}

		public void Load (string url) {
			_uniWView.Load(url);
			_uniWView.Show();
		}
	}
}
