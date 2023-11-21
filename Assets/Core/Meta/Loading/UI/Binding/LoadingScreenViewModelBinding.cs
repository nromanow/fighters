using Core.Binding;
using Core.Meta.Loading.UI.ViewModels;
using TMPro;
using UnityEngine;

namespace Core.Meta.Loading.UI.Binding {
	public class LoadingScreenViewModelBinding : BindingItemView<LoadingScreenViewModel> {
		[SerializeField]
		private TextMeshProUGUI _progressLabel;

		protected override void OnUpdate () {
			base.OnUpdate();

			_progressLabel.text = $"Loading... {target.progress.Value}%";
		}
		
		protected override void OnInitialize () {
			base.OnInitialize();
			
			this.Subscribe(target.progress);
		}
	}
}
