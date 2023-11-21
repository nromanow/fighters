using Core.Gameplay.Fight.Api;
using Core.Gameplay.Fight.Binding;
using Core.Gameplay.Fight.Models;
using System.Linq;

namespace Core.Gameplay.Fight.App {
	public class FightViewInitializeService : IFightViewInitializeService {
		public void InitFighters (FighterModel redFighter, FighterModel blueFighter) {
			var views = UnityEngine.Object.FindObjectsOfType<FighterModelBinding>();

			var redFighterView = views.Single(x => x.isRed);
			redFighterView.SetTarget(redFighter);
			
			var blueFighterView = views.Single(x => !x.isRed);
			blueFighterView.SetTarget(blueFighter);
		}
	}
}
