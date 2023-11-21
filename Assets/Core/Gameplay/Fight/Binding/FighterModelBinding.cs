using Core.Binding;
using Core.Gameplay.Fight.Models;
using UniRx;
using UnityEngine;

namespace Core.Gameplay.Fight.Binding {
	[RequireComponent(typeof(Animator))]
	public class FighterModelBinding : BindingItemView<FighterModel> {
		[SerializeField]
		private bool _isRed;
		
		public bool isRed => _isRed;
		
		private Animator _animator;
		
		protected override void OnInitialize () {
			base.OnInitialize();
			
			_animator = GetComponent<Animator>();
			
			this.Subscribe(target.position);

			target.kick
				.Subscribe(_ => _animator.Play("Kick"))
				.AddTo(bindingDisposable);
		}

		protected override void OnUpdate () {
			base.OnUpdate();

			transform.position = new Vector3(0, target.position.Value, 0);
		}
	}
}
