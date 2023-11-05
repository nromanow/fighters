using Core.Binding;
using Core.Meta.Fight.UI.ViewModels;
using TMPro;
using UnityEngine;

namespace Core.Meta.Fight.UI.Binding {
	public class PostMatchScreenViewModelBinding : BindingItemView<PostMatchScreenViewModel> {
		[SerializeField]
		private TextMeshProUGUI _redScoreText;
		
		[SerializeField]
		private TextMeshProUGUI _blueScoreText;
		
		[SerializeField]
		private GameObject _redWinnerLabel;
		
		[SerializeField]
		private GameObject _blueWinnerLabel;

		public void Restart () {
			target.Restart();
		}
		
		protected override void OnUpdate () {
			base.OnUpdate();
			
			_redScoreText.text = target.redScore.ToString();
			_blueScoreText.text = target.blueScore.ToString();
			
			_redWinnerLabel.SetActive(target.isRedWinner);
			_blueWinnerLabel.SetActive(!target.isRedWinner);
		}
	}
}
