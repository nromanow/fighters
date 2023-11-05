using Core.Meta.Fight.Models;
using UniRx;

namespace Core.Meta.Fight.Api {
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
