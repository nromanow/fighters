using Core.Gameplay.Fight.Models;
using UniRx;

namespace Core.Gameplay.Fight.Api {
	public interface IFightProvider {
		ReactiveCommand win { get; }
		
		IReadOnlyReactiveProperty<int> redScore { get; }
		IReadOnlyReactiveProperty<int> blueScore { get; }
		
		void RegisterRedFighter (FighterModel redFighter);
		
		void RegisterBlueFighter (FighterModel blueFighter);
		
		void RegisterRedKick ();
		
		void RegisterBlueKick ();
	}
}
