using System;
using UniRx;
using UnityEngine;

namespace Core.Binding {
	public class BindingItemView<T> : MonoBehaviour {
		private Action _onDataChanged;
		public T target { get; private set; }

		protected readonly CompositeDisposable bindingDisposable = new();

		private void OnDestroy () {
			OnDeinitialize();

			bindingDisposable.Dispose();
		}

		protected virtual void OnInitialize () {
			_onDataChanged += OnUpdate;
		}

		protected virtual void OnDeinitialize () {
			_onDataChanged -= OnUpdate;
		}
		
		protected virtual void OnUpdate() {}

		public Action GetOnDataChangedEvent () {
			return _onDataChanged;
		}

		public void SetTarget (T itemTarget) {
			target = itemTarget;

			OnInitialize();
			OnUpdate();
		}

		public void AddDisposable (IDisposable disposable) {
			this.bindingDisposable.Add(disposable);
		}
	}

	public static class BindingItemViewExtensions {
		public static void Subscribe<TItem, TObservable> (this BindingItemView<TItem> bindingItemView, IObservable<TObservable> source) {
			var action = bindingItemView.GetOnDataChangedEvent();
			bindingItemView.AddDisposable(source.Subscribe(_ => action()));
		}
	}
}
