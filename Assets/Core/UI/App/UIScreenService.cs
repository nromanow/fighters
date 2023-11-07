using Core.UI.Api;
using Core.UI.Data.Forms;
using Core.UI.Data.Layers;
using System;
using System.Linq;

namespace Core.UI.App {
	public class UIScreenService : IUIScreenService {
		private readonly GUILayer[] _layers;

		public UIScreenService (GUILayer[] layers) {
			_layers = layers;
		}

		public void ShowForm<T> (GUIForm form, T item = default) where T : class {
			GetLayer(form.layerType)
				.CreateFormInstance(form, item);
		}
		
		public void CloseForm (GUIForm form) {
			GetLayer(form.layerType)
				.DestroyFormInstance(form);
		}

		private GUILayer GetLayer (GUILayerType formLayerType) { 
			var layer = _layers.SingleOrDefault(x => x.layerType == formLayerType);

			if (layer == null) {
				throw new ArgumentException($"Layer with type {formLayerType} not found");
			}

			return layer;
		}
	}
}
