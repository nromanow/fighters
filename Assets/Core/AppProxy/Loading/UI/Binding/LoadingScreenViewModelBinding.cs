using Core.AppProxy.Loading.UI.ViewModels;
using Core.Binding;
using TMPro;
using UnityEngine;

namespace Core.AppProxy.Loading.UI.Binding {
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
