﻿using System.Threading;
using UnityEngine;

namespace Core.App {
	public class AppModule : ScriptableObject {
		protected readonly CancellationTokenSource moduleCancellationTokenSource = new();
		
		public virtual void OnInitialize (AppComponentRegistry componentRegistry) { }

		public virtual void OnDispose () {
			moduleCancellationTokenSource.Cancel();
			moduleCancellationTokenSource.Dispose();
		}
	}
}
