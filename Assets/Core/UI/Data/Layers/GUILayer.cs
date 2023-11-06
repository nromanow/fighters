using Core.Binding;
using Core.UI.Data.Forms;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI.Data.Layers {
	public class GUILayer : MonoBehaviour {
		private readonly Dictionary<GUIForm, GameObject> _formInstances = new();

		[SerializeField]
		private GUILayerType _layerType;

		public GUILayerType layerType => _layerType;

		public void CreateFormInstance<T> (GUIForm form, T item = default) {
			var instance = Instantiate(form.source, transform);

			if (item != null) {
				instance
					.GetComponentInChildren<BindingItemView<T>>()
					.SetTarget(item);
			}
			
			_formInstances.Add(form, instance);
		}
		
		public void DestroyFormInstance (GUIForm form) {
			if (!_formInstances.TryGetValue(form, out var instance)) return;

			Destroy(instance);
			_formInstances.Remove(form);
		}
	}

	public enum GUILayerType {
		Base = 0,
		Popup = 1,
		HUD = 2,
	}
}
