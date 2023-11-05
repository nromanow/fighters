using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.App {
	public class AppComponentRegistry : IDisposable {
		private readonly List<object> _items = new();

		public T Instantiate<T> (params object[] args) where T : class {
			var constructorParams =
				typeof(T)
					.GetConstructors()
					.First()
					.GetParameters();

			var requiredTypes =
				constructorParams
					.Select(x => x.ParameterType);

			var requiredInstances =
				_items
					.Where(x => requiredTypes.Any(type => type.IsInstanceOfType(x)))
					.ToList();

			args = args.Concat(requiredInstances).ToArray();

			var sortedArgsArray = requiredTypes
				.Select(type => args.First(type.IsInstanceOfType))
				.ToArray();

			var instance = args.Any()
				? Activator.CreateInstance(typeof(T), sortedArgsArray)
				: Activator.CreateInstance(typeof(T));

			_items.Add(instance);

			return instance as T;
		}

		public T Resolve<T> () where T : class {
			return _items
				.OfType<T>()
				.SingleOrDefault();
		}

		public T[] ResolveAll<T> () where T : class {
			return _items
				.OfType<T>()
				.ToArray();
		}

		public void Register<T> (T item) where T : class {
			_items.Add(item);
		}

		public void Dispose () {
			foreach (var item in _items) {
				if (item is IDisposable disposable) disposable.Dispose();
			}
		}
	}
}
