using System;

namespace Core.Utils {
	public static class UniRxUtils {
		public static void RecreateDisposable<T> (ref T disposable) where T : IDisposable, new() {
			ClearDisposable(ref disposable);
			disposable = new T();
		}
		
		public static void ClearDisposable<T> (ref T disposable) where T : IDisposable {
			disposable?.Dispose();
			disposable = default;
		}
	}
}
